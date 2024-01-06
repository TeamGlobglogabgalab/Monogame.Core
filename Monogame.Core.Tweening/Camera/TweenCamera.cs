using Microsoft.Xna.Framework;
using Monogame.Core.Tweening.Builder;
using Monogame.Core.Tweening.Enums;
using Monogame.Core.Tweening.Interpolations;
using Monogame.Core.Tweening.Tweens;
using Monogame.Core.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame.Core.Tweening.Camera;

class TweenCamera : ITweenCamera, ICameraFor, ICameraInterpolation, ICameraEase, ICameraBuild
{
    public Point Offset => _offset;

    private Point _offset;
    private Tween<Point> _tween = new Tween<Point>();

    public void GoTo(Point point)
    {
        _tween.Change(_offset, point);
        _tween.Start();
    }

    public void GoTo(int x, int y)
    {
        _tween.Change(_offset, new Point(x, y));
        _tween.Start();
    }

    public void Move(Point point)
    {
        _tween.Change(_offset, new Point(_offset.X + point.X, _offset.Y + point.Y));
        _tween.Start();
    }

    public void Move(int x, int y)
    {
        _tween.Change(_offset, new Point(_offset.X + x, _offset.Y + y));
        _tween.Start();
    }

    public void ChangeDuration(double milliseconds)
    {
        _tween.For(milliseconds);
    }

    public void Update(GameTime gameTime)
    {
        _offset = _tween.Update(gameTime);
    }

    public ICameraInterpolation For(double milliseconds)
    {
        _tween.For(milliseconds);
        return this;
    }

    public ICameraBuild Linear()
    {
        _tween.Interpolation = new Linear();
        return this;
    }

    public ICameraEase Sine()
    {
        _tween.Interpolation = new Sine();
        return this;
    }

    public ICameraEase Quadratic()
    {
        _tween.Interpolation = new Quadratic();
        return this;
    }

    public ICameraEase Cubic()
    {
        _tween.Interpolation = new Cubic();
        return this;
    }

    public ICameraEase Quart()
    {
        _tween.Interpolation = new Quart();
        return this;
    }

    public ICameraEase Quint()
    {
        _tween.Interpolation = new Quint();
        return this;
    }

    public ICameraEase Expo()
    {
        _tween.Interpolation = new Expo();
        return this;
    }

    public ICameraEase Circular()
    {
        _tween.Interpolation = new Circular();
        return this;
    }

    public ICameraEase Back(double intensity = 1)
    {
        _tween.Interpolation = new Back(intensity);
        return this;
    }

    public ICameraEase Elastic(double intensity = 1)
    {
        _tween.Interpolation = new Elastic(intensity);
        return this;
    }

    public ICameraEase Bounce(double intensity = 1)
    {
        _tween.Interpolation = new Bounce(intensity);
        return this;
    }

    public ICameraBuild EaseIn()
    {
        var easingInterpolation = _tween.Interpolation as EasingInterpolation;
        easingInterpolation!.EasingType = Ease.EaseIn;
        return this;
    }

    public ICameraBuild EaseOut()
    {
        var easingInterpolation = _tween.Interpolation as EasingInterpolation;
        easingInterpolation!.EasingType = Ease.EaseOut;
        return this;
    }

    public ICameraBuild EaseInOut()
    {
        var easingInterpolation = _tween.Interpolation as EasingInterpolation;
        easingInterpolation!.EasingType = Ease.EaseInOut;
        return this;
    }

    public ITweenCamera Build()
    {
        _tween.Change(_offset, _offset);
        _offset = new Point(0, 0);
        return this;
    }
}
