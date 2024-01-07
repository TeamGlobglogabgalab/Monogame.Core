using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Core.Graphics.Components;
using MonoGame.Core.Graphics.Origins;
using Monogame.Core.Graphics.Display;
using MonoGame.Core.Windows.Input;
using Monogame.Core.Tweening.Builder;

namespace Monogame.Core.Tweening.UI;

abstract class GrowShrinkButton : Button
{
    private ITween _tweenOpacity;
    private ITween _tweenScale;

    public GrowShrinkButton(DisplayManager displayManager, Point position, int drawOrder) : 
        base(displayManager, position, drawOrder)
    {
        Rotation = 0;
        Opacity = 0.8f;

        _tweenOpacity = TweenBuilder.OnOpacity(this).To(1).For(300f).Quart().EaseOut().Build();
        _tweenScale = TweenBuilder.OnScale(this).To(new Vector2(1.1f, 1.1f)).For(300f).Back().EaseOut().Build();
        
        MouseEnter += (button) => 
        {
            _tweenOpacity.GoForward();
            _tweenScale.Change(Scale, new Vector2(1.1f, 1.1f));
            _tweenScale.GoForward();
        };
        MouseLeave += (button) =>
        {
            _tweenOpacity.GoBackward();
            if (!MouseComponent.LeftDown)
            {
                _tweenScale.Change(Scale, new Vector2(1f, 1f));
                _tweenScale.GoForward();
            }
        };
        ButtonClicked += (button) => 
        {
            _tweenScale.Change(Scale, new Vector2(0.8f, 0.8f));
            _tweenScale.GoForward(); 
        };
        ButtonReleased += (button) => 
        {
            _tweenScale.Change(Scale, IsHovered ? new Vector2(1.1f, 1.1f) : new Vector2(1f, 1f));
            _tweenScale.GoForward();
        };
    }


    public override void Draw(GameTime gameTime)
    {
        Opacity = _tweenOpacity.Update(gameTime);
        Scale = _tweenScale.Update(gameTime);
        base.Draw(gameTime);
    }
}
