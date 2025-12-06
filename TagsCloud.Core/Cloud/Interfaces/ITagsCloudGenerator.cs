using System.Drawing;

namespace TagsCloud.Core.Cloud.Interfaces;

public interface ITagsCloudGenerator
{
    void Generate(Size imageSize, Stream output);
}
