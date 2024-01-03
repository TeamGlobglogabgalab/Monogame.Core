using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame.Core.Tweening;

public interface ILoop<TTween>
{
    IBuild<TTween>Loop(uint delay = 0);
    IBuild<TTween>Loop(uint delay, uint iterationCount);
    IBuild<TTween>LoopAlternate(uint delay = 0);
    IBuild<TTween>LoopAlternate(uint delay, uint iterationCount);
    TTween Build();
}
