using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Monogame.Core.Windows.Anchors;

public class TopCenterAnchor : Anchor
{
    private int _distanceFromCenter;

    public override Point GetAnchorPosition(Point basePosition, int newWidth, int newHeight)
    {
        _distanceFromCenter = basePosition.X - TargetWidth / 2;
        return new Point(newWidth / 2 + _distanceFromCenter, basePosition.Y);
    }
}