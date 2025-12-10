using TagsCloud.Core.Words.Interfaces;

namespace TagsCloud.Core.Words;

public class FileWordsSource(string filePath) : IWordsSource
{
    public IEnumerable<string> ReadWords()
    {
        return File.ReadLines(filePath)
            .Select(l => (l ?? string.Empty).Trim())
            .Where(l => l.Length > 0);
    }
}
