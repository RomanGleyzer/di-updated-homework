using FluentAssertions;
using System.Drawing;
using TagsCloud.Core.Cloud;
using TagsCloud.Core.Cloud.Fonts;
using TagsCloud.Core.Cloud.Interfaces;
using TagsCloud.Core.Cloud.Rendering;
using TagsCloud.Core.Words;
using TagsCloud.Core.Words.Interfaces;
using TagsCloud.Core.Words.Steps;

namespace TagsCloud.Tests;

public class MultiGeneratorTests
{
    private static readonly Size ImageSize = new(500, 400);

    private MultiCloudGenerator _generator = null!;

    [SetUp]
    public void SetUp()
    {
        IWordsSource source = new FakeWordsSource(["КОД", "код", "и", "тест", "тест", "тест"]);

        IWordProcessingStep[] steps =
        [
            new LowercaseStep(),
            new BoringWordsFilterStep(new RussianBoringWordsProvider())
        ];

        IWordsAnalyzer analyzer = new WordsAnalyzer(steps);
        IFontSizeScaler scaler = new LinearFontSizeScaler(1, 10, 12, 48);
        IFontSizeMeasurer measurer = new SystemDrawingTextMeasurer("Arial", scaler);

        ICloudRenderer renderer = new SystemDrawingPngCloudRenderer(
            "Arial",
            scaler,
            [Color.Black, Color.Red],
            new FrequencyColorSelector(),
            new CenteringOffsetCalculator());

        ICloudLayouterFactory[] factories =
        [
            new CircularFactory("circular-center", s => new Point(s.Width / 2, s.Height / 2)),
            new CircularFactory("circular-shift",  s => new Point(s.Width / 3, s.Height / 3))
        ];

        _generator = new MultiCloudGenerator(source, analyzer, measurer, factories, renderer);
    }

    [Test]
    public void GenerateAll_TwoFactories_TwoPngs()
    {
        var outputs = new Dictionary<string, MemoryStream>();

        _generator.GenerateAll(ImageSize, name =>
        {
            var ms = new MemoryStream();
            outputs.Add(name, ms);
            return ms;
        });

        outputs.Keys.Should().BeEquivalentTo(["circular-center", "circular-shift"]);

        foreach (var pair in outputs)
        {
            var name = pair.Key;
            var stream = pair.Value;

            stream.Length.Should().BeGreaterThan(0, $"'{name}' должен записать непустой результат");

            stream.Position = 0;
            using (var bmp = new Bitmap(stream))
            {
                bmp.Width.Should().Be(ImageSize.Width);
                bmp.Height.Should().Be(ImageSize.Height);
            }

            stream.Dispose();
        }
    }

    private sealed class FakeWordsSource(IEnumerable<string> words) : IWordsSource
    {
        private readonly IEnumerable<string> _words = words;

        public IEnumerable<string> ReadWords() => _words;
    }

    private sealed class CircularFactory(string name, Func<Size, Point> centerProvider) : ICloudLayouterFactory
    {
        private readonly Func<Size, Point> _centerProvider = centerProvider;

        public string Name { get; } = name;

        public ICloudLayouter Create(Size imageSize)
        {
            var center = _centerProvider(imageSize);
            return new Core.Layouters.CircularCloudLayouter(center);
        }
    }
}
