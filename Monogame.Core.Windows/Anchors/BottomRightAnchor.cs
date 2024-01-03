using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Monogame.Core.Windows.Anchors;

public class BottomRightAnchor : Anchor
{
    private int _distanceFromRightSide;
    private int _distanceFromBottom;

    public override Point GetAnchorPosition(Point basePosition, int newWidth, int newHeight)
    {
        _distanceFromRightSide = TargetWidth - basePosition.X;
        _distanceFromBottom = TargetHeight - basePosition.Y;
        return new Point(newWidth - _distanceFromRightSide, newHeight - _distanceFromBottom);
    }

    public override Point GetAnchorPosition(Point basePosition, Point sizeDelta, int newWidth, int newHeight)
    {
        return GetAnchorPosition(basePosition, newWidth, newHeight) + sizeDelta;
    }
}