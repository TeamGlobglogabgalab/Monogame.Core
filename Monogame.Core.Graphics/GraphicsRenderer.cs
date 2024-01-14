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
using Microsoft.Xna.Framework.Content;
using System.IO;
using System.Reflection;
using System.Reflection.Metadata;
using Monogame.Core.Windows.GameScreens;

namespace Monogame.Core.Graphics;

public class GraphicsRenderer : IGraphicsRenderer
{
    public const string ShadersLocation = @"\Monogame.Core.Graphics.Shaders";

    public IScalableContainer ScalableContainer
    {
        set => _scalableContainer = value;
    }
    public IGameCamera Camera
    {
        set => _camera = value;
    }

    protected SpriteBatch SpriteBatch;
    protected GraphicsDevice GraphicsDevice;
    protected Effect CircleShader { get; private set; }
    protected Effect TriangleShader { get; private set; }

    private bool _isSuspended;
    private IScalableContainer _scalableContainer;
    private IGameCamera _camera;
    private DrawableTransform _transformBuffer = new DrawableTransform();
    private Dictionary<Color, Texture2D> _colorTextures = new Dictionary<Color, Texture2D>();

    public GraphicsRenderer(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, ContentManager content, IScalableContainer scalableContainer, IGameCamera camera)
    {
        SpriteBatch = spriteBatch;
        GraphicsDevice = graphicsDevice;
        _isSuspended = true;
        _scalableContainer = scalableContainer;
        _camera = camera;

        var oldDirectory = content.RootDirectory;
        content.RootDirectory = AppDomain.CurrentDomain.BaseDirectory + ShadersLocation;
        CircleShader = content.Load<Effect>("Circle");
        TriangleShader = content.Load<Effect>("Triangle");
        content.RootDirectory = oldDirectory;
    }

    public void Resume()
    {
        _isSuspended = false;
    }

    public void Suspend()
    {
        _isSuspended = true;
    }

    public void Draw(Drawable drawable, Texture2D texture, Rectangle rect, GameTime gameTime, Effect effect = null, bool forceBasicTransform = false)
    {
        VerifySuspendState();
        _transformBuffer.UpdateTransform(drawable, rect, _scalableContainer, _camera, forceBasicTransform);
        var color = GetAlphaColor(drawable.Opacity);

        SpriteBatch.Begin(effect: effect);
        SpriteBatch.Draw(texture, _transformBuffer.DestinationRect, _transformBuffer.SourceRect, color,
            _transformBuffer.RotationRadian, new Vector2(0, 0), SpriteEffects.None, 0f);
        SpriteBatch.End();
    }

    public void DrawRectangle(Drawable drawable, Color color, Rectangle rect, GameTime gameTime, Effect effect = null)
    {
        var texture = GetColorTexture(color);
        Draw(drawable, texture, rect, gameTime, effect);
    }

    public void DrawString(Drawable drawable, SpriteFont font, string text, Vector2 position, Color color, GameTime gameTime, Effect effect = null)
    {
        VerifySuspendState();
        _transformBuffer.UpdateTransform(drawable, position, _scalableContainer, _camera);
        color = GetAlphaColor(color, drawable.Opacity);

        SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, effect: effect);
        SpriteBatch.DrawString(font, text, _transformBuffer.DestinationPosition, color, _transformBuffer.RotationRadian,
            new Vector2(0, 0), drawable.Scale, SpriteEffects.None, 0f);
        SpriteBatch.End();
    }

    public void DrawOval(Drawable drawable, Texture2D texture, Rectangle rect, GameTime gameTime)
    {
        Draw(drawable, texture, rect, gameTime, CircleShader, true);
    }

    public void DrawOval(Drawable drawable, Color color, Rectangle rect, GameTime gameTime)
    {
        DrawOval(drawable, GetColorTexture(color), rect, gameTime);
    }

    public void DrawTriangle(Drawable drawable, Color color, Vector2 a, Vector2 b, Vector2 c, GameTime gameTime)
    {
        var texture = GetColorTexture(color);
        DrawTriangle(drawable, texture, a, b, c, gameTime);
    }

    public void DrawTriangle(Drawable drawable, Texture2D texture, Vector2 a, Vector2 b, Vector2 c, GameTime gameTime)
    {
        var minX = MathTools.Min(a.X, b.X, c.X);
        var minY = MathTools.Min(a.Y, b.Y, c.Y);
        var width = MathTools.Max(a.X, b.X, c.X) - minX;
        var height = MathTools.Max(a.Y, b.Y, c.Y) - minY;
        var rect = new Rectangle((int)minX, (int)minY, (int)width, (int)height);

        TriangleShader.Parameters["ax"].SetValue(a.X / width);
        TriangleShader.Parameters["ay"].SetValue(a.Y / height);
        TriangleShader.Parameters["bx"].SetValue(b.X / width);
        TriangleShader.Parameters["by"].SetValue(b.Y / height);
        TriangleShader.Parameters["cx"].SetValue(c.X / width);
        TriangleShader.Parameters["cy"].SetValue(c.Y / height);
        Draw(drawable, texture, rect, gameTime, TriangleShader, true);
    }

    protected Texture2D CreateColorTexture(Color color)
    {
        var texture = new Texture2D(GraphicsDevice, 1, 1);
        texture.SetData(new[] { color });
        return texture;
    }

    protected Texture2D GetColorTexture(Color color)
    {
        if (!_colorTextures.TryGetValue(color, out Texture2D texture))
        {
            texture = CreateColorTexture(color);
            _colorTextures.Add(color, texture);
        }
        return texture;
    }

    protected void VerifySuspendState()
    {
        if (_isSuspended)
            throw new Exception("You must begin the DisplayManager before drawing");
    }

    protected Color GetAlphaColor(float opacity)
    {
        return Color.FromNonPremultiplied(255, 255, 255, (int)(255 * opacity));
    }

    protected Color GetAlphaColor(Color baseColor, float opacity)
    {
        return Color.FromNonPremultiplied(baseColor.R, baseColor.G, baseColor.B, (int)(baseColor.A * opacity));
    }
}
