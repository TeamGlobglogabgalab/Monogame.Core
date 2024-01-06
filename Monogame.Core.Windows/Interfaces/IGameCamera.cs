using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame.Core.Windows;

public interface IGameCamera
{
    public Point Offset { get; }
    public void Move(Point point);
    public void Move(int x, int y);
    public void GoTo(Point point);
    public void GoTo(int x, int y);
}
