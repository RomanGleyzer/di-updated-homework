using System.Drawing;

namespace TagsCloud.Core.Cloud;

public interface ICloudLayouter
{
    Rectangle PutNext(Size size);
}
