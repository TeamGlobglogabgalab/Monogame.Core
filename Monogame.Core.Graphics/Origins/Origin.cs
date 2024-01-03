using Microsoft.Xna.Framework;
using Monogame.Core;

namespace MonoGame.Core.Graphics.Origins;

public abstract class Origin
{
    public float X { get; protected set; }
    public float Y { get; protected set; }

    protected Point Size;

    protected Origin()
    {
    }

    protected Origin(Point size)
    {
        Size = size;
        Update(size);
    }

    public virtual void Update(Point size)
    {
        Size = size;
        var origin = CalculateOrigin(size);
        X = origin.X;
        Y = origin.Y;
    }

    public abstract Vector2 CalculateOrigin(Point size);

    public static Origin operator +(Origin value1, Origin value2)
    {
        value1.X += value2.X;
        value1.Y += value2.Y;
        return value1;
    }

    public static Origin operator -(Origin value1, Origin value2)
    {
        value1.X -= value2.X;
        value1.Y -= value2.Y;
        return value1;
    }
}