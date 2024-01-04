using System;
using Microsoft.Xna.Framework;
using MonoGame.Core.Extensions;
using MonoGame.Core.Graphics.Components;
using Monogame.Core.Graphics.Display;

namespace Monogame.Core.Tweening.UI;

public class TweenCube : Drawable
{
    public const int DefaultCubeSize = 50;
    
    private Rectangle _rect;
    private Color _color;

    public TweenCube(DisplayManager displayManager, Point position, int drawOrder, string hexColor) : 
        this(displayManager, position, DefaultCubeSize, drawOrder, hexColor)
    {
    }

    public TweenCube(DisplayManager displayManager, Point position, int size, int drawOrder, string hexColor) : base(displayManager, position, drawOrder)
    {
        _rect = new Rectangle(0, 0, size, size);
        _color = new Color().FromHex(hexColor);
        Scale = new Vector2(1, 2);
        Rotation = 36;
    }

    public override void Draw(GameTime gameTime)
    {
        //Graphics.DrawRectangle(this, _color, _rect, gameTime);
        Graphics.DrawTriangle(this, _color, new Vector2(0, 0), new Vector2(50, 0), new Vector2(25, 50), gameTime);
    }
}
