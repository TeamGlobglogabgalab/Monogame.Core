using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGame.Core.Windows.Input;

public class MouseComponent
{
    public Point Position => new Point(Mouse.GetState().X, Mouse.GetState().Y);
    public bool LeftDown => Mouse.GetState().LeftButton == ButtonState.Pressed;
    public bool RightDown => Mouse.GetState().RightButton == ButtonState.Pressed;
    public bool LeftPressed
    {
        get
        {
            if (!MouseInsideWindow) return false;
            if (!_leftPressed && LeftDown)
            {
                _leftPressed = true;
                return true;
            }
            else if (!LeftDown) _leftPressed = false;
            return false;
        }
    }
    public bool RightPressed
    {
        get
        {
            if (!MouseInsideWindow) return false;
            if (!_rightPressed && RightDown)
            {
                _rightPressed = true;
                return true;
            }
            else if (!RightDown) _rightPressed = false;
            return false;
        }
    }
    public bool MouseInsideWindow
    {
        get
        {
            var p = Position;
            return p.X >= 0 && p.X <= _window.ClientBounds.Width &&
                p.Y >= 0 && p.Y <= _window.ClientBounds.Height;
        }
    }

    private bool _leftPressed;
    private bool _rightPressed;
    private GameWindow _window;

    public MouseComponent(GameWindow window)
    {
        _window = window;
    }
}