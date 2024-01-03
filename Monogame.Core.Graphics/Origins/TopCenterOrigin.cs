using Microsoft.Xna.Framework;

namespace MonoGame.Core.Graphics.Origins;

public class TopCenterOrigin : Origin
{
    public TopCenterOrigin(Point size) : base(size)
    {
    }

    public override Vector2 CalculateOrigin(Point size)
    {
        return new Vector2((float)size.X / 2f, 0);
    }
}