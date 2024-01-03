using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Monogame.Core.Graphics.Display;
using Monogame.Core.Tweening;
using Monogame.Core.Tweening.Builder;
using Monogame.Core.Tweening.UI;
using Monogame.Core.Windows.Anchors;
using Monogame.Core.Windows.Containers;
using Monogame.Core.Windows.GameScreens;
using Monogame.Core.Windows.Structs;
using MonoGame.Core.Extensions;
using MonoGame.Core.Windows.Input;

namespace Monogame.Core.Demo;

public class Demo : Game
{
    public static readonly Color WindowBackColor = new Color().FromHex("#141414");

    private GraphicsDeviceManager GraphicsDeviceManager { get; set; }
    private DisplayManager Display1 { get; set; }
    private DisplayManager Display2 { get; set; }
    private MouseComponent MouseComponent { get; set; }

    private ITween _tween;
    private ITweenSequence _tweenSequence;
    private TweenCube _cube;
    private EaseGrid _grid;
    private TextButton _btn;

    private TweenCube _cube2;
    private TweenCube _cube3;
    private EaseGrid _grid2;

    public Demo()
    {
        IsMouseVisible = true;
        Window.AllowUserResizing = true;
        Content.RootDirectory = "Content";

        GraphicsDeviceManager  = new GraphicsDeviceManager(this);
        GraphicsDeviceManager.PreferredBackBufferWidth = 1024;
        GraphicsDeviceManager.PreferredBackBufferHeight = 600;
        GraphicsDeviceManager.ApplyChanges();

        MouseComponent = new MouseComponent(this.Window);
        _tween = TweenBuilder.From(new Point(100, 100)).To(new Point(666, 100))
            .On(p => _cube.Position = p).For(666f).Bounce().EaseOut().Build();
        _tweenSequence = TweenSequenceBuilder.From(new Point(100, 100))
            .To(new Point(300, 500), new Point(600, 300), new Point(800, 100))
            .For(1500f).Bounce(0.5f).EaseOut().Build();
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        Display1 = new DisplayManager(this, new KeepRatioContainer());
        Display2 = new DisplayManager(this, new PinpointScreen(Window, new Point(1024-200, 600-150), new Point(200, 150), new Padding(25), new BottomRightAnchor()), new KeepRatioContainer());

        _cube = new TweenCube(Display1, new Point(100, 100), 0, "#FF4500");
        _grid = new EaseGrid(Display1, new Point(512, 300), new Point(780, 466), 0);
        _btn = new TextButton(Display1, MouseComponent, "Gloubi boulga", "Fonts/Roboto", new Point(350, 250), 0);

        _cube2 = new TweenCube(Display2, new Point(50, 50), 0, "#FF4500");
        _cube3 = new TweenCube(Display2, new Point(462, 250), 0, "#FF4500");
        _grid2 = new EaseGrid(Display2, new Point(220, 116), new Point(780, 466), 0);
    }

    protected override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();
        base.Update(gameTime);

        if (Keyboard.GetState().IsKeyDown(Keys.Space))
            _tweenSequence.Start();

        if (Keyboard.GetState().IsKeyDown(Keys.S))
            Display1.ScalableContainer = new StretchContainer(GraphicsDevice, 1024, 600);
        else if (Keyboard.GetState().IsKeyDown(Keys.K))
            Display1.ScalableContainer = new KeepRatioContainer(GraphicsDevice, 1024, 600);
        else if (Keyboard.GetState().IsKeyDown(Keys.E))
            _tweenSequence.GoForward();
            //Display1.ScalableContainer = new StretchExpandContainer(GraphicsDevice, 1024, 600);
        else if (Keyboard.GetState().IsKeyDown(Keys.R))
            _tweenSequence.GoBackward();
            //_btn.Scale = new Vector2(_btn.Scale.X - 0.05f, _btn.Scale.Y - 0.05f);
        else if (Keyboard.GetState().IsKeyDown(Keys.E))
            _btn.Scale = new Vector2(_btn.Scale.X + 0.05f, _btn.Scale.Y + 0.05f);
        else if (Keyboard.GetState().IsKeyDown(Keys.D))
            _btn.Rotation -= 1;
        else if (Keyboard.GetState().IsKeyDown(Keys.F))
            _btn.Rotation += 1;

        _cube.Position = _tweenSequence.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        Display1.Begin(WindowBackColor);
        Display1.Draw(gameTime, _btn, _cube);

        Display2.Begin(Color.Green);
        Display2.Draw(gameTime, _cube2, _cube3);
        
        MultipleDisplayManager.EndAll(Display1, Display2);
        /*var texture = new Texture2D(GraphicsDevice, 1, 1);
        texture.SetData(new[] { Color.Red });
        var r = _btn.WindowBoundingBox;
        var sp = new SpriteBatch(GraphicsDevice);
        sp.Begin();
        sp.Draw(texture,
            new Rectangle((int)r._points[0].X, (int)r._points[0].Y, 2, 2), Color.White);
        sp.Draw(texture,
            new Rectangle((int)r._points[1].X, (int)r._points[1].Y, 2, 2), Color.White);
        sp.Draw(texture,
            new Rectangle((int)r._points[2].X, (int)r._points[2].Y, 2, 2), Color.White);
        sp.Draw(texture,
            new Rectangle((int)r._points[3].X, (int)r._points[3].Y, 2, 2), Color.White);
        sp.End();
        
        Display1.End();*/

        base.Draw(gameTime);
    }
}