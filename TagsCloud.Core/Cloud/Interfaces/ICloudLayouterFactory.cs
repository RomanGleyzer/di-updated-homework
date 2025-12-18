using System.Drawing;

namespace TagsCloud.Core.Cloud.Interfaces;

public interface ICloudLayouterFactory
{
    string Name { get; }
    ICloudLayouter Create(Size imageSize);
}
