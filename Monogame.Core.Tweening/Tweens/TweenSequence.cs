using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monogame.Core.Tweening.Structs;
using MonoGame.Core.Graphics.Components;

namespace Monogame.Core.Tweening.Tweens;

class TweenSequence<TIn> : TweenBase<TIn, ITweenSequence>,
    ITweenSequence,
    ISequenceFrom<TIn, ITweenSequence>,
    ISequenceTo<TIn, ITweenSequence>,
    ISequenceToOn<TIn, ITweenSequence>,
    ISequenceFor<TIn, ITweenSequence>,
    ISequenceForOn<TIn, ITweenSequence>
{
    private ITween CurrentTween => _tweens[_currentStepIndex];
    private bool OnLastStep => _currentStepIndex == _tweens.Count - 1;

    private int _currentStepIndex;
    private double[] _totalDurations;
    private List<ITween> _tweens = new List<ITween>();
    private List<TweenValue> _values = new List<TweenValue>();

    public override void Start()
    {
        IsStarted = true;
        CurrentTween.Start();
    }

    public override void Reset(bool resetLoops)
    {
        InvokeEvent = true;
        _currentStepIndex = 0;
        _tweens.ForEach(t => t.Reset(resetLoops));
    }

    public override void Reverse()
    {
        IsReversed = !IsReversed;
        var newTweens = new List<ITween>();
        for (int i = _tweens.Count - 1; i >= 0; i--)
        {
            _tweens[i].Reverse();
            newTweens.Add(_tweens[i]);
        }
        _tweens = newTweens;
        _currentStepIndex = _tweens.Count - 1 - _currentStepIndex;
    }

    public override void Pause()
    {
        IsStarted = false;
        CurrentTween.Pause();
    }

    public override void ToggleState()
    {
        IsStarted = !IsStarted;
        CurrentTween.ToggleState();
    }

    public ISequenceToOn<TIn, ITweenSequence> From(TIn startValue)
    {
        _values.Add((dynamic)startValue!);
        return this;
    }

    public ISequenceTo<TIn, ITweenSequence> OnPosition(Drawable target)
    {
        OutputAction = (value) => { target.Position = value; };
        return this;
    }

    public ISequenceTo<TIn, ITweenSequence> OnScale(Drawable target)
    {
        OutputAction = (value) => { target.Scale = value; };
        return this;
    }

    public ISequenceTo<TIn, ITweenSequence> OnRotation(Drawable target)
    {
        OutputAction = (value) => { target.Rotation = value; };
        return this;
    }

    public ISequenceTo<TIn, ITweenSequence> OnOpacity(Drawable target)
    {
        OutputAction = (value) => { target.Opacity = value; };
        return this;
    }

    public ISequenceFor<TIn, ITweenSequence> To(params TIn[] endValues)
    {
        foreach(var value in endValues) _values.Add((dynamic)value!);
        return this;
    }

    public ISequenceFor<TIn, ITweenSequence> To(ICollection<TIn> endValues)
    {
        foreach(var value in endValues) _values.Add((dynamic)value!);
        return this;
    }

    ISequenceForOn<TIn, ITweenSequence> ISequenceToOn<TIn, ITweenSequence>.To(params TIn[] endValues)
    {
        foreach(var value in endValues) _values.Add((dynamic)value!);
        return this;
    }

    ISequenceForOn<TIn, ITweenSequence> ISequenceToOn<TIn, ITweenSequence>.To(ICollection<TIn> endValues)
    {
        foreach(var value in endValues) _values.Add((dynamic)value!);
        return this;
    }

    public ISequenceFor<TIn, ITweenSequence> On(Action<TIn> action)
    {
        OutputAction = (value) => action.Invoke((TIn)(dynamic)value);
        return this;
    }

    public IInterpolation<ITweenSequence> For(double milliseconds)
    {
        if (milliseconds <= 0) throw new ArgumentException("Duration must be > 0");
        _totalDurations = new double[_values.Count];
        for (int i = 0; i < _values.Count; i++) _totalDurations[i] = milliseconds;
        return this;
    }

    public IInterpolation<ITweenSequence> For(params double[] millisecondsArray)
    {
        if (millisecondsArray.Any(d => d <= 0)) throw new ArgumentException("Durations must be > 0");
        else if (_values.Count - 1 != millisecondsArray.Length)
            throw new ArgumentException("The number of durations doesn't match the number of sequences (3 values = 2 sequences)");
        _totalDurations = millisecondsArray.ToArray();
        return this;
    }

    public IInterpolation<ITweenSequence> For(ICollection<double> millisecondsCollection)
    {
        return For(millisecondsCollection.ToArray());
    }

    public override ITweenSequence Build()
    {
        if (_values.Count < 2)
            throw new ArgumentException("Not enough values for the tween sequence");
        
        for (int i = 0; i < _values.Count - 1; i++)
        {
            var tween = new Tween<TIn>(this.OutputAction);
            tween.From((TIn)(dynamic)_values[i]);
            tween.To((TIn)(dynamic)_values[i+1]);
            tween.For(_totalDurations[i]);
            tween.Interpolation = this.Interpolation;
            tween.AnimationEnded += (t) =>
            {
                if (OnLastStep) this.OnAnimationEnded();
                else
                {
                    _currentStepIndex++;
                    CurrentTween.Start();
                }
            };
            _tweens.Add(tween);
        }
        IsBuilded = true;

        return this;
    }

    public void ChangeDurations(double milliseconds)
    {
        For(milliseconds);
    }

    public void ChangeDurations(params double[] millisecondsArray)
    {
        For(millisecondsArray);
    }

    public void ChangeDurations(ICollection<double> millisecondsCollection)
    {
        For(millisecondsCollection);
    }

    public override TweenValue Update(double currentTime)
    {
        return CurrentTween.Update(currentTime);
    }

    protected override void OnAnimationEnded() => InvokeAnimationEnded(this);
}