using FluentAssertions;
using TagsCloud.Core.Cloud.Fonts;

namespace TagsCloud.Tests;

public class LinearFontSizeScalerTests
{
    private const float Eps = 0.001f;
    private LinearFontSizeScaler _scaler = null!;

    [SetUp]
    public void SetUp()
    {
        _scaler = new LinearFontSizeScaler(minFreq: 1, maxFreq: 10, minSize: 10, maxSize: 20);
    }

    [Test]
    public void Scale_Min_ReturnsMin()
    {
        var size = _scaler.Scale(1);

        size.Should().BeApproximately(10f, Eps);
    }

    [Test]
    public void Scale_Max_ReturnsMax()
    {
        var size = _scaler.Scale(10);

        size.Should().BeApproximately(20f, Eps);
    }

    [Test]
    public void Scale_HigherFrequency_ReturnsLarger()
    {
        var small = _scaler.Scale(2);
        var big = _scaler.Scale(9);

        big.Should().BeGreaterThan(small);
    }

    [Test]
    public void Scale_OutOfRange_ReturnsClamped()
    {
        var scaler = new LinearFontSizeScaler(minFreq: 2, maxFreq: 3, minSize: 10, maxSize: 20);

        var below = scaler.Scale(0);
        var above = scaler.Scale(999);

        below.Should().BeApproximately(10f, Eps);
        above.Should().BeApproximately(20f, Eps);
    }
}
