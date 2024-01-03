namespace Monogame.Core.Tweening;

public interface IForOn<TIn, TTween>
{
    IFor<TTween> On(Action<TIn> action);
    IInterpolation<TTween> For(double milliseconds);
}
