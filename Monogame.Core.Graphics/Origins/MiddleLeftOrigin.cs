using Microsoft.Xna.Framework;

namespace MonoGame.Core.Graphics.Origins;

public class MiddleLeftOrigin : Origin
{
    public MiddleLeftOrigin(Point size) : base(size)
    {
    }

    public override Vector2 CalculateOrigin(Point size)
    {
        return new Vector2(0, (float)size.Y / 2f);
    }
}