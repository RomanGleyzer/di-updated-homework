namespace TagsCloud.Core.Words.Interfaces;

public interface IBoringWordsProvider
{
    ISet<string> BoringWords { get; }
}
