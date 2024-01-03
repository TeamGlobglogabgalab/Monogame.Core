using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Monogame.Core;

namespace Monogame.Core.Windows.Anchors;

public abstract class Anchor
{
    protected Point BasePosition;
    protected int TargetWidth;
    protected int TargetHeight;
    
    public void Initialize(Point basePosition, int targetWidth, int targetHeight)
    {
        BasePosition = basePosition;
        TargetWidth = targetWidth;
        TargetHeight = targetHeight;
    }

    public void UpdatePosition(Point location)
    {
        BasePosition = location;
    }

    public abstract Point GetAnchorPosition(Point basePosition, int newWidth, int newHeight);
    public abstract Point GetAnchorPosition(Point basePosition, Point sizeDelta, int newWidth, int newHeight);
}
