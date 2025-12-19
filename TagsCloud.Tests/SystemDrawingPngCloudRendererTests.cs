using System.Drawing;
using FluentAssertions;
using TagsCloud.Core.Cloud;
using TagsCloud.Core.Cloud.Fonts;
using TagsCloud.Core.Cloud.Rendering;

namespace TagsCloud.Tests;

public class SystemDrawingPngCloudRendererTests
{
    private Tag[] _tags = null!;
    private Size _imageSize;
    private LinearFontSizeScaler _scaler = null!;
    private Color[] _palette = null!;
    private SystemDrawingPngCloudRenderer _renderer = null!;

    [SetUp]
    public void SetUp()
    {
        _tags =
        [
            new Tag("code", new Rectangle(10, 10, 100, 30), 10),
            new Tag("test", new Rectangle(10, 60, 80, 20), 5)
        ];

        _imageSize = new Size(320, 240);

        _scaler = new LinearFontSizeScaler(1, 10, 12, 48);
        _palette = [Color.Black, Color.Red, Color.Green];

        _renderer = new SystemDrawingPngCloudRenderer(
            fontName: "Arial",
            scaler: _scaler,
            palette: _palette,
            colorSelector: new FrequencyColorSelector(),
            offsetCalculator: new CenteringOffsetCalculator());
    }

    [Test]
    public void Render_Tags_WritesValidPng()
    {
        using var ms = new MemoryStream();

        _renderer.Render(_tags, _imageSize, ms);

        ms.Length.Should().BeGreaterThan(0);

        ms.Position = 0;
        using var bmp = new Bitmap(ms);
        bmp.Width.Should().Be(_imageSize.Width);
        bmp.Height.Should().Be(_imageSize.Height);
    }
}
