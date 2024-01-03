using Microsoft.Xna.Framework;
using Monogame.Core.Tools;
using Monogame.Core.Windows;
using Monogame.Core.Graphics.Exceptions;
using MonoGame.Core.Graphics.Components;
using MonoGame.Core.Graphics.Origins;

namespace Monogame.Core.Graphics.Geometry;

public class DrawableTransform
{
    public Rectangle DestinationRect { get; private set; }
    public Vector2 DestinationPosition { get; private set; }
    public Rectangle? SourceRect { get; private set; } = null;
    public float RotationRadian { get; private set; } = 0;

    public DrawableTransform()
    {
    }

    public DrawableTransform(Drawable drawable, Rectangle rect, IScalableContainer container)
    {
        UpdateTransform(drawable, rect, container);
    }

    public DrawableTransform(Drawable drawable, Vector2 position, IScalableContainer container)
    {
        UpdateTransform(drawable, position, container);
    }

    public void UpdateTransform(Drawable drawable, Rectangle rect, IScalableContainer container)
    {
        //Basic transform without rotation or scale
        if (!IsDrawableRotated(drawable) && !IsDrawableScaled(drawable))
        {
            ApplyBasicTransform(drawable, rect, container);
            return;
        }

        if (!IsDrawableSizeDefined(drawable) && (IsDrawableRotated(drawable) || IsDrawableScaled(drawable)))
            throw new InvalidDrawableException("Drawable size must be defined and valid (1x1 minimum) to calculate scale or rotation");

        //Scale
        var scaledRect = ScaleRectangle(drawable.Scale, drawable.ScaleOrigin, 
            new Rectangle(rect.X, rect.Y, rect.Width, rect.Height));

        //Origin
        scaledRect.X -= (int)drawable.Origin.X;
        scaledRect.Y -= (int)drawable.Origin.Y;

        //Origin within scale
        var scaledSourceRect = ScaleRectangle(drawable.Scale, drawable.ScaleOrigin, 
            new Rectangle(0, 0, drawable.Size.X, drawable.Size.Y));
        scaledSourceRect.X -= (int)drawable.Origin.X;
        scaledSourceRect.Y -= (int)drawable.Origin.Y;
        var newSize = drawable.Origin.CalculateOrigin(new Point(scaledSourceRect.Width, scaledSourceRect.Height));

        //Rotation
        double rotation = MathHelper.ToRadians(drawable.Rotation);
        var c = new Point(scaledSourceRect.X + (int)newSize.X, scaledSourceRect.Y + (int)newSize.Y);
        Vector2 rotatedPosition = GeometryTools.RotatePointAroundCenter(rotation, c, new Point(scaledRect.X, scaledRect.Y));
        RotationRadian = (float)rotation;

        //Anchor
        var anchorPos = container.GetAnchorPosition(drawable.Position, rotatedPosition, drawable.Anchor);
        DestinationRect = new Rectangle(anchorPos.X, anchorPos.Y, scaledRect.Width, scaledRect.Height);
        DestinationPosition = new Vector2(anchorPos.X, anchorPos.Y);

        //Source rectangle
        if (drawable.Size.X != 0 && drawable.Size.X != 0)
            SourceRect = new Rectangle(0, 0, drawable.Size.X, drawable.Size.Y);
    }

    public void UpdateTransform(Drawable drawable, Vector2 position, IScalableContainer container)
    {
        var rect = new Rectangle((int)position.X, (int)position.Y, drawable.Size.X, drawable.Size.Y);
        UpdateTransform(drawable, rect, container);
    }

    private void ApplyBasicTransform(Drawable drawable, Rectangle rect, IScalableContainer container)
    {
        //Origin
        rect.X -= (int)drawable.Origin.X;
        rect.Y -= (int)drawable.Origin.Y;
        
        //TODO : this code should not be used here
        var c = new Point(rect.Width, rect.Height);
        Vector2 rotatedPosition = GeometryTools.RotatePointAroundCenter(0, c, new Point(rect.X, rect.Y));
        
        //Anchor
        var anchorPos = container.GetAnchorPosition(drawable.Position, rotatedPosition, drawable.Anchor);
        DestinationRect = new Rectangle(anchorPos.X, anchorPos.Y, rect.Width, rect.Height);
        DestinationPosition = new Vector2(anchorPos.X, anchorPos.Y);

        //Useless in this case
        SourceRect = null;
        RotationRadian = 0;
    }

    private Rectangle ScaleRectangle(Vector2 scale, Origin scaleOrigin, Rectangle rect)
    {
        var x = (rect.X * scale.X) + (scaleOrigin.X * (1 - scale.X));
        var y = (rect.Y * scale.Y) + (scaleOrigin.Y * (1 - scale.Y));
        var newWidth = rect.Width * scale.X;
        var newHeight = rect.Height * scale.Y;
        return new Rectangle((int)x, (int)y, (int)newWidth, (int)newHeight);
    }

    private bool IsDrawableSizeDefined(Drawable drawable) => drawable.Size.X > 0 && drawable.Size.Y > 0;
    private bool IsDrawableScaled(Drawable drawable) => drawable.Scale.X != 1 || drawable.Scale.Y != 1;
    private bool IsDrawableRotated(Drawable drawable) => drawable.Rotation != 0;
}
