using Monogame.Core.Tweening.Enums;
using Monogame.Core.Tweening.Structs;
using Monogame.Core.Tweening.Tweens;

namespace Monogame.Core.Tweening.Interpolations;

abstract class EasingInterpolation : Interpolation
{
    public Ease EasingType { get; set; }
    protected abstract Func<double, double> EasingInFunction { get; }
    protected abstract Func<double, double> EasingOutFunction { get; }
    protected abstract Func<double, double> EasingInOutFunction { get; }

    public override TweenValue Interpolate(TweenValue startValue, TweenValue endValue, double totalDuration, double currentDuration)
    {
        switch (EasingType)
        {
            case Ease.EaseIn:
                return Interpolate(EasingInFunction, startValue, endValue, totalDuration, currentDuration);
            case Ease.EaseOut:
                return Interpolate(EasingOutFunction, startValue, endValue, totalDuration, currentDuration);
            case Ease.EaseInOut:
                return Interpolate(EasingInOutFunction, startValue, endValue, totalDuration, currentDuration);
            default:
                throw new Exception("Easing type invalid or null");
        }
    }

    public override double Extrapolate(TweenValue startValue, TweenValue endValue, double totalDuration, double currentDuration, TweenValue interpolatedValue)
    {
        switch (EasingType)
        {
            case Ease.EaseIn:
                return Extrapolate(EasingInFunction, startValue, endValue, totalDuration, currentDuration, interpolatedValue);
            case Ease.EaseOut:
                return Extrapolate(EasingOutFunction, startValue, endValue, totalDuration, currentDuration, interpolatedValue);
            case Ease.EaseInOut:
                return totalDuration - currentDuration;
            default:
                throw new Exception("Easing type invalid or null");
        }
    }

    private TweenValue Interpolate(Func<double, double> EasingFunction, TweenValue startValue, TweenValue endValue, double totalDuration, double currentDuration)
    {
        double t = currentDuration / totalDuration;
        double tweenValue = EasingFunction(t);

        ResultsBuffer ??= new decimal[startValue.Values.Length];
        for (int i = 0; i < startValue.Values.Length; i++)
            ResultsBuffer[i] = startValue.Values[i] + (endValue.Values[i] - startValue.Values[i]) * Convert.ToDecimal(tweenValue);
        return new TweenValue(ResultsBuffer);
    }

    private double Extrapolate(Func<double, double> EasingFunction, TweenValue startValue, TweenValue endValue, double totalDuration, double currentDuration, TweenValue interpolatedValue)
    {
        var step = Math.Max(1, (int)totalDuration/100);
        var bestResult = new Tuple<TweenValue, double>(startValue, 0);
        for(int d = 0 ; d < totalDuration ; d += step)
        {
            var result = Interpolate(EasingFunction, startValue, endValue, totalDuration, d);
            if(EvaluateExtrapolatedResult(result, interpolatedValue, bestResult))
                bestResult = new Tuple<TweenValue, double>(new TweenValue(result), d);
        }
        return bestResult.Item2;
    }

    private bool EvaluateExtrapolatedResult(TweenValue result, TweenValue interpolatedValue, Tuple<TweenValue, double> bestResult)
    {
        decimal bestResultDelta = 0;
        for(int i=0 ; i < bestResult.Item1.Values.Length ; i++)
            bestResultDelta += Math.Abs(bestResult.Item1.Values[i] - interpolatedValue.Values[i]);

        decimal currentResultDelta = 0;
        for(int i=0 ; i < result.Values.Length ; i++)
            currentResultDelta += Math.Abs(result.Values[i] - interpolatedValue.Values[i]);

        return currentResultDelta < bestResultDelta;
    }
}
