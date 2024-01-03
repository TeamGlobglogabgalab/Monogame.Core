using Microsoft.Xna.Framework;
using Monogame.Core;
using Monogame.Core.Windows.Structs;

namespace Monogame.Core.Windows.GameScreens;

public class HorizontalSplitScreen : SplitScreen
{
    public override Rectangle ClientBounds
    {
        get
        {
            int width = GameWindow.ClientBounds.Width;
            int height = GameWindow.ClientBounds.Height / ScreenCount;
            int screenHeightCount = EndScreenIndex - StartScreenIndex + 1;
            _bounds.X = 0;
            _bounds.Y = height * StartScreenIndex;
            _bounds.Width = width;
            _bounds.Height = height * screenHeightCount;
            return ApplyPadding(_bounds);
        }    
    }
    public override Point TargetSize => _targetSize;

    private Rectangle _bounds = new Rectangle();
    private readonly Point _targetSize;

    public HorizontalSplitScreen(GameWindow gameWindow, int screenCount, int screenIndex) : base(gameWindow, screenCount, screenIndex) 
    {
        _targetSize = InitTargetSize();
    }

    public HorizontalSplitScreen(GameWindow gameWindow, int screenCount, int screenIndex, Padding padding) : base(gameWindow, screenCount, screenIndex, padding)
    {
        _targetSize = InitTargetSize();
    }

    public HorizontalSplitScreen(GameWindow gameWindow, int screenCount, int startScreenIndex, int endScreenIndex) :
        base(gameWindow, screenCount, startScreenIndex, endScreenIndex)
    {
        _targetSize = InitTargetSize();
    }

    public HorizontalSplitScreen(GameWindow gameWindow, int screenCount, int startScreenIndex, int endScreenIndex, Padding padding) :
        base(gameWindow, screenCount, startScreenIndex, endScreenIndex, padding)
    {
        _targetSize = InitTargetSize();
    }

    private Point InitTargetSize()
    {
        int height = GameWindow.ClientBounds.Height / ScreenCount;
        int screenHeightCount = EndScreenIndex - StartScreenIndex + 1;
        return new Point(GameWindow.ClientBounds.Width, height * screenHeightCount);
    }
}