namespace Monogame.Core.Tweening;

public interface ITo<TIn, TTween>
{
    IFor<TTween> To(TIn endValue);
}
