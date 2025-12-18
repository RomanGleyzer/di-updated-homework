using System.Drawing;
using TagsCloud.Core.Cloud.Interfaces;

namespace TagsCloud.Core.Layouters;

public sealed class CircularCenterLayouterFactory : ICloudLayouterFactory
{
    public string Name => "circular";

    public ICloudLayouter Create(Size imageSize)
    {
        var center = new Point(imageSize.Width / 2, imageSize.Height / 2);
        return new CircularCloudLayouter(center);
    }
}
