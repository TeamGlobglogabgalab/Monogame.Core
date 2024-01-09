using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monogame.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Monogame.Core.Windows.Containers;

public class StretchContainer : ScalableContainer
{
    public StretchContainer() : base()
    {
    }

    public StretchContainer(GraphicsDevice graphicsDevice, int targetWidth, int targetHeight) :
        base(graphicsDevice, targetWidth, targetHeight)
    {
    }

    public override Rectangle GetBoundingBoxRectangle(IGameScreen gameScreen, Rectangle rect)
    {
        Vector2 screenPos = GetScreenPosition(gameScreen);
        Vector2 scale = GetScale(gameScreen);
        return new Rectangle(
            (int)(rect.X * scale.X) + (int)screenPos.X,
            (int)(rect.Y * scale.Y) + (int)screenPos.Y,
            (int)(rect.Width * scale.X),
            (int)(rect.Height * scale.Y));
    }

    /*public override void Draw(IGameScreen gameScreen, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, SpriteEffects spriteEffects)
    {
        spriteBatch.Draw(RenderTarget, GetScreenPosition(gameScreen), null, Color.White, 0f, Vector2.Zero, GetScale(gameScreen), spriteEffects, 0f);
    }*/

    public override Vector2 GetDrawPosition(IGameScreen gameScreen) => GetScreenPosition(gameScreen);

    public override Vector2 GetDrawScale(IGameScreen gameScreen) => GetScale(gameScreen);
}
