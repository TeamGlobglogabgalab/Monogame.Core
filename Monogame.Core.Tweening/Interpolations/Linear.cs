using Monogame.Core.Tweening.Structs;
using Monogame.Core.Tweening.Tweens;

namespace Monogame.Core.Tweening.Interpolations;

class Linear : Interpolation
{
    public override TweenValue Interpolate(TweenValue startValue, TweenValue endValue, double totalDuration, double currentDuration)
    {
        ResultsBuffer ??= new decimal[startValue.Values.Length];
        for (int i = 0; i < startValue.Values.Length; i++)
            ResultsBuffer[i] = (endValue.Values[i] - startValue.Values[i]) * Convert.ToDecimal(currentDuration / totalDuration) + startValue.Values[i];
        return new TweenValue(ResultsBuffer);
    }

    public override double Extrapolate(TweenValue startValue, TweenValue endValue, double totalDuration, double currentDuration, TweenValue interpolatedValue)
    {
        return totalDuration - currentDuration;
    }
}
