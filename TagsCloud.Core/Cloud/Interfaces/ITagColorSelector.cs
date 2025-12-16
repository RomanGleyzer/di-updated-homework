namespace TagsCloud.Core.Cloud.Interfaces;

public interface ITagColorSelector
{
    int SelectColorIndex(Tag tag, int paletteSize);
}
