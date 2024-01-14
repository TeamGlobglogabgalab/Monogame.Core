using Microsoft.Xna.Framework;
using Monogame.Core;
using MonoGame.Core.Graphics.Origins;
using Monogame.Core.Graphics.Geometry;
using Monogame.Core.Graphics.Display;
using MonoGame.Core.Windows.Input;
using Microsoft.Xna.Framework.Input;

namespace MonoGame.Core.Graphics.Components;

public abstract class Button : Drawable, IButton
{
    public event IButton.ButtonEvent ButtonClicked;
    public event IButton.ButtonEvent ButtonReleased;
    public event IButton.ButtonEvent MouseEnter;
    public event IButton.ButtonEvent MouseLeave;
    public event IButton.ButtonEvent EnabledChanged;

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
    public bool Enabled
    {
        get => _enabled;
        set
        {
            _enabled = value;
            OnEnabledChanged();
        }
    }
    public RectangleTransform WindowBoundingBox
    {
        get
        {
            _drawableTransform.UpdateTransform(this, BoundingBox, Container, Camera);
            var rect = Container.GetBoundingBoxRectangle(Screen, _drawableTransform.DestinationRect);
            return new RectangleTransform(rect, this.Origin, this.Rotation);
        }
    }
    public bool UseHandCursor = true;

    protected MouseComponent MouseComponent;
    protected abstract Rectangle BoundingBox { get; }

    private bool _isHovered = false;
    private bool _hasBeenClicked = false;
    private DrawableTransform _drawableTransform = new DrawableTransform();
    private bool _enabled;

    protected Button(DisplayManager displayManager, Point position, int drawOrder) :
        this(displayManager, position, new Point(0, 0), new TopLeftOrigin(), 0, new TopLeftOrigin(), new Vector2(1, 1), drawOrder)
    {
    }

    protected Button(DisplayManager displayManager, Point position, Point size, Origin origin, float rotation, int drawOrder) :
        this(displayManager, position, size, origin, rotation, new TopLeftOrigin(), new Vector2(1, 1), drawOrder)
    {
    }

    protected Button(DisplayManager displayManager, Point position, Point size, Origin scaleOrigin, Vector2 scale, int drawOrder) :
        this(displayManager, position, size, new TopLeftOrigin(), 0, scaleOrigin, scale, drawOrder)
    {
    }

    protected Button(DisplayManager displayManager, Point position, Point size, Origin origin, float rotation, Origin scaleOrigin, Vector2 scale, int drawOrder) :
        base(displayManager, position, size, origin, rotation, scaleOrigin, scale, drawOrder)
    {
        _enabled = true;
        MouseComponent = new MouseComponent(displayManager.GameWindow);
        MouseEnter += (button) => { if (UseHandCursor) Mouse.SetCursor(MouseCursor.Hand); };
        MouseLeave += (button) => { if (UseHandCursor) Mouse.SetCursor(MouseCursor.Arrow); };
    }

    public void Update()
    {
        //Click
        if (IsClicked) OnButtonClicked();
        else if (_hasBeenClicked && !MouseComponent.LeftDown)
        {
            _hasBeenClicked = false;
            OnButtonRelease();
        }

        //Hover
        if (IsHovered)
        {
            if (!_isHovered) OnMouseEnter();
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

    private void OnButtonClicked()
    {
        if (_enabled) ButtonClicked?.Invoke(this);
    }
    private void OnButtonRelease() 
    {
        if(_enabled) ButtonReleased?.Invoke(this); 
    }
    private void OnMouseEnter()
    {
        if (_enabled) MouseEnter?.Invoke(this);
    }
    private void OnMouseLeave()
    {
        if (_enabled) MouseLeave?.Invoke(this);
    }
    private void OnEnabledChanged() => EnabledChanged?.Invoke(this);
}
