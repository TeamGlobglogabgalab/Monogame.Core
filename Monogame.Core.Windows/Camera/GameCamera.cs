using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame.Core.Windows.Camera;

class GameCamera : IGameCamera
{
    public Point Offset => _offset;

    private Point _offset;

    public GameCamera()
    {
        _offset = new Point(0, 0);
    }

    public void Move(Point point)
    {
        Move(point.X, point.Y);
    }

    public void Move(int x, int y)
    {
        _offset.X += x;
        _offset.Y += y;
    }
}
