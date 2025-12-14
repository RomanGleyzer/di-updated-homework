using System.Drawing;
using TagsCloud.Core.Cloud.Interfaces;
using TagsCloud.Core.Words.Interfaces;

namespace TagsCloud.Core.Cloud;

public class TagsCloudGenerator(
    IWordsSource source,
    IWordsAnalyzer analyzer,
    IFontSizeMeasurer fontSizeMeasurer,
    ICloudLayouter layouter,
    ICloudRenderer renderer) : ITagsCloudGenerator
{
    public void Generate(Size imageSize, Stream output)
    {
        var words = source.ReadWords();
        var wordInfos = analyzer.Analyze(words);

        var tags = new List<Tag>(wordInfos.Length);

        foreach (var info in wordInfos)
        {
            var size = fontSizeMeasurer.Measure(info.Word, info.Frequency);
            var rect = layouter.PutNext(size);

            tags.Add(new Tag(info.Word, rect, info.Frequency));
        }

        renderer.Render(tags, imageSize, output);
    }
}
