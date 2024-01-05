using Microsoft.Xna.Framework;
using Monogame.Core;
using Monogame.Core.Windows.Camera;
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

    private readonly Point _targetSize;
    private Rectangle _bounds = new Rectangle();

    public HorizontalSplitScreen(GameWindow gameWindow, int screenCount, int startScreenIndex, int endScreenIndex, Padding padding) :
        base(gameWindow, screenCount, startScreenIndex, endScreenIndex, padding)
    {
        _targetSize = InitTargetSize();
    }

    public HorizontalSplitScreen(GameWindow gameWindow, int screenCount, int startScreenIndex, int endScreenIndex) :
        this(gameWindow, screenCount, startScreenIndex, endScreenIndex, new Padding(0))
    {
    }

    public HorizontalSplitScreen(GameWindow gameWindow, int screenCount, int screenIndex, Padding padding) : 
        this(gameWindow, screenCount, screenIndex, screenIndex, padding)
    {
    }

    public HorizontalSplitScreen(GameWindow gameWindow, int screenCount, int screenIndex) :
        this(gameWindow, screenCount, screenIndex, screenIndex, new Padding(0)) 
    {
    }

    private Point InitTargetSize()
    {
        int height = GameWindow.ClientBounds.Height / ScreenCount;
        int screenHeightCount = EndScreenIndex - StartScreenIndex + 1;
        return new Point(GameWindow.ClientBounds.Width, height * screenHeightCount);
    }
}