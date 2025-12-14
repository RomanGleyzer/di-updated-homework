using TagsCloud.Core.Words.Interfaces;

namespace TagsCloud.Core.Words;

public class WordsAnalyzer(IWordProcessingStep[] steps) : IWordsAnalyzer
{
    private readonly IWordProcessingStep[] _steps = steps;

    public WordInfo[] Analyze(IEnumerable<string> words)
    {
        var processed = words
            .Select(ProcessWordThroughPipeline)
            .OfType<string>();

        return [.. processed
            .GroupBy(w => w)
            .Select(g => new WordInfo(g.Key, g.Count()))
            .OrderByDescending(x => x.Frequency)
            .ThenBy(x => x.Word)];
    }

    private string? ProcessWordThroughPipeline(string word)
    {
        var current = word;

        foreach (var step in _steps)
        {
            current = step.Process(current);
            if (current == null)
                return null;
        }

        return current;
    }
}
