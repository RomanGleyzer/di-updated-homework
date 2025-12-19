using FluentAssertions;
using TagsCloud.Core.Cloud.Fonts;

namespace TagsCloud.Tests;

public class SystemDrawingTextMeasurerTests
{
    private SystemDrawingTextMeasurer _measurer = null!;

    [SetUp]
    public void SetUp()
    {
        var scaler = new LinearFontSizeScaler(minFreq: 1, maxFreq: 10, minSize: 10, maxSize: 60);
        _measurer = new SystemDrawingTextMeasurer("Arial", scaler);
    }

    [Test]
    public void Measure_HighFrequency_ReturnsLargerSize()
    {
        const string word = "word";

        var small = _measurer.Measure(word, frequency: 1);
        var big = _measurer.Measure(word, frequency: 10);

        big.Width.Should().BeGreaterThanOrEqualTo(small.Width);
        big.Height.Should().BeGreaterThanOrEqualTo(small.Height);
        (big.Width > small.Width || big.Height > small.Height).Should().BeTrue();
    }
}
