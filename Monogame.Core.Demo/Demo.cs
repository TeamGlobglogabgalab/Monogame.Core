using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Monogame.Core.Demo.UI;
using Monogame.Core.Graphics.Display;
using Monogame.Core.Tweening;
using Monogame.Core.Tweening.Builder;
using Monogame.Core.Tweening.Camera;
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
    private BgTest _bgTest;
    private ITweenCamera _camera;

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
        _tween = TweenBuilder.From(new Point(100, 300)).To(new Point(666, 100))
            .On(p => _cube.Position = p).For(250f).Back().EaseOut().Build();
        _camera = TweenCameraBuilder.For(500).Expo().EaseOut().Build();
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        Display1 = new DisplayManager(this, camera: _camera);
        //Display2 = new DisplayManager(this, new PinpointScreen(Window, new Point(1024-200, 600-150), new Point(200, 150), new Padding(25), new BottomRightAnchor()), new KeepRatioContainer());

        _cube = new TweenCube(Display1, new Point(100, 300), 2, "#FF4500");
        _grid = new EaseGrid(Display1, new Point(512, 300), new Point(780, 466), 1);
        _btn = new TextButton(Display1, MouseComponent, "Gloubi boulga", "Fonts/Roboto", new Point(100, 100), 2);
        _btn.Anchor = new TopLeftAnchor();
        _bgTest = new BgTest(Display1, new Point(0, 0), 0);
    }

    protected override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();
        base.Update(gameTime);

        if (Keyboard.GetState().IsKeyDown(Keys.Space))
            _tween.Start();

        if (Keyboard.GetState().IsKeyDown(Keys.A))
        {
            _tween.Change(_cube.Position, new Point(100, 300), p => _cube.Position = p);
            _tween.Start();
        }
        else if (Keyboard.GetState().IsKeyDown(Keys.Z))
        {
            _tween.Change(_cube.Position, new Point(666, 100), p => _cube.Position = p);
            _tween.Start();
        }
        else if (Keyboard.GetState().IsKeyDown(Keys.E))
        {
            _tween.Change(_cube.Position, new Point(366, 300), p => _cube.Position = p);
            _tween.Start();
        }

        _camera.GoTo(_cube.Position);
        _camera.Update(gameTime);
        _tween.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        Display1.Begin(WindowBackColor);
        Display1.Draw(gameTime, _btn, _cube, _bgTest);

        //Display2.Begin(Color.Green);
        //Display2.Draw(gameTime, _cube2, _cube3);
        
        MultipleDisplayManager.EndAll(Display1);
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