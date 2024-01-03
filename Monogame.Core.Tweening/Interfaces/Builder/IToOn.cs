namespace Monogame.Core.Tweening;

public interface IToOn<TIn, TTween>
{
    IForOn<TIn, TTween> To(TIn endValue);
}
