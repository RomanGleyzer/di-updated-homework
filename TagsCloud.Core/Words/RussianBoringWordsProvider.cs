using TagsCloud.Core.Words.Interfaces;

namespace TagsCloud.Core.Words;

public class RussianBoringWordsProvider : IBoringWordsProvider
{
    private readonly HashSet<string> _words = [];

    public ISet<string> BoringWords => _words;
}
