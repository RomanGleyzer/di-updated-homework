using TagsCloud.Core.Words.Interfaces;

namespace TagsCloud.Core.Words;

public class RussianBoringWordsProvider : IBoringWordsProvider
{
    private static readonly HashSet<string> Default =
    [
        "и","в","во","не","что","он","на","я","с","со","как","а","то","все","она",
        "так","его","но","да","ты","к","у","же","вы","за","бы","по","только","ее",
        "мне","было","вот","от","меня","еще","нет","о","из","ему","теперь","когда"
    ];

    public ISet<string> BoringWords => Default;
}
