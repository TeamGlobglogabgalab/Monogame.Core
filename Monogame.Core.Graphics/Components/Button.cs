using Microsoft.Xna.Framework;
using Monogame.Core;
using MonoGame.Core.Graphics.Origins;
using Monogame.Core.Graphics.Geometry;
using Monogame.Core.Graphics.Display;
using MonoGame.Core.Windows.Input;

namespace MonoGame.Core.Graphics.Components;

public abstract class Button : Drawable, IButton
{
    public event IButton.ButtonEvent? ButtonClicked;
    public event IButton.ButtonEvent? ButtonReleased;
    public event IButton.ButtonEvent? MouseEnter;
    public event IButton.ButtonEvent? MouseLeave;

    public bool IsClicked
    {
        get 
        {
            var clicked = MouseComponent.LeftPressed && IsHovered;
            if (clicked) _hasBeenClicked = true;
            return clicked;
        }
    }
    public bool IsHovered => WindowBoundingBox.Contains(MouseComponent.Position);

    protected MouseComponent MouseComponent;
    protected abstract Rectangle BoundingBox { get; }
    
    public RectangleTransform WindowBoundingBox
    {
        get
        {
            _drawableTransform.UpdateTransform(this, BoundingBox, Container);
            var rect = Container.GetBoundingBoxRectangle(Screen, _drawableTransform.DestinationRect);
            return new RectangleTransform(rect, this.Origin, this.Rotation);
        }
    }
    private bool _isHovered = false;
    private bool _hasBeenClicked = false;
    private DrawableTransform _drawableTransform = new DrawableTransform();

    protected Button(DisplayManager displayManager, MouseComponent mouseComponent, Point position, int drawOrder) : 
        base(displayManager, position, drawOrder)
    {
        MouseComponent = mouseComponent;
    }

    protected Button(DisplayManager displayManager, MouseComponent mouseComponent, Point position, Point size, Origin origin, float rotation, int drawOrder) : 
        base(displayManager, position, size, origin, rotation, drawOrder)
    {
        MouseComponent = mouseComponent;
    }

    protected Button(DisplayManager displayManager, MouseComponent mouseComponent, Point position, Point size, Origin scaleOrigin, Vector2 scale, int drawOrder) : 
        base(displayManager, position, size, scaleOrigin, scale, drawOrder)
    {
        MouseComponent = mouseComponent;
    }

    protected Button(DisplayManager displayManager, MouseComponent mouseComponent, Point position, Point size, Origin origin, float rotation, Origin scaleOrigin, Vector2 scale, int drawOrder) : 
        base(displayManager, position, size, origin, rotation, scaleOrigin, scale, drawOrder)
    {
        MouseComponent = mouseComponent;
    }

    public void Update()
    {
        //Click
        if (IsClicked) OnButtonClicked();
        else if(_hasBeenClicked && !MouseComponent.LeftDown)
        {
            _hasBeenClicked = false;
            OnButtonRelease();
        }

        //Hover
        if (IsHovered)
        {
            if(!_isHovered) OnMouseEnter();
            _isHovered = true;
        }
        else
        {
            if (_isHovered) OnMouseLeave();
            _isHovered = false;
        }
    }
    public override void Draw(GameTime gameTime)
    {
        Update();
    }

    private void OnButtonClicked() => ButtonClicked?.Invoke(this);
    private void OnButtonRelease() => ButtonReleased?.Invoke(this);
    private void OnMouseEnter() => MouseEnter?.Invoke(this);
    private void OnMouseLeave() => MouseLeave?.Invoke(this);
}
