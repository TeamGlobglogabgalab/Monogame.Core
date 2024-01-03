namespace Monogame.Core.Tweening.Interpolations;

class Circular : EasingInterpolation
{
    protected override Func<double, double> EasingInFunction => (t) => { return 1 - Math.Sqrt(1 - Math.Pow(t, 2)); };
    protected override Func<double, double> EasingOutFunction => (t) => { return Math.Sqrt(1 - Math.Pow(t - 1, 2)); };
    protected override Func<double, double> EasingInOutFunction => (t) =>
    {
        return t < 0.5
            ? (1 - Math.Sqrt(1 - Math.Pow(2 * t, 2))) / 2
            : (Math.Sqrt(1 - Math.Pow(-2 * t + 2, 2)) + 1) / 2;
    };
}
