using System.Drawing;

namespace TagsCloud.Core.Cloud;

public interface IFontSizeProvider
{
    Size Measure(string word, int frequency);
}
