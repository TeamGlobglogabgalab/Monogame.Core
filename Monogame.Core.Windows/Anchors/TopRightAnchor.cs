using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame.Core.Windows.Anchors;

public class TopRightAnchor : Anchor
{
    private int _distanceFromRightSide;

    public override Point GetAnchorPosition(Point basePosition, int newWidth, int newHeight)
    {
        _distanceFromRightSide = TargetWidth - basePosition.X;
        return new Point(newWidth - _distanceFromRightSide, basePosition.Y);
    }

    public override Point GetAnchorPosition(Point basePosition, Point sizeDelta, int newWidth, int newHeight)
    {
        var pos = GetAnchorPosition(basePosition, newWidth, newHeight);
        pos.X += sizeDelta.X;
        return pos;
    }
}
