using System.Drawing;

namespace TagsCloud.Core.Cloud.Interfaces;

public interface IFontSizeMeasurer
{
    Size Measure(string word, int frequency);
}
