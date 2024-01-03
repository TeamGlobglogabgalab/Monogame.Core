namespace Monogame.Core.Tweening.Interpolations;

class Expo : EasingInterpolation
{
    protected override Func<double, double> EasingInFunction => (t) => { return t == 0 ? 0 : Math.Pow(2, 10 * t - 10); };
    protected override Func<double, double> EasingOutFunction => (t) => { return t == 1 ? 1 : 1 - Math.Pow(2, -10 * t); };
    protected override Func<double, double> EasingInOutFunction => (t) =>
    {
        return t == 0
            ? 0
            : t == 1
            ? 1
            : t < 0.5 ? Math.Pow(2, 20 * t - 10) / 2
            : (2 - Math.Pow(2, -20 * t + 10)) / 2;
    };
}
