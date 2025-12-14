using TagsCloud.Core.Words.Interfaces;

namespace TagsCloud.Core.Words.Steps;

public sealed class BoringWordsFilterStep(IBoringWordsProvider boringWordsProvider) : IWordProcessingStep
{
    private readonly ISet<string> _boring = boringWordsProvider.BoringWords;

    public string? Process(string word)
    {
        if (_boring.Contains(word))
            return null;

        return word;
    }
}
