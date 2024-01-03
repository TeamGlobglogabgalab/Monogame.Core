namespace Monogame.Core.Tweening;

public interface IFor<TTween>
{
    IInterpolation<TTween> For(double milliseconds);
}