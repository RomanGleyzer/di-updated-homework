using System.Drawing;
using TagsCloud.Core.Cloud.Interfaces;
using TagsCloud.Core.Words.Interfaces;

namespace TagsCloud.Core.Cloud;

public class TagsCloudGenerator(
    IWordsSource source, 
    IWordsAnalyzer analyzer, 
    IFontSizeProvider fontSizeProvider, 
    ICloudLayouter layouter, 
    ICloudRenderer renderer) : ITagsCloudGenerator
{
    public void Generate(Size imageSize, Stream output)
    {
        var words = source.ReadWords();
        var wordInfos = analyzer.Analyze(words);

        var tags = new List<Tag>();

        foreach (var info in wordInfos)
        {
            var size = fontSizeProvider.Measure(info.Word, info.Frequency);
            var rect = layouter.PutNext(size);
            var tag = new Tag(info.Word, rect.X, rect.Y, rect.Width, rect.Height);

            tags.Add(tag);
        }

        renderer.Render(tags, imageSize, output);
    }
}
