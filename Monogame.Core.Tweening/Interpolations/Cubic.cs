namespace Monogame.Core.Tweening.Interpolations;

class Cubic : EasingInterpolation
{
    protected override Func<double, double> EasingInFunction => (t) => { return Math.Pow(t, 3); };
    protected override Func<double, double> EasingOutFunction => (t) => { return 1 - Math.Pow(1 - t, 3); };
    protected override Func<double, double> EasingInOutFunction => (t) => 
    { 
        return t < 0.5 
            ? 4 * Math.Pow(t, 3) 
            : 1 - Math.Pow(-2 * t + 2, 3) / 2; 
    };
}
