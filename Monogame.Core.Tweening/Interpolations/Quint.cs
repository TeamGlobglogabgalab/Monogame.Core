namespace Monogame.Core.Tweening.Interpolations;

class Quint : EasingInterpolation
{
    protected override Func<double, double> EasingInFunction => (t) => { return Math.Pow(t, 5); };
    protected override Func<double, double> EasingOutFunction => (t) => { return 1 - Math.Pow(1 - t, 5); };
    protected override Func<double, double> EasingInOutFunction => (t) => 
    { 
        return t < 0.5 
            ? 16 * Math.Pow(t, 5) 
            : 1 - Math.Pow(-2 * t + 2, 5) / 2; 
    };
}
