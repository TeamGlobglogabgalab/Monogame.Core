using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame.Core.Tweening;

public interface ITweenSequence : ITweenBase<ITweenSequence>
{
    /// <summary>
    /// Change duration time for all sequences (milliseconds)
    /// </summary>
    void ChangeDurations(double milliseconds);
    /// <summary>
    /// Change duration time for all sequences (milliseconds)
    /// </summary>
    void ChangeDurations(params double[] millisecondsArray);
    /// <summary>
    /// Change duration time for all sequences (milliseconds)
    /// </summary>
    void ChangeDurations(ICollection<double> millisecondsCollection);
}
