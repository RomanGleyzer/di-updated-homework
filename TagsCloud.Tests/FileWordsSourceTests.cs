using FluentAssertions;
using TagsCloud.Core.Words;

namespace TagsCloud.Tests;

public class FileWordsSourceTests
{
    private const string TxtExt = ".txt";
    private static readonly string[] contents =
        [
            "  hello  ",
            "",
            "   ",
            "\tworld\t",
            "code"
        ];

    [Test]
    public void ReadWords_FileNotFound_ThrowsFileNotFoundException()
    {
        var path = Path.Combine(TestContext.CurrentContext.WorkDirectory, Guid.NewGuid() + TxtExt);
        var source = new FileWordsSource(path);

        var act = () => source.ReadWords().ToArray();

        act.Should().Throw<FileNotFoundException>();
    }

    [Test]
    public void ReadWords_FileHasEmptyAndSpacedLines_ReturnsTrimmedNonEmptyWords()
    {
        var path = Path.Combine(TestContext.CurrentContext.WorkDirectory, Guid.NewGuid() + TxtExt);
        File.WriteAllLines(path, contents);

        var source = new FileWordsSource(path);

        var words = source.ReadWords().ToArray();

        words.Should().Equal(["hello", "world", "code"]);
    }
}
