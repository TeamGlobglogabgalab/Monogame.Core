using Microsoft.Xna.Framework;
using Monogame.Core;
using Monogame.Core.Windows.Structs;

namespace Monogame.Core.Windows.GameScreens;

public class VerticalSplitScreen : SplitScreen
{
    public override Rectangle ClientBounds
    {
        get
        {
            int width = GameWindow.ClientBounds.Width / ScreenCount;
            int height = GameWindow.ClientBounds.Height;
            int screenWidthCount = EndScreenIndex - StartScreenIndex + 1;
            _bounds.X = width * StartScreenIndex;
            _bounds.Y = 0;
            _bounds.Width = width * screenWidthCount;
            _bounds.Height = height;
            return ApplyPadding(_bounds);
        }    
    }
    public override Point TargetSize => _targetSize;

    private Rectangle _bounds = new Rectangle();
    private readonly Point _targetSize;

    public VerticalSplitScreen(GameWindow gameWindow, int screenCount, int screenIndex) : base(gameWindow, screenCount, screenIndex)
    {
        _targetSize = InitTargetSize();
    }

    public VerticalSplitScreen(GameWindow gameWindow, int screenCount, int screenIndex, Padding padding) : base(gameWindow, screenCount, screenIndex, padding)
    {
        _targetSize = InitTargetSize();
    }

    public VerticalSplitScreen(GameWindow gameWindow, int screenCount, int startScreenIndex, int endScreenIndex) :
        base(gameWindow, screenCount, startScreenIndex, endScreenIndex)
    {
        _targetSize = InitTargetSize();
    }

    public VerticalSplitScreen(GameWindow gameWindow, int screenCount, int startScreenIndex, int endScreenIndex, Padding padding) : 
        base(gameWindow, screenCount, startScreenIndex, endScreenIndex, padding)
    {
        _targetSize = InitTargetSize();
    }

    private Point InitTargetSize()
    {
        int width = GameWindow.ClientBounds.Width / ScreenCount;
        int screenWidthCount = EndScreenIndex - StartScreenIndex + 1;
        return new Point(width * screenWidthCount, GameWindow.ClientBounds.Height);
    }
}