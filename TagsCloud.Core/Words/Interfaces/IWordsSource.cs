namespace TagsCloud.Core.Words.Interfaces;

public interface IWordsSource
{
    IEnumerable<string> ReadWords();
}
