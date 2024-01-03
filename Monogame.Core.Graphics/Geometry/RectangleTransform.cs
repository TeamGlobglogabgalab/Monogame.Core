using Microsoft.Xna.Framework;
using Monogame.Core.Tools;
using MonoGame.Core.Graphics.Origins;

namespace Monogame.Core.Graphics.Geometry;

public class RectangleTransform
{
    public Rectangle Rect
    {
        get => _rect;
        set
        {
            _rect = value;
            UpdatePointsPosition();
        }
    }
    public float Rotation
    {
        get => _rotation;
        set
        {
            _rotation = value;
            UpdatePointsPosition();
        }
    }
    public Origin Origin 
    {
        get => _origin;
        set
        {
            _origin = value;
            UpdatePointsPosition();
        }
    }

    private Rectangle _rect;
    private float _rotation;
    private Origin _origin;
    public readonly Vector2[] _points = new Vector2[4];

    public RectangleTransform(Rectangle rectangle, Origin origin, float rotation = 0f)
    {
        _rect = rectangle;
        _origin = origin;
        _rotation = rotation;
        UpdatePointsPosition();
    }

    public bool Contains(Point point)
    {
        return Contains(new Vector2(point.X, point.Y));
    }

    public bool Contains(Vector2 point)
    {
        //Approximation of rect area (works better for reasons...)
        var rectArea = GeometryTools.CalculateTriangleArea(_points[0], _points[1], _points[2]) +
            GeometryTools.CalculateTriangleArea(_points[0], _points[2], _points[3]);

        //4 triangles possible from point
        var areaA = GeometryTools.CalculateTriangleArea(_points[0], _points[1], point);
        var areaB = GeometryTools.CalculateTriangleArea(_points[1], _points[2], point);
        var areaC = GeometryTools.CalculateTriangleArea(_points[2], _points[3], point);
        var areaD = GeometryTools.CalculateTriangleArea(_points[0], _points[3], point);
        //System.Diagnostics.Debug.WriteLine(rectArea.ToString() + " >= " + (areaA + areaB + areaC + areaD).ToString());
        
        return rectArea >= areaA + areaB + areaC + areaD;
    }

    private void UpdatePointsPosition()
    {
        var localOrigin = Origin.CalculateOrigin(new Point(Rect.Width, Rect.Height));
        _points[0] = new Vector2(Rect.X, Rect.Y) - localOrigin;
        _points[1] = new Vector2(Rect.X + Rect.Width, Rect.Y) - localOrigin;
        _points[2] = new Vector2(Rect.X + Rect.Width, Rect.Y + Rect.Height) - localOrigin;
        _points[3] = new Vector2(Rect.X, Rect.Y + Rect.Height) - localOrigin;
        var globalOrigin = new Vector2(_points[0].X, _points[0].Y);

        var rotationRadian = MathHelper.ToRadians(Rotation);
        for (int i = 0; i < _points.Length; i++)
            _points[i] = GeometryTools.RotatePointAroundCenter(rotationRadian, globalOrigin, _points[i]) + localOrigin;
    }
}