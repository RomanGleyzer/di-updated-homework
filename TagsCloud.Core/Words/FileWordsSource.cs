using TagsCloud.Core.Words.Interfaces;

namespace TagsCloud.Core.Words;

public class FileWordsSource(string filePath) : IWordsSource
{
    private readonly string _filePath = filePath;

    public IEnumerable<string> ReadWords()
    {
        using var reader = new StreamReader(_filePath);

        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            if (line == null)
                continue;

            var trimmed = line.Trim();
            if (trimmed.Length == 0)
                continue;

            yield return trimmed;
        }
    }
}
