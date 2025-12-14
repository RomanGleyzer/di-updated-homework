using TagsCloud.Core.Words.Interfaces;

namespace TagsCloud.Core.Words;

public class FileWordsSource(string filePath) : IWordsSource
{
    public IEnumerable<string> ReadWords()
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"Файл не был найден: '{filePath}'", filePath);

        return File.ReadLines(filePath)
            .Select(l => (l ?? string.Empty).Trim())
            .Where(l => l.Length > 0);
    }
}
