using Microsoft.Xna.Framework;
using System;

namespace Monogame.Core.Tweening.Demo.Models;

public class TextComponent
{
    public string Text { get; set; }
    public Vector2 Position { get; set; }
    public Color Color { get; set; }

    public TextComponent(string text, Vector2 position, Color color)
    {
        Text = text;
        Position = position;
        Color = color;
    }
}
