using TagsCloud.Core.Words.Interfaces;

namespace TagsCloud.Core.Words.Steps;

public class BoringWordsFilterStep : IWordProcessingStep
{
    private readonly ISet<string> _boring;
    private readonly IDictionary<bool, Func<string, string>> _handlers;

    public BoringWordsFilterStep(IBoringWordsProvider boringWordsProvider)
    {
        _boring = boringWordsProvider.BoringWords;

        _handlers = new Dictionary<bool, Func<string, string>>()
        {
            { true, word => null! },
            { false, word => word }
        };
    }

    public string? Process(string word)
    {
        return _handlers[_boring.Contains(word)](word);
    }
}
