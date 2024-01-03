using Microsoft.Xna.Framework;
using Monogame.Core.Windows.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame.Core.Windows.GameScreens;

public class SmartGridScreen : GameScreen
{
    public override Rectangle ClientBounds => GetBounds(OptimalGridDefinition, _gridIndex);
    public int ScreenIndex
    {
        get => _screenIndex;
        set
        {
            if (value < 0 || value >= ScreenCount)
                throw new ArgumentOutOfRangeException(nameof(ScreenIndex), $"Must be in [0;{ScreenCount - 1}]");
            _screenIndex = value;
        }
    }
    public int ScreenCount
    {
        get => _screenCount;
        set
        {
            if (value < 1) throw new ArgumentOutOfRangeException(nameof(ScreenCount), "Minimum value is 1");
            if(value < ScreenIndex + 1) throw new ArgumentException($"Must be > {nameof(ScreenIndex)}", nameof(ScreenCount));
            _screenCount = value;
            _gridDefinitions = ListAllGridDefinitions(_screenCount);
        }
    }

    private GridDefinition OptimalGridDefinition
    {
        get
        {
            _ratioScores.Clear();
            foreach (var gd in _gridDefinitions)
            {
                var gridIndex = GetGridIndexFromCellIndex(_screenCount, gd.columnCount, _screenIndex);
                var bounds = GetBounds(gd, gridIndex);
                var ratio = (float)bounds.Width / (float)bounds.Height;
                _ratioScores.Add(Math.Abs(_optimalScreenRatio - ratio));
            }
            var bestRatioIndex = _ratioScores.IndexOf(_ratioScores.Min());
            var definition = _gridDefinitions[bestRatioIndex];
            _gridIndex = GetGridIndexFromCellIndex(_screenCount, definition.columnCount, _screenIndex);
            return definition;
        }
    }
    public override Point TargetSize => _targetSize;

    private int _screenIndex;
    private readonly float _optimalScreenRatio;
    private GridIndex _gridIndex;
    private int _screenCount;
    private List<GridDefinition> _gridDefinitions;
    private Rectangle _bounds = new Rectangle();
    private readonly List<float> _ratioScores = new List<float>();
    private readonly Point _targetSize;

    public SmartGridScreen(GameWindow gameWindow, int screenCount, int screenIndex, int optimalWidth, int optimalHeight, Padding padding) : base(gameWindow, padding)
    {
        ScreenCount = screenCount;
        ScreenIndex = screenIndex;
        _optimalScreenRatio = (float)optimalWidth / (float)optimalHeight;
        _targetSize = InitTargetSize();
    }

    public SmartGridScreen(GameWindow gameWindow, int screenCount, int screenIndex, int optimalWidth, int optimalHeight) : 
        this(gameWindow, screenCount, screenIndex, optimalWidth, optimalHeight, new Padding(0))
    {
    }

    public SmartGridScreen(GameWindow gameWindow, int screenCount, int screenIndex, float optimalScreenRatio, Padding padding) :
        this(gameWindow, screenCount, screenIndex, (int)(900 * optimalScreenRatio), 900, padding)
    {
    }

    public SmartGridScreen(GameWindow gameWindow, int screenCount, int screenIndex, float optimalScreenRatio) :
        this(gameWindow, screenCount, screenIndex, (int)(900 * optimalScreenRatio), 900, new Padding(0))
    {
    }

    private GridIndex GetGridIndexFromCellIndex(int screenCount, int columnCount, int screenIndex)
    {
        int rowIndex = screenIndex / columnCount;
        int columnIndex = screenIndex % columnCount;
        return new GridIndex(rowIndex, columnIndex);
    }

    private List<GridDefinition> ListAllGridDefinitions(int screenCount)
    {
        var definitions = new List<GridDefinition>();
        bool oddScreenCount = screenCount % 2 != 0;
        var adjustedCount = screenCount % 2 == 0 ? screenCount : screenCount + 1;
        for (int rows = 1; rows <= adjustedCount; rows++)
        {
            if (adjustedCount % rows != 0) continue;
            
            int columns = adjustedCount / rows;
            if(oddScreenCount && rows == 1 && columns > 1)
                definitions.Add(new GridDefinition(rows, screenCount));
            else if (oddScreenCount && columns == 1 && rows > 1)
                definitions.Add(new GridDefinition(screenCount, columns));
            else
                definitions.Add(new GridDefinition(rows, columns));
        }
        return definitions;
    }

    private Rectangle GetBounds(GridDefinition definition, GridIndex gridIndex)
    {
        int width = GameWindow.ClientBounds.Width / definition.columnCount;
        int height = GameWindow.ClientBounds.Height / definition.rowCount;
        _bounds.X = width * gridIndex.columnIndex;
        _bounds.Y = height * gridIndex.rowIndex;
        _bounds.Width = width;
        _bounds.Height = height;
        return ApplyPadding(_bounds);
    }

    private Point InitTargetSize()
    {
        var definition = OptimalGridDefinition;
        return new Point(GameWindow.ClientBounds.Width / definition.columnCount, GameWindow.ClientBounds.Height / definition.rowCount);
    }
}
