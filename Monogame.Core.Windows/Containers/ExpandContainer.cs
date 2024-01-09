using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monogame.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame.Core.Windows.Containers;

public class ExpandContainer : ScalableContainer
{
    private Vector2 _drawScale = new Vector2(1, 1);

    public ExpandContainer() : base()
    {
    }

    public ExpandContainer(GraphicsDevice graphicsDevice, int targetWidth, int targetHeight) :
        base(graphicsDevice, targetWidth, targetHeight)
    {
    }

    public override void SetRenderTarget(IGameScreen gameScreen, GraphicsDevice graphicsDevice)
    {
        if (RenderTarget.Width != gameScreen.ClientBounds.Width || RenderTarget.Height != gameScreen.ClientBounds.Height)
        {
            RenderTarget.Dispose();
            RenderTarget = new RenderTarget2D(graphicsDevice, gameScreen.ClientBounds.Width, gameScreen.ClientBounds.Height);
        }
        base.SetRenderTarget(gameScreen, graphicsDevice);
    }

    public override Rectangle GetBoundingBoxRectangle(IGameScreen gameScreen, Rectangle rect)
    {
        Vector2 screenPos = GetScreenPosition(gameScreen);
        return new Rectangle(rect.X + (int)screenPos.X, rect.Y + (int)screenPos.Y, rect.Width, rect.Height);
    }

    /*public override void Draw(IGameScreen gameScreen, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, SpriteEffects spriteEffects)
    {
        spriteBatch.Draw(RenderTarget, GetScreenPosition(gameScreen), null, Color.White, 0f, Vector2.Zero, new Vector2(1, 1), spriteEffects, 0f);
    }*/

    public override Vector2 GetDrawPosition(IGameScreen gameScreen) => GetScreenPosition(gameScreen);

    public override Vector2 GetDrawScale(IGameScreen gameScreen) => _drawScale;
}
