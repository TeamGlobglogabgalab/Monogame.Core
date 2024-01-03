using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame.Core.Tweening;

public interface ISequenceFor<TIn, TTween>
{
    IInterpolation<TTween> For(double milliseconds);
    IInterpolation<TTween> For(params double[] millisecondsArray);
    IInterpolation<TTween> For(ICollection<double> millisecondsCollection);
}
