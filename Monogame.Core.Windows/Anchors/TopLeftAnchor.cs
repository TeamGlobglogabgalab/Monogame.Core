using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Monogame.Core.Windows.Anchors;

public class TopLeftAnchor : Anchor
{
    public override Point GetAnchorPosition(Point basePosition, int newWidth, int newHeight)
    {
        return basePosition;
    }

    public override Point GetAnchorPosition(Point basePosition, Point sizeDelta, int newWidth, int newHeight)
    {
        return GetAnchorPosition(basePosition, newWidth, newHeight);
    }
}
