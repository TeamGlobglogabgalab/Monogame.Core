using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Monogame.Core.Demo.UI;
using Monogame.Core.Demo.Window;
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
    public static readonly Color PrimaryColor = new Color().FromHex("#9370DB");

    private GraphicsDeviceManager GraphicsDeviceManager { get; set; }
    private DisplayManager MainDisplay { get; set; }
    private DisplayManager GridDisplay { get; set; }

    /*private ITween _tween;
    private ITweenSequence _tweenSequence;
    private TweenCube _cube;
    private EaseGrid _grid;
    private TextButton _btn;
    private BgTest _bgTest;*/
    private GrowShrinkButton _btn;
    private EaseGrid _grid;
    private ITweenCamera _camera;

    public Demo()
    {
        IsMouseVisible = true;
        Window.AllowUserResizing = true;
        Content.RootDirectory = "Content";

        GraphicsDeviceManager  = new GraphicsDeviceManager(this);
        WindowResizer.SetWindowSize(GraphicsDeviceManager);

        /*_tween = TweenBuilder.From(new Point(100, 300)).To(new Point(666, 100))
            .On(p => _cube.Position = p).For(250f).Back().EaseOut().Build();*/
        _camera = TweenCameraBuilder.For(500).Expo().EaseOut().Build();
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        MainDisplay = new DisplayManager(this,
            gameScreen: new FullScreen(Window, new KeepRatioContainer(GraphicsDevice, WindowResizer.TargetResolution.X, WindowResizer.TargetResolution.Y)));
        GridDisplay = new DisplayManager(this,
            gameScreen: new PinpointScreen(Window, new Point(263, 141), new Point(914, 511), new BottomRightAnchor()),
            camera: _camera);

        /*_cube = new TweenCube(Display1, new Point(100, 300), 2, "#FF4500");
        _grid = new EaseGrid(Display1, new Point(512, 300), new Point(780, 466), 1);
        _btn = new TextButton(Display1, "Gloubi boulga", "Fonts/Roboto", new Point(100, 100), 2);
        _btn.Anchor = new TopLeftAnchor();
        _bgTest = new BgTest(Display1, new Point(0, 0), 0);*/
        _btn = new Logo(MainDisplay, new Point(65, 65), 0);
        _grid = new EaseGrid(GridDisplay, new Point(0, 0), new Point(2438, 1363), 0);
    }

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();

        _camera.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        MainDisplay.Begin(WindowBackColor);
        MainDisplay.Draw(gameTime, _btn);

        GridDisplay.Begin(Color.Black);
        GridDisplay.Draw(gameTime, _grid);
        
        MultipleDisplayManager.EndAll(MainDisplay, GridDisplay);
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