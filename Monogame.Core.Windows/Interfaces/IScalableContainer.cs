using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monogame.Core.Windows.Anchors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame.Core.Windows;

public interface IScalableContainer : IDisposable
{
    public int TargetWidth { get; }
    public int TargetHeight { get; }
    public RenderTarget2D RenderTarget { get; }
    public Point RenderingSize { get; }
    public bool IsReady { get; }
    public void UpdateTargetSize(GraphicsDevice graphicsDevice, int targetWidth, int targetHeight);
    public Point GetAnchorPosition(Point basePosition, Vector2 targetPosition, Anchor anchor);
    public void SetRenderTarget(IGameScreen gameScreen, GraphicsDevice graphicsDevice);
    public Rectangle GetBoundingBoxRectangle(IGameScreen gameScreen, Rectangle rect);
    public Vector2 GetDrawPosition(IGameScreen gameScreen);
    public Vector2 GetDrawScale(IGameScreen gameScreen);
}
