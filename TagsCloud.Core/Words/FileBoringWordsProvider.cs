using TagsCloud.Core.Words.Interfaces;

namespace TagsCloud.Core.Words;

public class FileBoringWordsProvider(string filePath) : IBoringWordsProvider
{
    private readonly HashSet<string> _words = [.. File.ReadAllLines(filePath).Select(t => t.Trim()).Where(t => t.Length > 0)];

    public ISet<string> BoringWords => _words;
}
