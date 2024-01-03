namespace Monogame.Core.Tweening.Interpolations;

class Quart : EasingInterpolation
{
    protected override Func<double, double> EasingInFunction => (t) => { return Math.Pow(t, 4); };
    protected override Func<double, double> EasingOutFunction => (t) => { return 1 - Math.Pow(1 - t, 4); };
    protected override Func<double, double> EasingInOutFunction => (t) => 
    { 
        return t < 0.5 
            ? 8 * Math.Pow(t, 4) 
            : 1 - Math.Pow(-2 * t + 2, 4) / 2; 
    };
}
