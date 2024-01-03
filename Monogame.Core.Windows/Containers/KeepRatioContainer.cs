using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monogame.Core;

namespace Monogame.Core.Windows.Containers;

public class KeepRatioContainer : ScalableContainer
{
    public KeepRatioContainer() : base()
    {
    }

    public KeepRatioContainer(GraphicsDevice graphicsDevice, int targetWidth, int targetHeight) :
        base(graphicsDevice, targetWidth, targetHeight)
    {
    }

    public override Rectangle GetBoundingBoxRectangle(IGameScreen gameScreen, Rectangle rect)
    {
        float minScale = GetMinScale(gameScreen);
        Vector2 position = GetPosition(gameScreen, minScale);
        Vector2 screenPos = GetScreenPosition(gameScreen);
        return new Rectangle(
            (int)(rect.X * minScale) + (int)position.X + (int)screenPos.X,
            (int)(rect.Y * minScale) + (int)position.Y + (int)screenPos.Y,
            (int)(rect.Width * minScale),
            (int)(rect.Height * minScale));
    }

    public override void Draw(IGameScreen gameScreen, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, SpriteEffects spriteEffects)
    {
        float minScale = GetMinScale(gameScreen);
        Vector2 position = GetPosition(gameScreen, minScale);
        Vector2 screenPos = GetScreenPosition(gameScreen);
        spriteBatch.Draw(RenderTarget, position + screenPos, null, Color.White, 0f, Vector2.Zero, new Vector2(minScale, minScale), spriteEffects, 0f);
    }

    private float GetMinScale(IGameScreen gameScreen)
    {
        Vector2 scale = GetScale(gameScreen);
        return Math.Min(scale.X, scale.Y);
    }

    private Vector2 GetPosition(IGameScreen gameScreen, float scale)
    {
        return new Vector2()
        {
            X = (gameScreen.ClientBounds.Width - TargetWidth * scale) / 2,
            Y = (gameScreen.ClientBounds.Height - TargetHeight * scale) / 2
        };
    }
}
