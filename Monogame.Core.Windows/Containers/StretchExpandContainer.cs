using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monogame.Core;

namespace Monogame.Core.Windows.Containers;

public class StretchExpandContainer : ScalableContainer
{
    public StretchExpandContainer() : base()
    {
    }

    public StretchExpandContainer(GraphicsDevice graphicsDevice, int targetWidth, int targetHeight) :
        base(graphicsDevice, targetWidth, targetHeight)
    {
    }

    public override void SetRenderTarget(IGameScreen gameScreen, GraphicsDevice graphicsDevice)
    {
        //Scale calculation
        var scale = GetScale(gameScreen);
        float maxScale = GetMaxScale(gameScreen);
        float minScale = GetMinScale(gameScreen);
        float maxScaleIfMinEqual1 = 1 * maxScale / minScale;
        int screenWidth, screenHeight;
        if (scale.X >= scale.Y)
        {
            screenWidth = (int)(TargetWidth * maxScaleIfMinEqual1);
            screenHeight = TargetHeight;
        }
        else
        {
            screenWidth = TargetWidth;
            screenHeight = (int)(TargetHeight * maxScaleIfMinEqual1);
        }

        //Set RenderTarget
        if (RenderTarget.Width != screenWidth || RenderTarget.Height != screenHeight)
        {
            RenderTarget.Dispose();
            RenderTarget = new RenderTarget2D(graphicsDevice, screenWidth, screenHeight);
        }
        base.SetRenderTarget(gameScreen, graphicsDevice);
    }

    public override Rectangle GetBoundingBoxRectangle(IGameScreen gameScreen, Rectangle rect)
    {
        float minScale = GetMinScale(gameScreen);
        Vector2 screenPos = GetScreenPosition(gameScreen);
        return new Rectangle(
            (int)(rect.X * minScale) + (int)screenPos.X,
            (int)(rect.Y * minScale) + (int)screenPos.Y,
            (int)(rect.Width * minScale),
            (int)(rect.Height * minScale));
    }

    /*public override void Draw(IGameScreen gameScreen, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, SpriteEffects spriteEffects)
    {
        float minScale = GetMinScale(gameScreen);
        spriteBatch.Draw(RenderTarget, GetScreenPosition(gameScreen), null, Color.White, 0f, Vector2.Zero, new Vector2(minScale, minScale), spriteEffects, 0f);
    }*/

    public override Vector2 GetDrawPosition(IGameScreen gameScreen) => GetScreenPosition(gameScreen);

    public override Vector2 GetDrawScale(IGameScreen gameScreen)
    {
        var minScale = GetMinScale(gameScreen);
        return new Vector2(minScale, minScale);
    }

    private float GetMinScale(IGameScreen gameScreen)
    {
        Vector2 scale = GetScale(gameScreen);
        return Math.Min(scale.X, scale.Y);
    }

    private float GetMaxScale(IGameScreen gameScreen)
    {
        Vector2 scale = GetScale(gameScreen);
        return Math.Max(scale.X, scale.Y);
    }
}
