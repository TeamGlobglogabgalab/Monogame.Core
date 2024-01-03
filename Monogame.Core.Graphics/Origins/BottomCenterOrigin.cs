using Microsoft.Xna.Framework;

namespace MonoGame.Core.Graphics.Origins;

public class BottomCenterOrigin : Origin
{
    public BottomCenterOrigin(Point size) : base(size)
    {
    }

    public override Vector2 CalculateOrigin(Point size)
    {
        return new Vector2((float)size.X / 2f, size.Y);
    }
}