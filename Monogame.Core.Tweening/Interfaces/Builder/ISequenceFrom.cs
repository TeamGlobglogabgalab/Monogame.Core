using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Core.Graphics.Components;

namespace Monogame.Core.Tweening;

public interface ISequenceFrom<TIn, TTween>
{
    ISequenceToOn<TIn, TTween> From(TIn startValue);
    ISequenceTo<TIn, TTween> OnPosition(Drawable target);
    ISequenceTo<TIn, TTween> OnScale(Drawable target);
    ISequenceTo<TIn, TTween> OnRotation(Drawable target);
    ISequenceTo<TIn, TTween> OnOpacity(Drawable target);
}
