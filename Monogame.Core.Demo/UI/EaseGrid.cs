using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Core.Graphics.Components;
using MonoGame.Core.Graphics.Origins;
using MonoGame.Core.Extensions;
using Monogame.Core.Graphics.Display;
using Monogame.Core.Tweening.Demo.Models;

namespace Monogame.Core.Tweening.UI;

class EaseGrid : Drawable
{
    private int Padding => (int)((double)Size.X * 16 / 780);
    
    private readonly Rectangle _rect;
    private readonly Color _backgroundColor;
    private readonly Color _lineColor;
    private readonly Rectangle[] _linesRectangles;
    private readonly TextComponent[] _easeTexts;
    private readonly SpriteFont _font;

    public EaseGrid(DisplayManager displayManager, Point position, Point size, int drawOrder) : base(displayManager, position, drawOrder)
    {
        if (size.X < 100 || size.Y < 100)
            throw new ArgumentException("Size must be minimum 100x100");
        Size = size;

        _rect = new Rectangle(0, 0, Size.X, Size.Y);
        _backgroundColor = new Color().FromHex("#0C0C0C");
        _lineColor =  new Color().FromHex("#272727");
        _font = Content.Load<SpriteFont>("Fonts/Roboto-Light-78");

        //Lines
        int bottomPadding = (int)(Size.Y * 0.16);
        int lineSpacing = (Size.Y - bottomPadding) / 3;
        var lineWidth = Size.X - (Padding * 2);
        _linesRectangles = new Rectangle[3]
        {
            new Rectangle(Padding, lineSpacing, lineWidth, 2), 
            new Rectangle(Padding, lineSpacing * 2, lineWidth, 2),
            new Rectangle(Padding, lineSpacing * 3, lineWidth, 2)
        };

        //Texts
        var textColor = new Color().FromHex("#787878");
        var horizontalBase = Size.X - Padding;
        _easeTexts = new TextComponent[3]
        {
            new TextComponent("Ease In", new Vector2(horizontalBase - _font.MeasureString("Ease In").X, _linesRectangles[0].Y + 8), textColor), 
            new TextComponent("Ease Out", new Vector2(horizontalBase - _font.MeasureString("Ease Out").X, _linesRectangles[1].Y + 8), textColor),
            new TextComponent("Ease In Out", new Vector2(horizontalBase - _font.MeasureString("Ease In Out").X, _linesRectangles[2].Y + 8), textColor)
        };
    }

    public override void Draw(GameTime gameTime)
    {
        Graphics.DrawRectangle(this, _backgroundColor, _rect, gameTime);
        _linesRectangles.ToList().ForEach(lr => Graphics.DrawRectangle(this, _lineColor, lr, gameTime));
        _easeTexts.ToList().ForEach(t => Graphics.DrawString(this, _font, t.Text, t.Position, t.Color, gameTime));
    }
}
