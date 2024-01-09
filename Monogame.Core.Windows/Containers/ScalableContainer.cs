using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monogame.Core;
using Monogame.Core.Windows.Anchors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame.Core.Windows.Containers;

public abstract class ScalableContainer : IScalableContainer
{
    public int TargetWidth { get; private set; }
    public int TargetHeight { get; private set; }
    public RenderTarget2D RenderTarget { get; protected set; }
    public Point RenderingSize => new Point(RenderTarget.Width, RenderTarget.Height);
    public bool IsReady { get; private set; }

    protected ScalableContainer()
    {
    }

    protected ScalableContainer(GraphicsDevice graphicsDevice, int targetWidth, int targetHeight)
    {
        UpdateTargetSize(graphicsDevice, targetWidth, targetHeight);
    }

    public void UpdateTargetSize(GraphicsDevice graphicsDevice, int targetWidth, int targetHeight)
    {
        if (targetWidth < 0 || targetHeight < 0)
            throw new ArgumentException("Invalid size! Minimum 1x1");

        TargetWidth = targetWidth;
        TargetHeight = targetHeight;
        RenderTarget = new RenderTarget2D(graphicsDevice, targetWidth, targetHeight);
        IsReady = true;
    }

    public Point GetAnchorPosition(Point basePosition, Vector2 targetPosition, Anchor anchor)
    {
        return anchor.GetAnchorPosition(
            new Point(basePosition.X + (int)targetPosition.X, basePosition.Y + (int)targetPosition.Y),
            RenderingSize.X, RenderingSize.Y);
    }

    public virtual void SetRenderTarget(IGameScreen gameScreen, GraphicsDevice graphicsDevice)
    {
        graphicsDevice.SetRenderTarget(RenderTarget);
    }

    public void Dispose()
    {
        RenderTarget.Dispose();
        GC.SuppressFinalize(this);
    }

    public abstract Rectangle GetBoundingBoxRectangle(IGameScreen gameScreen, Rectangle rect);
    //public abstract void Draw(IGameScreen gameScreen, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, SpriteEffects spriteEffects);
    public abstract Vector2 GetDrawPosition(IGameScreen gameScreen);
    public abstract Vector2 GetDrawScale(IGameScreen gameScreen);

    protected Rectangle GetAnchorBoundingBox(Point position, Anchor anchor, Rectangle baseRect)
    {
        var anchorPos = GetAnchorPosition(position, new Vector2(baseRect.X, baseRect.Y), anchor);
        return new Rectangle(anchorPos.X + baseRect.X, anchorPos.Y + baseRect.Y, baseRect.Width, baseRect.Height);
    }

    protected Vector2 GetScale(IGameScreen gameScreen)
    {
        float scaleX = (float)gameScreen.ClientBounds.Width / TargetWidth;
        float scaleY = (float)gameScreen.ClientBounds.Height / TargetHeight;
        return new Vector2(scaleX, scaleY);
    }

    protected Vector2 GetScreenPosition(IGameScreen screen) => new Vector2(screen.ClientBounds.X, screen.ClientBounds.Y);
    protected Point GetScreenSize(IGameScreen screen) => new Point(screen.ClientBounds.Width, screen.ClientBounds.Height);
}
