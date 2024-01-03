using Microsoft.Xna.Framework;

namespace Monogame.Core.Tools;

public static class GeometryTools
{
    public static Vector2 RotatePointAroundZero(double rotation, Point point)
    {
        return RotatePointAroundCenter(rotation, new Point(0, 0), point);
    }
    
    public static Vector2 RotatePointAroundZero(double rotation, Vector2 point)
    {
        return RotatePointAroundCenter(rotation, new Vector2(0, 0), point);
    }
    
    public static Vector2 RotatePointAroundCenter(double rotation, Point center, Point point)
    {
        return RotatePointAroundCenter(rotation, new Vector2(center.X, center.Y), new Vector2(point.X, point.Y));
    }
    
    public static Vector2 RotatePointAroundCenter(double rotation, Vector2 center, Vector2 point)
    {
        var rx = (int)((point.X - center.X) * Math.Cos(rotation) - (point.Y - center.Y) * Math.Sin(rotation) + center.X);
        var ry = (int)((point.X - center.X) * Math.Sin(rotation) + (point.Y - center.Y) * Math.Cos(rotation) + center.Y);
        return new Vector2(rx, ry);
    }

    public static decimal CalculateTriangleArea(Point a, Point b, Point c)
    {
        return CalculateTriangleArea(new Vector2(a.X, a.Y), new Vector2(b.X, b.Y), new Vector2(c.X, c.Y));
    }
    
    public static decimal CalculateTriangleArea(Vector2 a, Vector2 b, Vector2 c)
    {
        decimal ax = Convert.ToDecimal(a.X);
        decimal ay = Convert.ToDecimal(a.Y);
        decimal bx = Convert.ToDecimal(b.X);
        decimal by = Convert.ToDecimal(b.Y);
        decimal cx = Convert.ToDecimal(c.X);
        decimal cy = Convert.ToDecimal(c.Y);
        return Math.Abs((bx * ay - ax * by) + (cx * by - bx * cy) + (ax * cy - cx * ay)) / 2;
    }
}