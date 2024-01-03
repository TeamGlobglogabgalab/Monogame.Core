using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Monogame.Core.Windows.Anchors;

public class MiddleLeftAnchor : Anchor
{
    private int _distanceFromMiddle;

    public override Point GetAnchorPosition(Point basePosition, int newWidth, int newHeight)
    {
        _distanceFromMiddle = basePosition.Y - TargetHeight / 2;
        return new Point(basePosition.X, newHeight / 2 + _distanceFromMiddle);
    }

    public override Point GetAnchorPosition(Point basePosition, Point sizeDelta, int newWidth, int newHeight)
    {
        var pos = GetAnchorPosition(basePosition, newWidth, newHeight);
        pos.Y += sizeDelta.Y / 2;
        return pos;
    }
}
