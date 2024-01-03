using Microsoft.Xna.Framework;

namespace MonoGame.Core.Graphics.Origins;

public class BottomLeftOrigin : Origin
{
    public BottomLeftOrigin(Point size) : base(size)
    {
    }

    public override Vector2 CalculateOrigin(Point size)
    {
        return new Vector2(0, size.Y);
    }
}