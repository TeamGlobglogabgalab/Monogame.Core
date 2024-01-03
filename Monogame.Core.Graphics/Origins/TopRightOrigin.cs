using Microsoft.Xna.Framework;

namespace MonoGame.Core.Graphics.Origins;

public class TopRightOrigin : Origin
{
    public TopRightOrigin(Point size) : base(size)
    {
    }

    public override Vector2 CalculateOrigin(Point size)
    {
        return new Vector2 (size.X, 0);
    }
}