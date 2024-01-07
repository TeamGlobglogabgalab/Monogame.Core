using Microsoft.Xna.Framework;
using Monogame.Core.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame.Core.Tweening;

public interface ITweenCamera : IGameCamera
{
    /// <summary>
    /// Move to destination point using tween function
    /// </summary>
    public void MoveTo(Point point);
    /// <summary>
    /// Move to destination (x, y) using tween function
    /// </summary>
    public void MoveTo(int x, int y);
    /// <summary>
    /// Change duration time (milliseconds)
    /// </summary>
    void ChangeDuration(double milliseconds);
    public void Update(GameTime gameTime);
}
