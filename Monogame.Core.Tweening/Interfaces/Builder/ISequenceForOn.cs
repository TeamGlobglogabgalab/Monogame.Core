namespace Monogame.Core.Tweening;

public interface ISequenceForOn<TIn, TTween>
{
    ISequenceFor<TIn, TTween> On(Action<TIn> action);
    IInterpolation<TTween> For(double milliseconds);
    IInterpolation<TTween> For(params double[] millisecondsArray);
    IInterpolation<TTween> For(ICollection<double> millisecondsCollection);
}