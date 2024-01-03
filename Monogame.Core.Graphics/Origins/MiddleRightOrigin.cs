using Microsoft.Xna.Framework;

namespace MonoGame.Core.Graphics.Origins;

public class MiddleRightOrigin : Origin
{
    public MiddleRightOrigin(Point size) : base(size)
    {
    }

    public override Vector2 CalculateOrigin(Point size)
    {
        return new Vector2(size.X, (float)size.Y / 2f);
    }
}