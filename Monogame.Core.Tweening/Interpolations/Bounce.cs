namespace Monogame.Core.Tweening.Interpolations;

class Bounce : IntensityEasingInterpolation
{
    private double _n1, _d1;

    public Bounce(double intensity)
    {
        SetIntensity(intensity);

        _n1 = 7.5625;
        _d1 = 2.75;
    }

    protected override Func<double, double> EasingInFunction => (t) => { return 1 - EasingOutFunction(1 - t); };
    protected override Func<double, double> EasingOutFunction => (t) =>
    {
        if (t < 1 / _d1)
            return _n1 * t * t;
        else if (t < 2 / _d1)
            return _n1 * (t -= 1.5 / _d1) * t + 0.75;
        else if (t < 2.5 / _d1)
            return _n1 * (t -= 2.25 / _d1) * t + 0.9375;
        else
            return _n1 * (t -= 2.625 / _d1) * t + 0.984375;
    };
    protected override Func<double, double> EasingInOutFunction => (t) =>
    {
        return t < 0.5
            ? (1 - EasingOutFunction(1 - 2 * t)) / 2
            : (1 + EasingOutFunction(2 * t - 1)) / 2;
    };
}
