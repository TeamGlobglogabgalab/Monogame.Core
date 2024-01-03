namespace Monogame.Core.Tweening.Interpolations;

class Back : IntensityEasingInterpolation
{
    private double _c1, _c2, _c3;

    public Back(double intensity)
    {
        SetIntensity(intensity);

        _c1 = 1.70158 * Intensity;
        _c2 = _c1 * 1.525;
        _c3 = _c1 + 1;
    }

    protected override Func<double, double> EasingInFunction => (t) => { return _c3 * t * t * t - _c1 * t * t; };
    protected override Func<double, double> EasingOutFunction => (t) => { return 1 + _c3 * Math.Pow(t - 1, 3) + _c1 * Math.Pow(t - 1, 2); };
    protected override Func<double, double> EasingInOutFunction => (t) =>
    {
        return t > 0.5
            ? (Math.Pow(2 * t - 2, 2) * ((_c2 + 1) * (t * 2 - 2) + _c2) + 2) / 2
            :  Math.Pow(2 * t, 2) * ((_c2 + 1) * 2 * t - _c2) / 2;
    };
}
