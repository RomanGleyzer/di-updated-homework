using System.Drawing;
using FluentAssertions;
using TagsCloud.Core.Cloud;
using TagsCloud.Core.Cloud.Rendering;

namespace TagsCloud.Tests;

public class CenteringOffsetCalculatorTests
{
    private CenteringOffsetCalculator _calc = null!;

    [SetUp]
    public void SetUp()
    {
        _calc = new CenteringOffsetCalculator();
    }

    [Test]
    public void CalculateOffset_Tags_ReturnsCenteredOffset()
    {
        var tags = new[]
        {
            new Tag("a", new Rectangle(10, 20, 100, 40), 1),
            new Tag("b", new Rectangle(200, 120, 60, 20), 2)
        };
        var imageSize = new Size(500, 400);

        var offset = _calc.CalculateOffset(tags, imageSize);

        var minX = tags.Min(t => t.Bounds.Left);
        var maxX = tags.Max(t => t.Bounds.Right);
        var minY = tags.Min(t => t.Bounds.Top);
        var maxY = tags.Max(t => t.Bounds.Bottom);

        var cloudW = maxX - minX;
        var cloudH = maxY - minY;

        var expectedLeft = (imageSize.Width - cloudW) / 2;
        var expectedTop = (imageSize.Height - cloudH) / 2;

        (minX + offset.X).Should().Be(expectedLeft);
        (minY + offset.Y).Should().Be(expectedTop);
    }

    [Test]
    public void CalculateOffset_NoTags_ReturnsEmpty()
    {
        var tags = Array.Empty<Tag>();
        var imageSize = new Size(100, 100);

        var offset = _calc.CalculateOffset(tags, imageSize);

        offset.Should().Be(Point.Empty);
    }
}
