using TagsCloud.Core.Cloud.Interfaces;

namespace TagsCloud.Core.Cloud.Rendering;

public class FrequencyColorSelector : ITagColorSelector
{
    public int SelectColorIndex(Tag tag, int paletteSize)
    {
        return tag.Frequency % paletteSize;
    }
}
