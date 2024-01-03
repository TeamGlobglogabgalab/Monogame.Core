﻿namespace Monogame.Core.Tweening.Interpolations;

class Quadratic : EasingInterpolation
{
    protected override Func<double, double> EasingInFunction => (t) => { return Math.Pow(t, 2); };
    protected override Func<double, double> EasingOutFunction => (t) => { return 1 - Math.Pow(1 - t, 2); };
    protected override Func<double, double> EasingInOutFunction => (t) => 
    { 
        return t < 0.5 
            ? 2 * Math.Pow(t, 2) 
            : 1 - Math.Pow(-2 * t + 2, 2) / 2; 
    };
}
