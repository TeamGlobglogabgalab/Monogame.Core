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

public class TextButton : Button
{
    protected override Rectangle BoundingBox => _boundingBox;

    private string _text;
    private SpriteFont _font;
    private Rectangle _boundingBox;
    private Color _color;
    private Color _colorHovered;
    private Color _currentColor;
    private ITween _tween;
    private ITween _tweenColor;

    public TextButton(DisplayManager displayManager, MouseComponent mouseComponent, string text, string fontName, Point position, int drawOrder) : 
        base(displayManager, mouseComponent, position, drawOrder)
    {
        _text = text;
        _font = Content.Load<SpriteFont>(fontName);
        Vector2 size = _font.MeasureString(_text);
        Size = new Point((int)size.X, (int)size.Y);
        _boundingBox = new Rectangle(0, 0, (int)size.X, (int)size.Y);
        _color = Color.White;
        _colorHovered = Color.Purple;
        _currentColor = Color.White;

        Origin = new MiddleCenterOrigin(Size);
        ScaleOrigin = new MiddleCenterOrigin(Size);
        Rotation = 0;

        _tween = TweenBuilder.OnScale(this).To(new Vector2(0.8f, 0.8f)).For(300f).Back(2).EaseOut().Build();
        _tweenColor = TweenBuilder.From(_color).To(_colorHovered).For(150).Linear().Build();
        MouseEnter += (button) => { _tweenColor.GoForward(); };
        MouseLeave += (button) => { _tweenColor.GoBackward(); };
        ButtonClicked += (button) => { _tween.GoForward(); };
        ButtonReleased += (button) => { _tween.GoBackward(); };
    }


    public override void Draw(GameTime gameTime)
    {
        _tween.Update(gameTime);
        _currentColor = _tweenColor.Update(gameTime);

        base.Draw(gameTime);
        Graphics.DrawString(this, _font, _text, Vector2.Zero, _currentColor, gameTime);
    }
}
