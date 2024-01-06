using Monogame.Core.Tweening.Tweens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame.Core.Tweening;

public interface ICameraInterpolation
{
    ICameraBuild Linear();
    ICameraEase Sine();
    ICameraEase Quadratic();
    ICameraEase Cubic();
    ICameraEase Quart();
    ICameraEase Quint();
    ICameraEase Expo();
    ICameraEase Circular();
    ICameraEase Back(double intensity = 1);
    ICameraEase Elastic(double intensity = 1);
    ICameraEase Bounce(double intensity = 1);
}
