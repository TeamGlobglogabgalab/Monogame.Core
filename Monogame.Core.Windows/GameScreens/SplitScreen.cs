using Microsoft.Xna.Framework;
using Monogame.Core.Windows.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame.Core.Windows.GameScreens;

public abstract class SplitScreen : GameScreen
{
    public int ScreenCount
    {
        get => _screenCount;
        set
        {
            if(value < 1) throw new ArgumentOutOfRangeException(nameof(ScreenCount), value, "Must be >= 1");
            if(value < EndScreenIndex + 1) throw new ArgumentException($"Must be > EndScreenIndex", nameof(ScreenCount));
            _screenCount = value;
        }
    }
    public int StartScreenIndex
    {
        get => _startScreenIndex;
        set
        {
            if (value < 0 || value >= ScreenCount)
                throw new ArgumentOutOfRangeException(nameof(StartScreenIndex), value, $"Must be in [0;{ScreenCount - 1}]");
            if(value > EndScreenIndex)
                throw new ArgumentException($"Must be <= EndScreenIndex", nameof(StartScreenIndex));
            _startScreenIndex = value;
        }
    }
    public int EndScreenIndex
    {
        get => _endScreenIndex;
        set
        {
            if (value < 0 || value >= ScreenCount)
                throw new ArgumentOutOfRangeException(nameof(EndScreenIndex), value, $"Must be in [0;{ScreenCount - 1}]");
            if(value < StartScreenIndex)
                throw new ArgumentException($"Must be >= StartScreenIndex", nameof(EndScreenIndex));
            _endScreenIndex = value;
        }
    }
    
    private int _screenCount;
    private int _startScreenIndex;
    private int _endScreenIndex;
    
    protected SplitScreen(GameWindow gameWindow, int screenCount, int startScreenIndex, int endScreenIndex, Padding padding) : base(gameWindow, padding)
    {
        ScreenCount = screenCount;
        EndScreenIndex = endScreenIndex;
        StartScreenIndex = startScreenIndex;
    }

    protected SplitScreen(GameWindow gameWindow, int screenCount, int startScreenIndex, int endScreenIndex) : 
        this(gameWindow, screenCount, startScreenIndex, endScreenIndex, new Padding(0))
    {
    }

    protected SplitScreen(GameWindow gameWindow, int screenCount, int screenIndex, Padding padding) : 
        this(gameWindow, screenCount, screenIndex, screenIndex, padding)
    {
    }

    protected SplitScreen(GameWindow gameWindow, int screenCount, int screenIndex) : 
        this(gameWindow, screenCount, screenIndex, new Padding(0))
    {
    }
}
