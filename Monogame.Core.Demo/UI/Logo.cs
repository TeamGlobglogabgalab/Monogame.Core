using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monogame.Core.Graphics.Display;
using Monogame.Core.Tweening.UI;
using MonoGame.Core.Graphics.Origins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame.Core.Demo.UI;

class Logo : GrowShrinkButton
{
    protected override Rectangle BoundingBox => _boundingBox;

    private const string _textA = "TWEENING";
    private const string _textB = "Monogame.Core";
    private const float _lineSpacing = 5f;

    private Rectangle _boundingBox;
    private SpriteFont _fontA;
    private SpriteFont _fontB;
    private int _textAHeight;
    
    public Logo(DisplayManager displayManager, Point position, int drawOrder) : base(displayManager, position, drawOrder)
    {
        _fontA = Content.Load<SpriteFont>("Fonts/Roboto-Black-75");
        _fontB = Content.Load<SpriteFont>("Fonts/Roboto-Medium-50");
        Vector2 size = _fontA.MeasureString(_textA);
        size += new Vector2(0, _lineSpacing);
        _textAHeight = (int)size.Y;
        size += new Vector2(0, _fontB.MeasureString(_textB).Y);
        Size = new Point((int)size.X, (int)size.Y);
        _boundingBox = new Rectangle(0, 0, Size.X, Size.Y);

        Position += new Point(Size.X / 2, Size.Y / 2);
        Origin = new MiddleCenterOrigin(Size);
        ScaleOrigin = new MiddleCenterOrigin(Size);
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
        Graphics.DrawString(this, _fontA, _textA, Vector2.Zero, Demo.PrimaryColor, gameTime);
        Graphics.DrawString(this, _fontB, _textB, new Vector2(0, _textAHeight), Demo.PrimaryColor, gameTime);
    }
}
