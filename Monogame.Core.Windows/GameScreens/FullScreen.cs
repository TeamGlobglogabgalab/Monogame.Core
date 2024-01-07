using Microsoft.Xna.Framework;
using Monogame.Core;
using Monogame.Core.Windows.Camera;
using Monogame.Core.Windows.Structs;

namespace Monogame.Core.Windows.GameScreens;

public class FullScreen : GameScreen
{
    public override Rectangle ClientBounds
    {
        get
        {
            _bounds.Width = GameWindow.ClientBounds.Width;
            _bounds.Height = GameWindow.ClientBounds.Height;
            return ApplyPadding(_bounds);
        }
    }
    public override Point TargetSize => _targetSize;

    private readonly Point _targetSize;
    private Rectangle _bounds;

    public FullScreen(GameWindow gameWindow, Padding padding) : base(gameWindow, padding)
    {
        GameWindow = gameWindow;
        _targetSize = new Point(gameWindow.ClientBounds.Width, gameWindow.ClientBounds.Height);
        //_bounds = new Rectangle(0, 0, gameWindow.ClientBounds.Width, gameWindow.ClientBounds.Height);
    }

    public FullScreen(GameWindow gameWindow) : this(gameWindow, new Padding(0))
    {
    }
}