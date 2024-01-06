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
    /// Change duration time (milliseconds)
    /// </summary>
    void ChangeDuration(double milliseconds);
    public void Update(GameTime gameTime);
}
