using System.Drawing;

namespace TagsCloud.Core.Cloud.Interfaces;

public interface ICloudLayouter
{
    Rectangle PutNext(Size size);
}
