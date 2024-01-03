namespace Monogame.Core.Tweening;

public interface ISequenceToOn<TIn, TTween>
{
    ISequenceForOn<TIn, TTween> To(params TIn[] endValues);
    ISequenceForOn<TIn, TTween> To(ICollection<TIn> endValues);
}
