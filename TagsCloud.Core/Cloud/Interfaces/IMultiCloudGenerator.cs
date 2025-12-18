using System.Drawing;

namespace TagsCloud.Core.Cloud.Interfaces;

public interface IMultiCloudGenerator
{
    void GenerateAll(Size imageSize, Func<string, Stream> outputFactory);
}