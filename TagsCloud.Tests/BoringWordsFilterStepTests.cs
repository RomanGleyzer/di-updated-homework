using FluentAssertions;
using TagsCloud.Core.Words;
using TagsCloud.Core.Words.Steps;

namespace TagsCloud.Tests;

public class BoringWordsFilterStepTests
{
    private BoringWordsFilterStep _step = null!;

    [SetUp]
    public void SetUp()
    {
        var provider = new RussianBoringWordsProvider();
        _step = new BoringWordsFilterStep(provider);
    }

    [TestCase("и")]
    [TestCase("в")]
    [TestCase("во")]
    [TestCase("не")]
    [TestCase("что")]
    public void Process_Boring_ReturnsNull(string word)
    {
        var result = _step.Process(word);

        result.Should().BeNull();
    }

    [TestCase("код")]
    [TestCase("тест")]
    [TestCase("программа")]
    public void Process_NotBoring_ReturnsWord(string word)
    {
        var result = _step.Process(word);

        result.Should().Be(word);
    }
}
