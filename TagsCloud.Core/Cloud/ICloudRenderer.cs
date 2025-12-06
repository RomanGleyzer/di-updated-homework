using System.Drawing;

namespace TagsCloud.Core.Cloud;

public interface ICloudRenderer
{
    void Render(IReadOnlyCollection<Tag> tags, Size imageSize, Stream output);
}
