using System.Drawing;

namespace TagsCloud.Core.Cloud.Interfaces;

public interface ITagsOffsetCalculator
{
    Point CalculateOffset(IReadOnlyCollection<Tag> tags, Size imageSize);
}
