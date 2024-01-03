using Monogame.Core.Tweening.Structs;

namespace Monogame.Core.Tweening.Interpolations;

abstract class Interpolation
{
    protected decimal[]? ResultsBuffer;

    public abstract TweenValue Interpolate(TweenValue startValue, TweenValue endValue, double totalDuration, double currentDuration);
    public abstract double Extrapolate(TweenValue startValue, TweenValue endValue, double totalDuration, double currentDuration, TweenValue interpolatedValue);
}
