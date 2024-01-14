using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monogame.Core.Graphics.Components;
using Monogame.Core.Graphics.Display;
using Monogame.Core.Tweening;
using Monogame.Core.Tweening.Builder;
using Monogame.Core.Windows.Anchors;
using MonoGame.Core.Extensions;
using MonoGame.Core.Graphics.Origins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame.Core.Demo.UI;

class SimpleSequenceMenu : CheckBox
{
    protected override Rectangle BoundingBox => _boundingBox;

    private Color _fontColor;
    private Color _fontColorChecked = Color.White;
    private Color _fontColorUnchecked = new Color().FromHex("#A6A6A6");
    private string _text;
    private Vector2 _textPosition;
    private SpriteFont _font;
    private SpriteFont _fontChecked;
    private SpriteFont _fontUnchecked;
    private ITween _tweenOpacity;
    private ITween _tweenScale;
    private Rectangle _boundingBox;

    public SimpleSequenceMenu(DisplayManager displayManager, string text, Point position, int drawOrder) : base(displayManager, position, drawOrder)
    {
        _text = text;
        _fontChecked = Content.Load<SpriteFont>("Fonts/Roboto-Bold-155");
        _fontUnchecked = Content.Load<SpriteFont>("Fonts/Roboto-Light-155");

        Vector2 size = _fontChecked.MeasureString(text);
        Size = new Point((int)size.X, (int)size.Y);
        Opacity = 0.8f;
        Origin = new MiddleCenterOrigin(Size);
        ScaleOrigin = new MiddleCenterOrigin(Size);
        Scale = new Vector2(0.66f, 0.66f);
        _boundingBox = new Rectangle(0, 0, Size.X, Size.Y);

        _tweenScale = TweenBuilder.From(new Vector2(0.66f, 0.66f)).To(new Vector2(1f, 1f)).For(300f).Back().EaseOut().Build();

        CheckChanged += (check) =>
        {
            if (check.Checked)
            {
                _font = _fontChecked;
                _fontColor = _fontColorChecked;
                _tweenScale.GoForward();
            }
            else
            {
                _font = _fontUnchecked;
                _fontColor = _fontColorUnchecked;
                _tweenScale.GoBackward();
            }
        };

        _tweenOpacity = TweenBuilder.From(0.8f).To(1f).For(300f).Quart().EaseOut().Build();
        MouseEnter += (button) => _tweenOpacity.GoForward();
        MouseLeave += (button) => _tweenOpacity.GoBackward();
    }

    public override void Draw(GameTime gameTime)
    {
        Scale = _tweenScale.Update(gameTime);
        Opacity = _tweenOpacity.Update(gameTime);
        Graphics.DrawString(this, _font, _text, new Vector2(0, 0), _fontColor, gameTime);
        base.Draw(gameTime);
    }
}
