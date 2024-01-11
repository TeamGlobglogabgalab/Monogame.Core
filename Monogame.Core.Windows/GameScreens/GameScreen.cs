using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monogame.Core.Windows.Camera;
using Monogame.Core.Windows.Containers;
using Monogame.Core.Windows.Structs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame.Core.Windows.GameScreens;

public abstract class GameScreen : IGameScreen
{
    public abstract Rectangle ClientBounds { get; }
    public Padding Padding { get; set; }
    public float ScreenRatio => (float)ClientBounds.Width / (float)ClientBounds.Height;
    public abstract Point TargetSize { get; }
    public IScalableContainer ScalableContainer { get; private set; }
    public GameWindow GameWindow { get; protected set; }

    protected GameScreen(GameWindow gameWindow, IScalableContainer container, Padding padding)
    {
        GameWindow = gameWindow;
        ScalableContainer = container;
        Padding = padding;
    }

    protected GameScreen(GameWindow gameWindow, IScalableContainer container) : 
        this(gameWindow, container, new Padding(0))
    {
    }

    protected GameScreen(GameWindow gameWindow) :
        this(gameWindow, new KeepRatioContainer(), new Padding(0))
    {
    }

    public void Draw(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, SpriteEffects spriteEffects)
    {
        Vector2 position = ScalableContainer.GetDrawPosition(this);
        Vector2 scale = ScalableContainer.GetDrawScale(this);
        spriteBatch.Draw(ScalableContainer.RenderTarget, position, null, Color.White, 0f, Vector2.Zero, scale, spriteEffects, 0f);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    protected Rectangle ApplyPadding(Rectangle clientBounds)
    {
        clientBounds.X += Padding.Left;
        clientBounds.Y += Padding.Top;
        clientBounds.Width -= Padding.Right + Padding.Left;
        clientBounds.Height -= Padding.Bottom + Padding.Top;
        return clientBounds;
    }

    protected static IScalableContainer GetDefaultContainer() => new KeepRatioContainer();
}
