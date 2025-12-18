using System.Drawing;
using TagsCloud.Core.Cloud.Interfaces;
using TagsCloud.Core.Words.Interfaces;

namespace TagsCloud.Core.Cloud;

public sealed class MultiCloudGenerator(
    IWordsSource source,
    IWordsAnalyzer analyzer,
    IFontSizeMeasurer measurer,
    IEnumerable<ICloudLayouterFactory> layouterFactories,
    ICloudRenderer renderer) : IMultiCloudGenerator
{
    private readonly IWordsSource _source = source;
    private readonly IWordsAnalyzer _analyzer = analyzer;
    private readonly IFontSizeMeasurer _measurer = measurer;
    private readonly ICloudRenderer _renderer = renderer;
    private readonly ICloudLayouterFactory[] _factories = [.. layouterFactories];

    public void GenerateAll(Size imageSize, Func<string, Stream> outputFactory)
    {
        var words = _source.ReadWords();
        var infos = _analyzer.Analyze(words);

        for (var i = 0; i < _factories.Length; i++)
        {
            var factory = _factories[i];
            var layouter = factory.Create(imageSize);
            var tags = new List<Tag>(infos.Length);

            for (var j = 0; j < infos.Length; j++)
            {
                var info = infos[j];
                var size = _measurer.Measure(info.Word, info.Frequency);
                var rect = layouter.PutNext(size);

                tags.Add(new Tag(info.Word, rect, info.Frequency));
            }

            using var stream = outputFactory(factory.Name);
            _renderer.Render(tags, imageSize, stream);
        }
    }
}