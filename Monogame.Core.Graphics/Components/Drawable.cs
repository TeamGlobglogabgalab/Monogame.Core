using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Core.Extensions;
using Monogame.Core.Windows.Anchors;
using Monogame.Core.Graphics;
using Monogame.Core;
using MonoGame.Core.Graphics.Origins;
using Monogame.Core.Windows;
using Monogame.Core.Graphics.Display;

namespace MonoGame.Core.Graphics.Components;

public abstract class Drawable : IDrawable
{
    public int DrawOrder { get; set; }
    public bool Visible { get; set; }
    /// <summary>
    /// Origin used for position and rotation
    /// </summary>
    public Origin Origin;
    public Origin ScaleOrigin;
    /// <summary>
    /// Rotation in degrees (default: 0)
    /// </summary>
    public float Rotation;
    /// <summary>
    /// Drawable opacity from 0 to 1
    /// </summary>
    public float Opacity
    {
        get => _opacity;
        set
        {
            _opacity = value < 0 ? 0f : (value > 1f ? 1f : value);
        }
    }
    public Vector2 Scale;
    public Anchor Anchor
    {
        get => _anchor;
        set 
        {
            _anchor = value;
            _anchor.Initialize(this.Position, Container.TargetWidth, Container.TargetHeight);
        }
    }
    public Point Position
    {
        get => _position;
        set
        {
            Anchor?.UpdatePosition(value);
            _position = value;
        }
    }
    public Point Size
    {
        get => _size;
        set
        {
            _size = value;
            Origin.Update(_size);
            ScaleOrigin.Update(_size);
        }
    }

    public event EventHandler<EventArgs>? DrawOrderChanged;
    public event EventHandler<EventArgs>? VisibleChanged;

    protected IGraphicsRenderer Graphics => _displayManager.GraphicsRenderer;
    protected IGameScreen Screen => _displayManager.GameScreen;
    protected IGameCamera Camera => _displayManager.Camera;
    protected IScalableContainer Container => _displayManager.ScalableContainer;
    protected ContentManager Content => _displayManager.ContentManager;

    private readonly DisplayManager _displayManager;
    private float _opacity;
    private Anchor _anchor;
    private Point _position;
    private Point _size;

    protected Drawable(DisplayManager displayManager, Point position, int drawOrder) : 
        this(displayManager, position, new Point(0, 0), new TopLeftOrigin(), 0, new TopLeftOrigin(), new Vector2(1, 1), drawOrder)
    {
    }

    protected Drawable(DisplayManager displayManager, Point position, Point size, Origin origin, float rotation, int drawOrder) :
        this(displayManager, position, size, origin, rotation, new TopLeftOrigin(), new Vector2(1, 1), drawOrder)
    {

    }

    protected Drawable(DisplayManager displayManager, Point position, Point size, Origin scaleOrigin, Vector2 scale, int drawOrder) :
        this(displayManager, position, size, new TopLeftOrigin(), 0, scaleOrigin, scale, drawOrder)
    {
    }

    protected Drawable(DisplayManager displayManager, Point position, Point size, Origin origin, float rotation, Origin scaleOrigin, Vector2 scale, int drawOrder)
    {
        _displayManager = displayManager;
        /*_anchor = new MiddleCenterAnchor();
        _anchor.Initialize(position, Container.TargetWidth, Container.TargetHeight);*/

        Position = position;
        DrawOrder = drawOrder;
        Visible = true;
        Origin = origin;
        ScaleOrigin = scaleOrigin;
        Rotation = rotation;
        Opacity = 1f;
        Scale = scale;
        Size = size;
    }

    public void VerifyGraphicsRenderer(IGraphicsRenderer graphicsRenderer)
    {
        if(graphicsRenderer != this.Graphics)
            throw new ArgumentException("Specified drawable doesn't belong to this DisplayManager");
    }

    public abstract void Draw(GameTime gameTime);
}