using Monogame.Core.Tweening.Camera;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame.Core.Tweening.Builder;

public static class TweenCameraBuilder
{
    public static ICameraInterpolation For(double cameraTweenDurationMs) => new TweenCamera().For(cameraTweenDurationMs);
}
