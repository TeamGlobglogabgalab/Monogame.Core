using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoGame.Core.Graphics.Components;
using Monogame.Core.Tweening.Structs;
using static System.Collections.Specialized.BitVector32;

namespace Monogame.Core.Tweening.Tweens;

class Tween<TIn> : TweenBase<TIn, ITween>,
    ITween,
    IFrom<TIn, ITween>,
    ITo<TIn, ITween>,
    IToOn<TIn, ITween>,
    IFor<ITween>,
    IForOn<TIn, ITween>
{
    private TweenValue _startValue;
    private TweenValue _endValue;
    private double _totalDuration;
    private double _currentDuration;

    public Tween() 
    {
    }

    public Tween(Action<TweenValue> outputAction)
    {
        OutputAction = outputAction;
    }

    public override void Start()
    {
        IsStarted = true;
    }

    public override void Reset(bool resetLoops)
    {
        InvokeEvent = true;
        _currentDuration = 0;
        if(resetLoops) Loops = LoopsCount;
    }

    public override void Reverse()
    {
        TweenValue bufferValue = _startValue;
        _startValue = _endValue;
        _endValue = bufferValue;
        InvokeEvent = true;

        IsReversed = !IsReversed;
        if(_currentDuration == 0 || _currentDuration >= _totalDuration)
            _currentDuration = 0;
        else
        {
            var result = Interpolation.Interpolate(_endValue, _startValue, _totalDuration, _currentDuration);
            _currentDuration = Interpolation.Extrapolate(_startValue, _endValue, _totalDuration, _currentDuration, new TweenValue(result));
        }
    }

    public override void Pause()
    {
        IsStarted = false;
    }

    public void ChangeDuration(double milliseconds)
    {
        For(milliseconds);
    }

    public override void ToggleState()
    {
        IsStarted = !IsStarted;
    }
    public IToOn<TIn, ITween> From(TIn startValue)
    {
        _startValue = (dynamic)startValue!;
        return this;
    }

    public ITo<TIn, ITween> OnPosition(Drawable target)
    {
        _startValue = target.Position;
        OutputAction = (value) => { target.Position = value; };
        return this;
    }

    public ITo<TIn, ITween> OnScale(Drawable target)
    {
        _startValue = target.Scale;
        OutputAction = (value) => { target.Scale = value; };
        return this;
    }

    public ITo<TIn, ITween> OnRotation(Drawable target)
    {
        _startValue = target.Rotation;
        OutputAction = (value) => { target.Rotation = value; };
        return this;
    }

    public ITo<TIn, ITween> OnOpacity(Drawable target)
    {
        _startValue = target.Opacity;
        OutputAction = (value) => { target.Opacity = value; };
        return this;
    }

    public IFor<ITween> To(TIn endValue)
    {
        _endValue = (dynamic)endValue!;
        return this;
    }

    IForOn<TIn, ITween> IToOn<TIn, ITween>.To(TIn endValue)
    {
        _endValue = (dynamic)endValue!;
        return this;
    }

    public IFor<ITween> On(Action<TIn> action)
    {
        OutputAction = (value) => action.Invoke((TIn)(dynamic)value);
        return this;
    }

    public IInterpolation<ITween> For(double milliseconds)
    {
        if (milliseconds <= 0) throw new ArgumentException("Duration must be > 0");
        _totalDuration = milliseconds;
        return this;
    }

    public override ITween Build()
    {
        _currentDuration = 0;
        IsBuilded = true;
        return this;
    }

    public override TweenValue Update(double elapsedTimeMs)
    {
        var updatedTime = UpdateTime(elapsedTimeMs);
        var result = Interpolation.Interpolate(_startValue, _endValue, _totalDuration, updatedTime);
        OutputAction?.Invoke(result);
        return result;
    }

    protected override void OnAnimationEnded() => InvokeAnimationEnded(this);

    protected double UpdateTime(double elapsedTimeMs, bool triggerAnimationEnded = true)
    {
        if (!IsStarted) return _currentDuration;
        _currentDuration += elapsedTimeMs;
        if (_currentDuration <= _totalDuration)
            return _currentDuration;

        //Animation ended
        IsStarted = false;
        _currentDuration = _totalDuration;

        if (triggerAnimationEnded) OnAnimationEnded();
        _currentDuration += elapsedTimeMs;
        return _currentDuration;
    }

    public void Change<T>(T from, T to, Action<T> on)
    {
        From((TIn)(dynamic)from!);
        To((TIn)(dynamic)to!);
        OutputAction = (value) => on.Invoke((T)(dynamic)value);
        Reset(true);
    }
}
