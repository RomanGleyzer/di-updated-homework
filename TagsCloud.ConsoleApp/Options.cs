using CommandLine;

namespace TagsCloud.ConsoleApp;

public sealed class Options
{
    [Option("input", Required = true)]
    public string InputPath { get; set; } = string.Empty;

    [Option("output", Required = true, HelpText = "Шаблон имени файла. Пример: {name}.png")]
    public string OutputPattern { get; set; } = "{name}.png";

    [Option("width", Required = true)]
    public int Width { get; set; }

    [Option("height", Required = true)]
    public int Height { get; set; }

    [Option("font", Required = false)]
    public string FontName { get; set; } = null!;

    [Option("colors", Required = false, HelpText = "Палитра через запятую: #RRGGBB,#RRGGBB или имена цветов.")]
    public string Colors { get; set; } = "#1f77b4,#ff7f0e,#2ca02c,#d62728";

    [Option("boring", Required = false, HelpText = "Файл со скучными словами (по одному в строке).")]
    public string? BoringWordsPath { get; set; }

    [Option("minFont", Required = false, Default = 12f)]
    public float MinFont { get; set; }

    [Option("maxFont", Required = false, Default = 64f)]
    public float MaxFont { get; set; }
}
