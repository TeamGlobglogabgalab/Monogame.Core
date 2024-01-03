using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monogame.Core.Tools;
using Monogame.Core.Windows.Containers;
using MonoGame.Core.Graphics.Origins;
using Monogame.Core.Windows;
using Monogame.Core.Graphics.Geometry;
using MonoGame.Core.Graphics.Components;

namespace Monogame.Core.Graphics;

public class GraphicsRenderer : IGraphicsRenderer
{
    public IScalableContainer ScalableContainer
    {
        set => _scalableContainer = value;
    }
    
    protected SpriteBatch SpriteBatch;
    protected GraphicsDevice GraphicsDevice;

    private bool _isSuspended;
    private IScalableContainer _scalableContainer;
    private DrawableTransform _transformBuffer = new DrawableTransform();
    private Dictionary<Color, Texture2D> _colorTextures = new Dictionary<Color, Texture2D>();

    public GraphicsRenderer(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, IScalableContainer scalableContainer)
    {
        SpriteBatch = spriteBatch;
        GraphicsDevice = graphicsDevice;
        _isSuspended = true;
        _scalableContainer = scalableContainer;
    }

    public void Resume()
    {
        _isSuspended = false;
    }

    public void Suspend()
    {
        _isSuspended = true;
    }

    public void DrawRectangle(Drawable drawable, Color color, Rectangle rect, GameTime gameTime)
    {
        Texture2D texture;
        if(!_colorTextures.TryGetValue(color, out texture))
        {
            texture = CreateColorTexture(color);
            _colorTextures.Add(color, texture);
        }
        Draw(drawable, texture, rect, gameTime);
    }

    public void Draw(Drawable drawable, Texture2D texture, Rectangle rect, GameTime gameTime)
    {
        VerifySuspendState();
        _transformBuffer.UpdateTransform(drawable, rect, _scalableContainer);
        var color = GetAlphaColor(drawable.Opacity);

        SpriteBatch.Begin();
        SpriteBatch.Draw(texture, _transformBuffer.DestinationRect, _transformBuffer.SourceRect, color, 
            _transformBuffer.RotationRadian, new Vector2(0, 0), SpriteEffects.None, 0f);
        SpriteBatch.End();
    }

    public void DrawString(Drawable drawable, SpriteFont font, string text, Vector2 position, Color color, GameTime gameTime)
    {
        VerifySuspendState();
        _transformBuffer.UpdateTransform(drawable, position, _scalableContainer);
        color = GetAlphaColor(color, drawable.Opacity);

        SpriteBatch.Begin();
        SpriteBatch.DrawString(font, text, _transformBuffer.DestinationPosition, color, _transformBuffer.RotationRadian, 
            new Vector2(0, 0), drawable.Scale, SpriteEffects.None, 0f);
        SpriteBatch.End();
    }

    private void VerifySuspendState()
    {
        if (_isSuspended)
            throw new Exception("You must begin the DisplayManager before drawing");
    }

    private Color GetAlphaColor(float opacity)
    {
        return Color.FromNonPremultiplied(255, 255, 255, (int)(255 * opacity));
    }

    private Color GetAlphaColor(Color baseColor, float opacity)
    {
        return Color.FromNonPremultiplied(baseColor.R, baseColor.G, baseColor.B, (int)(baseColor.A * opacity));
    }

    protected Texture2D CreateColorTexture(Color color)
    {
        var texture = new Texture2D(GraphicsDevice, 1, 1);
        texture.SetData(new[] { color });
        return texture;
    }
}
