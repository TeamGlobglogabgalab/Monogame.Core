using Microsoft.Xna.Framework;

namespace MonoGame.Core.Graphics.Origins;

public class MiddleCenterOrigin : Origin
{
    public MiddleCenterOrigin(Point size) : base(size)
    {
    }

    public override Vector2 CalculateOrigin(Point size)
    {
        return new Vector2((float)size.X / 2f, (float)size.Y / 2f);
    }
}