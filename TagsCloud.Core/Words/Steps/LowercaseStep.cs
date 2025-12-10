using System.Globalization;
using TagsCloud.Core.Words.Interfaces;

namespace TagsCloud.Core.Words.Steps;

public class LowercaseStep : IWordProcessingStep
{
    public string? Process(string word)
    {
        return word.ToLower(CultureInfo.InvariantCulture);
    }
}
