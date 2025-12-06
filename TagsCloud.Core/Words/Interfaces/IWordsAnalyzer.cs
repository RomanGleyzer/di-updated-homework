namespace TagsCloud.Core.Words.Interfaces;

public interface IWordsAnalyzer
{
    IReadOnlyCollection<WordInfo> Analyze(IEnumerable<string> words);
}
