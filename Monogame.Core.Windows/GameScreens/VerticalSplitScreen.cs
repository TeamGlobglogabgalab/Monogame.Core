using Microsoft.Xna.Framework;
using Monogame.Core;
using Monogame.Core.Windows.Camera;
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

    private readonly Point _targetSize;
    private Rectangle _bounds = new Rectangle();

    public VerticalSplitScreen(GameWindow gameWindow, IScalableContainer container, int screenCount, int startScreenIndex, int endScreenIndex, Padding padding) :
        base(gameWindow, container, screenCount, startScreenIndex, endScreenIndex, padding)
    {
        _targetSize = InitTargetSize();
    }

    public VerticalSplitScreen(GameWindow gameWindow, int screenCount, int startScreenIndex, int endScreenIndex, Padding padding) :
        this(gameWindow, GetDefaultContainer(), screenCount, startScreenIndex, endScreenIndex, padding)
    {
    }

    public VerticalSplitScreen(GameWindow gameWindow, IScalableContainer container, int screenCount, int startScreenIndex, int endScreenIndex) :
        this(gameWindow, container, screenCount, startScreenIndex, endScreenIndex, new Padding(0))
    {
    }

    public VerticalSplitScreen(GameWindow gameWindow, int screenCount, int startScreenIndex, int endScreenIndex) :
       this(gameWindow, screenCount, startScreenIndex, endScreenIndex, new Padding(0))
    {
    }

    public VerticalSplitScreen(GameWindow gameWindow, IScalableContainer container, int screenCount, int screenIndex, Padding padding) : 
        this(gameWindow, container, screenCount, screenIndex, screenIndex, padding)
    {
    }

    public VerticalSplitScreen(GameWindow gameWindow, int screenCount, int screenIndex, Padding padding) : 
        this(gameWindow, screenCount, screenIndex, screenIndex, padding)
    {
    }

    public VerticalSplitScreen(GameWindow gameWindow, IScalableContainer container, int screenCount, int screenIndex) : 
        this(gameWindow, container, screenCount, screenIndex, screenIndex, new Padding(0))
    {
    }

    public VerticalSplitScreen(GameWindow gameWindow, int screenCount, int screenIndex) :
        this(gameWindow, screenCount, screenIndex, screenIndex, new Padding(0))
    {
    }

    private Point InitTargetSize()
    {
        int width = GameWindow.ClientBounds.Width / ScreenCount;
        int screenWidthCount = EndScreenIndex - StartScreenIndex + 1;
        return new Point(width * screenWidthCount, GameWindow.ClientBounds.Height);
    }
}