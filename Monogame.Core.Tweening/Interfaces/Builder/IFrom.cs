using MonoGame.Core.Graphics.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame.Core.Tweening;

public interface IFrom<TIn, TTween>
{
    IToOn<TIn, TTween> From(TIn startValue);
    ITo<TIn, TTween> OnPosition(Drawable target);
    ITo<TIn, TTween> OnScale(Drawable target);
    ITo<TIn, TTween> OnRotation(Drawable target);
    ITo<TIn, TTween> OnOpacity(Drawable target);
}
