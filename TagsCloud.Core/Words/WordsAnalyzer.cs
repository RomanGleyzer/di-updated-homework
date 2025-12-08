using TagsCloud.Core.Words.Interfaces;

namespace TagsCloud.Core.Words;

public class WordsAnalyzer(IWordProcessingStep[] steps) : IWordsAnalyzer
{
    private readonly IWordProcessingStep[] _steps = steps;

    public WordInfo[] Analyze(IEnumerable<string> words)
    {
        var processed = words.Select(ProcessWordThroughPipeline).OfType<string>();
        return [.. processed.GroupBy(word => word).Select(group => new WordInfo(group.Key, group.Count()))];
    }

    private string? ProcessWordThroughPipeline(string word)
    {
        var current = word;

        foreach (var step in _steps)
        {
            if (current == null)
                return null;

            current = step.Process(current);
        }

        return current;
    }
}
