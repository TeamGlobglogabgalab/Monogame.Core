using Microsoft.Xna.Framework;
using System;

namespace Monogame.Core.Tweening.Demo.Models;

public class TextComponent
{
    public string Text { get; set; }
    public Vector2 Position { get; set; }
    public Color Color { get; set; }
    public float Scale { get; set; }

    public TextComponent(string text, Vector2 position, Color color, float scale)
    {
        if (scale <= 0 || scale > 1) throw new ArgumentOutOfRangeException("Scale must in ]0;1]");

        Text = text;
        Position = position;
        Color = color;
        Scale = scale;
    }
}
