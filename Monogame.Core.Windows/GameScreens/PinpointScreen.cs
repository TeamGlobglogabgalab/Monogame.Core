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
            float minScale = GetMinScale();
            _bounds.Width = (int)(_size.X * minScale);
            _bounds.Height = (int)(_size.Y * minScale);

            //Position in parent screen
            if (HasParentScreen)
            {
                Vector2 drawScale = _parentScreen!.ScalableContainer.GetDrawScale(_parentScreen);
                Vector2 drawPos = _parentScreen.ScalableContainer.GetDrawPosition(_parentScreen);
                Point containerSize = _parentScreen.ScalableContainer.RenderingSize;
                Point posInScreen = _anchor.GetAnchorPosition(_position, containerSize.X, containerSize.Y);
                _bounds.X = (int)(posInScreen.X * drawScale.X + drawPos.X);
                _bounds.Y = (int)(posInScreen.Y * drawScale.Y + drawPos.Y);
                return ApplyPadding(_bounds);
            }

            //Position in global window
            var scale = GetScale();
            float maxScale = GetMaxScale();
            float maxScaleIfMinEqual1 = 1 * maxScale / minScale;
            int screenWidth, screenHeight;
            if (scale.X >= scale.Y)
            {
                screenWidth = (int)(_baseWindowSize.X * maxScaleIfMinEqual1);
                screenHeight = _baseWindowSize.Y;
            }
            else
            {
                screenWidth = _baseWindowSize.X;
                screenHeight = (int)(_baseWindowSize.Y * maxScaleIfMinEqual1);
            }
            Point posInWindow = _anchor.GetAnchorPosition(_position, screenWidth, screenHeight);
            _bounds.X = (int)(posInWindow.X * minScale);
            _bounds.Y = (int)(posInWindow.Y * minScale);
            return ApplyPadding(_bounds);
        }
    }

    public override Point TargetSize => _targetSize;

    private bool HasParentScreen => _parentScreen is not null;
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
        Vector2 scale = GetScale();
        return Math.Min(scale.X, scale.Y);
    }

    private float GetMaxScale()
    {
        Vector2 scale = GetScale();
        return Math.Max(scale.X, scale.Y);
    }

    private Vector2 GetScale()
    {
        Point screenSize = GetScreenSize();
        float scaleX = (float)screenSize.X / _baseWindowSize.X;
        float scaleY = (float)screenSize.Y / _baseWindowSize.Y;
        return new Vector2(scaleX * _baseScale.X, scaleY * _baseScale.Y);
    }

    private Point GetScreenSize()
    {
        if (HasParentScreen)
            return new Point(_parentScreen!.ClientBounds.Width, _parentScreen!.ClientBounds.Height);
        return new Point(GameWindow.ClientBounds.Width, GameWindow.ClientBounds.Height);
    }
}
