using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monogame.Core.Windows;
using MonoGame.Core.Graphics.Components;

namespace Monogame.Core.Graphics;

public interface IGraphicsRenderer
{
    public IScalableContainer ScalableContainer { set; }
    public void Resume();
    public void Suspend();
    public void Draw(Drawable drawable, Texture2D texture, Rectangle rect, GameTime gameTime, Effect effect = null, bool forceBasicTransform = false);
    public void DrawRectangle(Drawable drawable, Color color, Rectangle rect, GameTime gameTime, Effect effect = null);
    public void DrawString(Drawable drawable, SpriteFont font, string text, Vector2 position, Color color, GameTime gameTime, Effect effect = null);
    public void DrawOval(Drawable drawable, Texture2D texture, Rectangle rect, GameTime gameTime);
    public void DrawOval(Drawable drawable, Color color, Rectangle rect, GameTime gameTime);
    public void DrawTriangle(Drawable drawable, Color color, Vector2 a, Vector2 b, Vector2 c, GameTime gameTime);
    public void DrawTriangle(Drawable drawable, Texture2D texture, Vector2 a, Vector2 b, Vector2 c, GameTime gameTime);
}
