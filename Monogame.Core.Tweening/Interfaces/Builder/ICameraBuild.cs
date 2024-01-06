using Monogame.Core.Tweening.Camera;
using Monogame.Core.Tweening.Tweens;
using Monogame.Core.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame.Core.Tweening;

public interface ICameraBuild
{
    ITweenCamera Build();
}
