using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame.Core.Tweening;

public interface ISequenceTo<TIn, TTween>
{
    ISequenceFor<TIn, TTween> To(params TIn[] endValues);
    ISequenceFor<TIn, TTween> To(ICollection<TIn> endValues);
}
