using FluentAssertions;
using TagsCloud.Core.Words;
using TagsCloud.Core.Words.Steps;

namespace TagsCloud.Tests;

public class WordsAnalyzerTests
{
    private WordsAnalyzer _analyzerWithBoringFilter = null!;
    private WordsAnalyzer _analyzerLowercaseOnly = null!;

    [SetUp]
    public void SetUp()
    {
        _analyzerWithBoringFilter = new WordsAnalyzer(
        [
            new LowercaseStep(),
            new BoringWordsFilterStep(new RussianBoringWordsProvider())
        ]);

        _analyzerLowercaseOnly = new WordsAnalyzer(
        [
            new LowercaseStep()
        ]);
    }

    [Test]
    public void Analyze_BoringAndCases_ReturnsFilteredLowercasedAndCounted()
    {
        var input = new[] { "КОД", "код", "и", "Тест", "тест", "тест" };

        var result = _analyzerWithBoringFilter.Analyze(input);

        result.Select(x => x.Word).Should().Equal(["тест", "код"]);
        result.Select(x => x.Frequency).Should().Equal([3, 2]);
    }

    [Test]
    public void Analyze_EqualFrequencies_ReturnsOrderedByWordAscending()
    {
        var input = new[] { "b", "a" };

        var result = _analyzerLowercaseOnly.Analyze(input);

        result.Select(x => x.Word).Should().Equal(["a", "b"]);
        result.Select(x => x.Frequency).Should().Equal([1, 1]);
    }
}
