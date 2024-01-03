using Microsoft.Xna.Framework;
using Monogame.Core.Tweening.Interpolations;
using Monogame.Core.Tweening.Structs;
using Monogame.Core.Tweening.Enums;

namespace Monogame.Core.Tweening.Tweens;

abstract class TweenBase<TIn, TTween> : ITweenBase<TTween>,
    IInterpolation<TTween>,
    IEase<TTween>, 
    ILoop<TTween>,
    IBuild<TTween> where TTween : ITweenBase<TTween>
{
    public event ITweenBase<TTween>.TweenEvent? AnimationEnded;
    public Interpolation Interpolation;
    public bool IsStarted { get; protected set; } = false;
    public bool IsReversed { get; protected set; } = false;

    protected bool IsBuilded = false;
    protected bool InvokeEvent = true;
    protected Action<TweenValue> OutputAction;
    protected long Loops = 0;
    protected long LoopsCount = 0;

    public void GoForward()
    {
        if (IsReversed) Reverse();
        Start();
    }

    public void GoBackward()
    {
        if (!IsReversed) Reverse();
        Start();
    }

    public void Restart()
    {
        Reset();
        Start();
    }

    public void Stop()
    {
        Reset();
        IsStarted = false;
    }

    public ILoop<TTween> Linear()
    {
        Interpolation = new Linear();
        return this;
    }

    public IEase<TTween> Sine()
    {
        Interpolation = new Sine();
        return this;
    }

    public IEase<TTween> Quadratic()
    {
        Interpolation = new Quadratic();
        return this;
    }

    public IEase<TTween> Cubic()
    {
        Interpolation = new Cubic();
        return this;
    }

    public IEase<TTween> Quart()
    {
        Interpolation = new Quart();
        return this;
    }

    public IEase<TTween> Quint()
    {
        Interpolation = new Quint();
        return this;
    }

    public IEase<TTween> Expo()
    {
        Interpolation = new Expo();
        return this;
    }

    public IEase<TTween> Circular()
    {
        Interpolation = new Circular();
        return this;
    }

    public IEase<TTween> Back(double intensity = 1)
    {
        Interpolation = new Back(intensity);
        return this;
    }

    public IEase<TTween> Elastic(double intensity = 1)
    {
        Interpolation = new Elastic(intensity);
        return this;
    }

    public IEase<TTween> Bounce(double intensity = 1)
    {
        Interpolation = new Bounce(intensity);
        return this;
    }

    public ILoop<TTween> EaseIn()
    {
        var easingInterpolation = Interpolation as EasingInterpolation;
        easingInterpolation!.EasingType = Ease.EaseIn;
        return this;
    }

    public ILoop<TTween> EaseOut()
    {
        var easingInterpolation = Interpolation as EasingInterpolation;
        easingInterpolation!.EasingType = Ease.EaseOut;
        return this;
    }
    public ILoop<TTween> EaseInOut()
    {
        var easingInterpolation = Interpolation as EasingInterpolation;
        easingInterpolation!.EasingType = Ease.EaseInOut;
        return this;
    }

    public IBuild<TTween> Loop(uint delay = 0)
    {
        return Loop(delay, 0);
    }

    public IBuild<TTween> Loop(uint delay, uint iterationCount = 0)
    {
        Loops = iterationCount;
        LoopsCount = iterationCount;
        if (delay == 0) 
        { 
            AnimationEnded += (tween) => 
            { 
                if(--Loops > 0 || iterationCount == 0) 
                {
                    tween.Reset(false);
                    tween.Start();
                }
            };
            return this; 
        }

        AnimationEnded += (tween) =>
        {
            new Thread(() =>
            {
                Thread.Sleep((int)delay);
                if(--Loops > 0 || iterationCount == 0)
                {
                    tween.Reset(false);
                    tween.Start();
                }
            }).Start();
        };
        return this;
    }

    public IBuild<TTween> LoopAlternate(uint delay = 0)
    {
        return LoopAlternate(delay, 0);
    }

    public IBuild<TTween> LoopAlternate(uint delay = 0, uint iterationCount = 0)
    {
        Loops = iterationCount;
        LoopsCount = iterationCount;
        if(delay == 0) 
        { 
            AnimationEnded += (tween) => 
            {
                if(--Loops > 0 || iterationCount == 0)
                {
                    tween.Reverse(); 
                    tween.Reset(false);
                    tween.Start();
                } 
            }; 
            return this; 
        }

        AnimationEnded += (tween) => 
        { 
            new Thread(() => 
            { 
                if(--Loops > 0 || iterationCount == 0)
                {
                    Thread.Sleep((int)delay);
                    tween.Reverse();
                    tween.Reset(false);
                    tween.Start();
                } 
            }).Start();
        };
        return this;
    }

    public TweenValue Update(GameTime gameTime)
    {
        return Update(gameTime.ElapsedGameTime.Milliseconds);
    }

    public abstract TTween Build();
    public abstract void Start();
    public abstract void Reset(bool resetLoops = true);
    public abstract void Reverse();
    public abstract void Pause();
    public abstract void ToggleState();
    public abstract TweenValue Update(double elapsedTimeMs);

    protected void InvokeAnimationEnded(TTween tween)
    {
        if (InvokeEvent)
        {
            InvokeEvent = false;
            AnimationEnded?.Invoke(tween);
        }
    }
    protected abstract void OnAnimationEnded();
}