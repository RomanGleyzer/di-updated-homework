namespace TagsCloud.Core.Words.Interfaces;

public interface IWordsAnalyzer
{
    WordInfo[] Analyze(IEnumerable<string> words);
}
