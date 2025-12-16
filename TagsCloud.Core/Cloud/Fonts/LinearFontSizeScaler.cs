using TagsCloud.Core.Cloud.Interfaces;

namespace TagsCloud.Core.Cloud.Fonts;

public sealed class LinearFontSizeScaler : IFontSizeScaler
{
    private readonly int _minFreq;
    private readonly int _maxFreq;
    private readonly float _minSize;
    private readonly float _maxSize;

    public LinearFontSizeScaler(int minFreq, int maxFreq, float minSize, float maxSize)
    {
        _minFreq = Math.Max(1, minFreq);
        _maxFreq = Math.Max(_minFreq, maxFreq);
        _minSize = minSize;
        _maxSize = Math.Max(minSize, maxSize);
    }

    public float Scale(int frequency)
    {
        var clampedFrequency = Math.Max(_minFreq, Math.Min(_maxFreq, frequency));

        var normalizedFrequency = (clampedFrequency - _minFreq)
            / (float)(_maxFreq - _minFreq == 0 ? 1 : (_maxFreq - _minFreq));

        return _minSize + (_maxSize - _minSize) * normalizedFrequency;
    }
}
