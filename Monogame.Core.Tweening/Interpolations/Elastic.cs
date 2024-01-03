namespace Monogame.Core.Tweening.Interpolations;

class Elastic : IntensityEasingInterpolation
{
    private double _c4;

    public Elastic(double intensity)
    {
        SetIntensity(intensity);

        _c4 = (2 * Math.PI) / 3;
    }

    protected override Func<double, double> EasingInFunction => (t) => { return 1 - EasingOutFunction(1 - t); };
    protected override Func<double, double> EasingOutFunction => (t) =>
    {
        return t == 0
            ? 0
            : t == 1
            ? 1
            : Math.Pow(2, -10 * t) * Math.Sin((t * 10 * Intensity - 0.75) * _c4) + 1;
    };
    protected override Func<double, double> EasingInOutFunction => (t) =>
    {
        return t == 0
            ? 0
            : t == 1
            ? 1
            : t < 0.5 ? (1 - EasingOutFunction(1 - 2 * t)) / 2 : (1 + EasingOutFunction(2 * t - 1)) / 2;
    };
}
