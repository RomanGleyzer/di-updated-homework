using System.Drawing;
using System.Drawing.Imaging;
using TagsCloud.Core.Cloud.Interfaces;

namespace TagsCloud.Core.Cloud.Rendering;

public class SystemDrawingPngCloudRenderer(
    string fontName,
    IFontSizeScaler scaler,
    Color[] palette,
    ITagColorSelector colorSelector,
    ITagsOffsetCalculator offsetCalculator) : ICloudRenderer
{
    private readonly string _fontName = fontName;
    private readonly IFontSizeScaler _scaler = scaler;
    private readonly Color[] _palette = palette;
    private readonly ITagColorSelector _colorSelector = colorSelector;
    private readonly ITagsOffsetCalculator _offsetCalculator = offsetCalculator;

    public void Render(IReadOnlyCollection<Tag> tags, Size imageSize, Stream output)
    {
        using var bmp = new Bitmap(imageSize.Width, imageSize.Height);
        using var g = Graphics.FromImage(bmp);

        g.Clear(Color.White);

        var offset = _offsetCalculator.CalculateOffset(tags, imageSize);

        var brushes = new SolidBrush[_palette.Length];
        for (var i = 0; i < _palette.Length; i++)
            brushes[i] = new SolidBrush(_palette[i]);

        try
        {
            foreach (var tag in tags)
            {
                var fontSize = _scaler.Scale(tag.Frequency);

                using var font = new Font(_fontName, fontSize, FontStyle.Regular, GraphicsUnit.Pixel);

                var index = _colorSelector.SelectColorIndex(tag, brushes.Length);
                var bounds = tag.Bounds;

                g.DrawString(tag.Word, font, brushes[index], bounds.Left + offset.X, bounds.Top + offset.Y);
            }

            bmp.Save(output, ImageFormat.Png);
        }
        finally
        {
            foreach (var b in brushes)
                b.Dispose();
        }
    }
}
