using Microsoft.Xna.Framework;
using Monogame.Core.Windows.Anchors;
using Monogame.Core.Windows.Camera;
using Monogame.Core.Windows.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame.Core.Windows.GameScreens;

public class PinpointScreen : GameScreen
{
    public override Rectangle ClientBounds
    {
        get
        {
            var minScale = GetMinScale();
            _bounds.Width = (int)(_size.X * minScale);
            _bounds.Height = (int)(_size.Y * minScale);
            var delta = new Point(_size.X - _bounds.Width, _size.Y - _bounds.Height);
            Point clientBounds = new Point(GameWindow.ClientBounds.Width, GameWindow.ClientBounds.Height);
            //if(_parentContainer is not null) clientBounds = _parentContainer.RenderTargetCurrentSize;
            var pos = _anchor.GetAnchorPosition(_position, delta, clientBounds.X, clientBounds.Y);
            _bounds.X = pos.X;
            _bounds.Y = pos.Y;
            return ApplyPadding(_bounds);
        }
    }

    public override Point TargetSize => _targetSize;

    private readonly Point _targetSize;
    private Point _position;
    private Point _size;
    private Anchor _anchor;
    private Point _baseWindowSize;
    private Rectangle _bounds;
    private readonly IScalableContainer? _parentContainer;

    public PinpointScreen(GameWindow gameWindow, Point position, Point size, Padding padding, Anchor anchor, IScalableContainer parentContainer) : base(gameWindow, padding)
    {
        _position = position;
        _size = size;
        _targetSize = size;
        _baseWindowSize = new Point(gameWindow.ClientBounds.Width, gameWindow.ClientBounds.Height);
        _anchor = anchor;
        _anchor.Initialize(position, _baseWindowSize.X, _baseWindowSize.Y);
        _parentContainer = parentContainer;
        Padding = padding;
    }

    public PinpointScreen(GameWindow gameWindow, Point position, Point size, Padding padding) : 
        this(gameWindow, position, size, padding, new MiddleCenterAnchor(), null)
    {
    }

    public PinpointScreen(GameWindow gameWindow, Point position, Point size, Anchor anchor) :
        this(gameWindow, position, size, new Padding(0), anchor, null)
    {
    }

    public PinpointScreen(GameWindow gameWindow, Point position, Point size) :
        this(gameWindow, position, size, new Padding(0), new MiddleCenterAnchor(), null)
    {
    }

    private float GetMinScale()
    {
        float scaleX = (float)GameWindow.ClientBounds.Width / _baseWindowSize.X;
        float scaleY = (float)GameWindow.ClientBounds.Height / _baseWindowSize.Y;
        return Math.Min(scaleX, scaleY);
    }
}
