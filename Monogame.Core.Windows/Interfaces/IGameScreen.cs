using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Monogame.Core.Windows.Structs;

namespace Monogame.Core.Windows;

public interface IGameScreen : IDisposable
{
    public Point TargetSize { get; }
    public Rectangle ClientBounds { get; }
    public float ScreenRatio { get; }
    public Padding Padding { get; set; }
}
