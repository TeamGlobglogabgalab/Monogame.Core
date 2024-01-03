using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Monogame.Core.Windows.Anchors;

public class BottomLeftAnchor : Anchor
{
    private int _distanceFromBottom;

    public override Point GetAnchorPosition(Point basePosition, int newWidth, int newHeight)
    {
        _distanceFromBottom = TargetHeight - basePosition.Y;
        return new Point(basePosition.X, newHeight - _distanceFromBottom);
    }

    public override Point GetAnchorPosition(Point basePosition, Point sizeDelta, int newWidth, int newHeight)
    {
        var pos = GetAnchorPosition(basePosition, newWidth, newHeight);
        pos.Y += sizeDelta.Y;
        return pos;
    }
}