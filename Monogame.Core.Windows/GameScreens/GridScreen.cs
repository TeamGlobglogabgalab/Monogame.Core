using Microsoft.Xna.Framework;
using Monogame.Core.Windows.Camera;
using Monogame.Core.Windows.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame.Core.Windows.GameScreens;

public class GridScreen : GameScreen
{
    public override Rectangle ClientBounds
    {
        get
        {
            int width = GameWindow.ClientBounds.Width / GridDefinition.columnCount;
            int height = GameWindow.ClientBounds.Height / GridDefinition.rowCount;
            int screenWidthCount = _endGridIndex.columnIndex - _startGridIndex.columnIndex + 1;
            int screenHeightCount = _endGridIndex.rowIndex - _startGridIndex.rowIndex + 1;
            _bounds.X = width * _startGridIndex.columnIndex;
            _bounds.Y = height * _startGridIndex.rowIndex;
            _bounds.Width = width * screenWidthCount;
            _bounds.Height = height * screenHeightCount;
            return ApplyPadding(_bounds);
        }
    }

    public GridIndex StartGridIndex
    {
        get => _startGridIndex;
        set
        {
            if (value.columnIndex >= GridDefinition.columnCount || value.rowIndex >= GridDefinition.rowCount)
                throw new ArgumentOutOfRangeException(nameof(StartGridIndex), $"Must be in [(0,0);({GridDefinition.columnCount - 1},{GridDefinition.rowCount - 1})]");
            if (EndGridIndex.columnIndex < value.columnIndex || EndGridIndex.rowIndex < value.rowIndex)
                throw new ArgumentException($"Must be in <= {nameof(EndGridIndex)}", nameof(StartGridIndex));
            _startGridIndex = value;
        }
    }

    public GridIndex EndGridIndex
    {
        get => _endGridIndex;
        set
        {
            if (value.columnIndex >= GridDefinition.columnCount || value.rowIndex >= GridDefinition.rowCount)
                throw new ArgumentOutOfRangeException(nameof(EndGridIndex), $"Must be in [(0,0);({GridDefinition.columnCount - 1},{GridDefinition.rowCount - 1})]");
            if (value.columnIndex < StartGridIndex.columnIndex || value.rowIndex < StartGridIndex.rowIndex)
                throw new ArgumentException($"Must be in >= {nameof(StartGridIndex)}", nameof(EndGridIndex));
            _endGridIndex = value;
        }
    }

    public GridDefinition GridDefinition
    {
        get => _gridDefinition;
        set
        {
            if(value.rowCount < 1 || value.columnCount < 1) 
                throw new ArgumentOutOfRangeException(nameof(GridDefinition), value, "Grid size must be minimum 1x1");
            if(value.rowCount < EndGridIndex.rowIndex + 1) 
                throw new ArgumentException($"Must be > {nameof(EndGridIndex.rowIndex)}", nameof(GridDefinition));
            if(value.columnCount < EndGridIndex.columnIndex + 1) 
                throw new ArgumentException($"Must be > {nameof(EndGridIndex.columnIndex)}", nameof(GridDefinition));

            _gridDefinition = value;
        }
    }
    public override Point TargetSize => _targetSize;

    private readonly Point _targetSize;
    private GridIndex _startGridIndex;
    private GridIndex _endGridIndex;
    private GridDefinition _gridDefinition;
    private Rectangle _bounds = new Rectangle();

    public GridScreen(GameWindow gameWindow, GridDefinition definition, GridIndex startGridIndex, GridIndex endGridIndex, Padding padding) : base(gameWindow, padding)
    {
        GridDefinition = definition;
        EndGridIndex = endGridIndex;
        StartGridIndex = startGridIndex;
        _targetSize = InitTargetSize();
    }

    public GridScreen(GameWindow gameWindow, GridDefinition definition, GridIndex startGridIndex, GridIndex endGridIndex) : 
        this(gameWindow, definition, startGridIndex, endGridIndex, new Padding(0))
    {
    }

    public GridScreen(GameWindow gameWindow, GridDefinition definition, GridIndex gridIndex, Padding padding) : 
        this(gameWindow, definition, gridIndex, gridIndex, padding)
    {
    }

    public GridScreen(GameWindow gameWindow, GridDefinition definition, GridIndex gridIndex) : 
        this(gameWindow, definition, gridIndex, gridIndex, new Padding(0))
    {
    }

    private Point InitTargetSize()
    {
        int width = GameWindow.ClientBounds.Width / GridDefinition.columnCount;
        int height = GameWindow.ClientBounds.Height / GridDefinition.rowCount;
        int screenWidthCount = _endGridIndex.columnIndex - _startGridIndex.columnIndex + 1;
        int screenHeightCount = _endGridIndex.rowIndex - _startGridIndex.rowIndex + 1;
        return new Point(width * screenWidthCount, height * screenHeightCount);
    }
}
