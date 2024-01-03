using Microsoft.Xna.Framework;

namespace MonoGame.Core.Graphics.Origins;

public class CustomOrigin : Origin
{
    public CustomOrigin(float x, float y)
    {
        X = x;
        Y = y;
    }

    public override Vector2 CalculateOrigin(Point size)
    {
        return new Vector2(X, Y);
    }
}