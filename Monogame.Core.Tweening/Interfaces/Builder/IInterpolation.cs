namespace Monogame.Core.Tweening;

public interface IInterpolation<TTween>
{
    ILoop<TTween>Linear();
    IEase<TTween>Sine();
    IEase<TTween>Quadratic();
    IEase<TTween>Cubic();
    IEase<TTween>Quart();
    IEase<TTween>Quint();
    IEase<TTween>Expo();
    IEase<TTween>Circular();
    IEase<TTween>Back(double intensity = 1);
    IEase<TTween>Elastic(double intensity = 1);
    IEase<TTween>Bounce(double intensity = 1);
}
