using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame.Core.Tweening;

public interface ITween : ITweenBase<ITween>
{
    /// <summary>
    /// Change duration time (milliseconds)
    /// </summary>
    void ChangeDuration(double milliseconds);
    /// <summary>
    /// Change tween values
    /// </summary>
    void Change<T>(T from, T to, Action<T> on);
}
