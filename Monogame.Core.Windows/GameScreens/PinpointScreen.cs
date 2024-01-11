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
            /*var minScale = GetMinScale();
            _bounds.Width = (int)(_size.X * minScale);
            _bounds.Height = (int)(_size.Y * minScale);
            var delta = new Point(_size.X - _bounds.Width, _size.Y - _bounds.Height);
            Point screenSize = GetScreenSize();
            var pos = _anchor.GetAnchorPosition(_position, delta, screenSize.X, screenSize.Y);
            _bounds.X = pos.X;
            _bounds.Y = pos.Y;
            return ApplyPadding(_bounds);*/
            float minScale = GetMinScale();
            _bounds.Width = (int)(_size.X * minScale);
            _bounds.Height = (int)(_size.Y * minScale);

            if(_parentScreen is null)
            {
                Point screenSize = GetScreenSize();
                Vector2 scale = new Vector2((float)screenSize.X / (float)_baseWindowSize.X, (float)screenSize.Y / (float)_baseWindowSize.Y);
                //Point delta = new Point((int)((_size.X - _bounds.Width) * scale.X), (int)((_size.Y - _bounds.Height) * scale.Y));
                Point delta = new Point((int)((_size.X - _bounds.Width) * scale.X), (int)((_size.Y - _bounds.Height) * scale.Y));
                _position = new Point((int)(_position.X * scale.X), (int)(_position.Y * scale.Y));
                _anchor.UpdateSize(screenSize.X, screenSize.Y);
                Point pos = _anchor.GetAnchorPosition(_position, screenSize.X, screenSize.Y);
                _bounds.X = (int)(pos.X * scale.X);
                _bounds.Y = (int)(pos.Y * scale.Y);
            }
            else
            {
                Vector2 drawScale = _parentScreen!.ScalableContainer.GetDrawScale(_parentScreen);
                Vector2 drawPos = _parentScreen.ScalableContainer.GetDrawPosition(_parentScreen);
                Point containerSize = _parentScreen.ScalableContainer.RenderingSize;
                Point pos = _anchor.GetAnchorPosition(_position, containerSize.X, containerSize.Y);
                _bounds.X = (int)(pos.X * drawScale.X + drawPos.X);
                _bounds.Y = (int)(pos.Y * drawScale.Y + drawPos.Y);
            }
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
    private IGameScreen? _parentScreen;
    private Vector2 _baseScale = new Vector2(1f, 1f);

    public PinpointScreen(GameWindow gameWindow, Point position, Point size, Anchor anchor, Padding padding) : base(gameWindow, GetDefaultContainer(), padding)
    {
        _position = position;
        _size = size;
        _targetSize = size;
        _baseWindowSize = GetScreenSize();
        _anchor = anchor;
        _anchor.Initialize(position, _baseWindowSize.X, _baseWindowSize.Y);
        Padding = padding;
    }

    public PinpointScreen(GameWindow gameWindow, Point position, Point size, Anchor anchor) :
        this(gameWindow, position, size, anchor, new Padding(0))
    {
    }

    public PinpointScreen(IGameScreen parentScreen, Point position, Point size, Anchor anchor, Padding padding) : 
        this(parentScreen.GameWindow,  position, size, anchor, padding)
    {
        _parentScreen = parentScreen;
        _anchor.Initialize(position, _parentScreen.ScalableContainer.TargetWidth, _parentScreen.ScalableContainer.TargetHeight);
        _baseScale = _parentScreen.ScalableContainer.GetDrawScale(_parentScreen);
    }

    private float GetMinScale()
    {
        Point screenSize = GetScreenSize();
        float scaleX = (float)screenSize.X / _baseWindowSize.X;
        float scaleY = (float)screenSize.Y / _baseWindowSize.Y;
        return Math.Min(scaleX * _baseScale.X, scaleY * _baseScale.Y);
    }

    private Point GetScreenSize()
    {
        if (_parentScreen is null)
            return new Point(GameWindow.ClientBounds.Width, GameWindow.ClientBounds.Height);
        return new Point(_parentScreen.ClientBounds.Width, _parentScreen.ClientBounds.Height);
    }
}
