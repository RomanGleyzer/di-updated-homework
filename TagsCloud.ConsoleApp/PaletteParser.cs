using System.Drawing;

namespace TagsCloud.ConsoleApp;

public static class PaletteParser
{
    private const string DefaultPalette = "#1f77b4,#ff7f0e,#2ca02c,#d62728";

    public static Color[] Parse(string? value)
    {
        var text = string.IsNullOrWhiteSpace(value) ? DefaultPalette : value;

        return [.. text.Split(',')
            .Select(s => s.Trim())
            .Where(s => s.Length > 0)
            .Select(ParseOne)];
    }

    private static Color ParseOne(string token) =>
        ColorTranslator.FromHtml(token);
}
