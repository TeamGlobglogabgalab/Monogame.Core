using Microsoft.Xna.Framework;

namespace MonoGame.Core.Graphics.Origins;

public class TopLeftOrigin : Origin
{
    public TopLeftOrigin()
    {
        X = 0;
        Y = 0;
    }
    
    public TopLeftOrigin(Point size) : base(size)
    {
    }

    public override Vector2 CalculateOrigin(Point size)
    {
        return new Vector2(0, 0);
    }
}