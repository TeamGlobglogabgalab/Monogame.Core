using Monogame.Core.Tweening.Tweens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame.Core.Tweening;

public interface ICameraFor
{
    ICameraInterpolation For(double milliseconds);
}
