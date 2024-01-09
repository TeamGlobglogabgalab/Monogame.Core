using Microsoft.Xna.Framework;
using Monogame.Core.Tools;
using Monogame.Core.Windows;
using Monogame.Core.Graphics.Exceptions;
using MonoGame.Core.Graphics.Components;
using MonoGame.Core.Graphics.Origins;
using Monogame.Core.Windows.Anchors;

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

    public DrawableTransform(Drawable drawable, Rectangle rect, IScalableContainer container, IGameCamera camera)
    {
        UpdateTransform(drawable, rect, container, camera);
    }

    public DrawableTransform(Drawable drawable, Vector2 position, IScalableContainer container, IGameCamera camera)
    {
        UpdateTransform(drawable, position, container, camera);
    }

    public void UpdateTransform(Drawable drawable, Rectangle rect, IScalableContainer container, IGameCamera camera, bool forceBasicTransform = false)
    {
        //Basic transform without rotation or scale
        if (forceBasicTransform || (!IsDrawableRotated(drawable) && !IsDrawableScaled(drawable)))
        {
            ApplyBasicTransform(drawable.Origin, drawable.Position, drawable.Anchor, rect, container, camera);
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
        Point finalPosition;
        if(drawable.Anchor is not null)
            finalPosition = container.GetAnchorPosition(drawable.Position, rotatedPosition, drawable.Anchor);
        else //Camera
        {
            finalPosition.X = drawable.Position.X + (int)rotatedPosition.X - camera.Target.X + container.RenderingSize.X / 2;
            finalPosition.Y = drawable.Position.Y + (int)rotatedPosition.Y - camera.Target.Y + container.RenderingSize.Y / 2;
        }

        //Destination Rectangle
        DestinationRect = new Rectangle(finalPosition.X, finalPosition.Y, scaledRect.Width, scaledRect.Height);
        DestinationPosition = new Vector2(finalPosition.X, finalPosition.Y);

        //Source rectangle
        if (drawable.Size.X != 0 && drawable.Size.X != 0)
            SourceRect = new Rectangle(0, 0, drawable.Size.X, drawable.Size.Y);
    }

    public void UpdateTransform(Drawable drawable, Vector2 position, IScalableContainer container, IGameCamera camera)
    {
        var rect = new Rectangle((int)position.X, (int)position.Y, drawable.Size.X, drawable.Size.Y);
        UpdateTransform(drawable, rect, container, camera);
    }

    private void ApplyBasicTransform(Origin origin, Point position, Anchor anchor, Rectangle rect, IScalableContainer container, IGameCamera camera)
    {
        //Origin
        rect.X -= (int)origin.X;
        rect.Y -= (int)origin.Y;
        
        //TODO : this code should not be used here
        var c = new Point(rect.Width, rect.Height);
        Vector2 rotatedPosition = GeometryTools.RotatePointAroundCenter(0, c, new Point(rect.X, rect.Y));

        //Anchor
        Point finalPosition;
        if (anchor is not null)
            finalPosition = container.GetAnchorPosition(position, rotatedPosition, anchor);
        else //Camera
        {
            finalPosition.X = position.X + (int)rotatedPosition.X - camera.Target.X + container.RenderingSize.X / 2;
            finalPosition.Y = position.Y + (int)rotatedPosition.Y - camera.Target.Y + container.RenderingSize.Y / 2;
        }
        DestinationRect = new Rectangle(finalPosition.X, finalPosition.Y, rect.Width, rect.Height);
        DestinationPosition = new Vector2(finalPosition.X, finalPosition.Y);

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
