using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame.Core.Graphics.Display;

public static class MultipleDisplayManager
{
    public static void EndAll(ICollection<DisplayManager> displays)
    {
        EndCollection(displays);
    }

    public static void EndAll(params DisplayManager[] displays)
    {
        EndCollection(displays);
    }

    private static void EndCollection(ICollection<DisplayManager> displays)
    {
        foreach (var d in displays) d.InitEndDraw();
        foreach (var d in displays) d.Draw();
        foreach (var d in displays) d.EndDraw();
    }
}
