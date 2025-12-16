using System.Drawing;
using TagsCloud.Core.Cloud.Interfaces;

namespace TagsCloud.Core.Cloud.Fonts;

public sealed class SystemDrawingTextMeasurer(string fontName, IFontSizeScaler scaler) : IFontSizeMeasurer
{
    private readonly string _fontName = fontName;
    private readonly IFontSizeScaler _scaler = scaler;

    public Size Measure(string word, int frequency)
    {
        var fontSize = _scaler.Scale(frequency);

        using var bmp = new Bitmap(1, 1);
        using var graphics = Graphics.FromImage(bmp);
        using var font = new Font(_fontName, fontSize, FontStyle.Regular, GraphicsUnit.Pixel);

        var size = graphics.MeasureString(word, font);

        return new Size((int)Math.Ceiling(size.Width), (int)Math.Ceiling(size.Height));
    }
}
