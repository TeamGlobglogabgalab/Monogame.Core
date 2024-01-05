using Microsoft.Xna.Framework;
using Monogame.Core.Windows.Camera;
using Monogame.Core.Windows.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame.Core.Windows.GameScreens;

public abstract class GameScreen : IGameScreen
{
    public abstract Rectangle ClientBounds { get; }
    public Padding Padding { get; set; }
    public float ScreenRatio => (float)ClientBounds.Width / (float)ClientBounds.Height;
    public IGameCamera Camera { get; private set; }
    public abstract Point TargetSize { get; }

    protected GameWindow GameWindow;

    protected GameScreen(GameWindow gameWindow, Padding padding)
    {
        GameWindow = gameWindow;
        Padding = padding;
        Camera = new GameCamera();
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
}
