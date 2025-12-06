using System.Drawing;

namespace TagsCloud.Core.Cloud.Interfaces;

public interface IFontSizeProvider
{
    Size Measure(string word, int frequency);
}
