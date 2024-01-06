using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame.Core.Windows.Camera;

public class GameCamera : IGameCamera
{
    public Point Target => _target;

    private Point _target;

    public GameCamera()
    {
        _target = new Point(0, 0);
    }

    public void Move(Point point)
    {
        Move(point.X, point.Y);
    }

    public void Move(int x, int y)
    {
        _target.X += x;
        _target.Y += y;
    }

    public void GoTo(Point point)
    {
        _target = point;
    }

    public void GoTo(int x, int y)
    {
        _target.X = x;
        _target.Y = y;
    }
}
