using System.Drawing;
using TagsCloud.Core.Cloud.Interfaces;

namespace TagsCloud.Core.Cloud.Rendering;

public class CenteringOffsetCalculator : ITagsOffsetCalculator
{
    public Point CalculateOffset(IReadOnlyCollection<Tag> tags, Size imageSize)
    {
        if (tags.Count == 0)
            return Point.Empty;

        var minX = int.MaxValue;
        var maxX = int.MinValue;
        var minY = int.MaxValue;
        var maxY = int.MinValue;

        foreach (var t in tags)
        {
            var r = t.Bounds;
            if (r.Left < minX) minX = r.Left;
            if (r.Top < minY) minY = r.Top;
            if (r.Right > maxX) maxX = r.Right;
            if (r.Bottom > maxY) maxY = r.Bottom;
        }

        var cloudW = maxX - minX;
        var cloudH = maxY - minY;

        var offsetX = (imageSize.Width - cloudW) / 2 - minX;
        var offsetY = (imageSize.Height - cloudH) / 2 - minY;

        return new Point(offsetX, offsetY);
    }
}
