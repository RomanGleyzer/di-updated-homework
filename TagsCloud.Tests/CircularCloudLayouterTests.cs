using System.Drawing;
using FluentAssertions;
using TagsCloud.Core.Layouters;

namespace TagsCloud.Tests;

public class CircularCloudLayouterTests
{
    private CircularCloudLayouter _layouter = null!;
    private Size _size;

    [SetUp]
    public void SetUp()
    {
        _layouter = new CircularCloudLayouter(new Point(400, 300));
        _size = new Size(50, 20);
    }

    [Test]
    public void PutNext_ManyRectangles_NoIntersections()
    {
        const int count = 120;

        var rectangles = Enumerable.Range(0, count)
            .Select(_ => _layouter.PutNext(_size))
            .ToArray();

        for (var i = 0; i < rectangles.Length; i++)
            for (var j = i + 1; j < rectangles.Length; j++)
                rectangles[i].IntersectsWith(rectangles[j]).Should().BeFalse();
    }
}
