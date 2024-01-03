namespace Monogame.Core.Tweening;

public interface IEase<TTween>
{
    ILoop<TTween> EaseIn();
    ILoop<TTween> EaseOut();
    ILoop<TTween> EaseInOut();
}