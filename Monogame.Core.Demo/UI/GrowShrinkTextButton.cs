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

class GrowShrinkTextButton : GrowShrinkButton
{
    protected override Rectangle BoundingBox => _boundingBox;

    private string _text;
    private SpriteFont _font;
    private Rectangle _boundingBox;
    private Color _color;

    public GrowShrinkTextButton(DisplayManager displayManager, string text, string fontName, Color color, Point position, int drawOrder) : 
        base(displayManager, position, drawOrder)
    {
        _text = text;
        _font = Content.Load<SpriteFont>(fontName);
        Vector2 size = _font.MeasureString(_text);
        Size = new Point((int)size.X, (int)size.Y);
        _boundingBox = new Rectangle(0, 0, Size.X, Size.Y);
        _color = color;

        Position += new Point(Size.X / 2, Size.Y / 2);
        Origin = new MiddleCenterOrigin(Size);
        ScaleOrigin = new MiddleCenterOrigin(Size);
    }


    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
        Graphics.DrawString(this, _font, _text, Vector2.Zero, _color, gameTime);
    }
}
