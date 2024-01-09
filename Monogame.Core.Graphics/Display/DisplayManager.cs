using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Core.Graphics;
using Monogame.Core.Windows;
using Monogame.Core.Windows.Containers;
using Monogame.Core.Windows.GameScreens;
using Monogame.Core.Windows.Camera;
using MonoGame.Core.Graphics.Components;
using System.ComponentModel;

namespace Monogame.Core.Graphics.Display;

public class DisplayManager : IDisposable
{
    public ContentManager ContentManager { get; private set; }
    public GraphicsDevice GraphicsDevice { get; private set; }
    public GameWindow GameWindow { get; private set; }
    public IGraphicsRenderer GraphicsRenderer { get; private set; }
    public IGameScreen GameScreen
    {
        get => _gameScreen;
        set
        {
            _gameScreen?.Dispose();
            _gameScreen = value;
        }
    }
    public IGameCamera Camera
    {
        get => _camera;
        set
        {
            GraphicsRenderer.Camera = value;
        }
    }

    private IScalableContainer ScalableContainer => _gameScreen.ScalableContainer;
    private bool IsStarted
    {
        get => _isStarted;
        set
        {
            _isStarted = value;
            if(_isStarted) GraphicsRenderer?.Resume();
            else GraphicsRenderer?.Suspend();
        }
    }
    private bool _isStarted;
    private SpriteBatch _spriteBatch;
    private SpriteEffects _spriteEffects;
    private IGameScreen _gameScreen;
    private IGameCamera _camera;

    public DisplayManager(Game monoGame) : this(monoGame, null, null, null)
    {
    }

    public DisplayManager(Game monoGame, IGameScreen gameScreen) :
        this(monoGame, gameScreen, null, null)
    {
    }

    public DisplayManager(Game monoGame, IGameScreen gameScreen, IGraphicsRenderer graphicsRenderer) :
        this(monoGame, gameScreen, graphicsRenderer, new GameCamera())
    {
    }

    public DisplayManager(Game monoGame, IGameScreen gameScreen = null, IGraphicsRenderer graphicsRenderer = null, IGameCamera camera = null)
    {
        _spriteBatch = new SpriteBatch(monoGame.GraphicsDevice);
        _gameScreen = gameScreen ?? new FullScreen(monoGame.Window);

        if (!ScalableContainer.IsReady)
            ScalableContainer.UpdateTargetSize(monoGame.GraphicsDevice, _gameScreen.TargetSize.X, _gameScreen.TargetSize.Y);

        _camera = camera ?? new GameCamera();
        _camera.GoTo(new Point(ScalableContainer.RenderingSize.X / 2, ScalableContainer.RenderingSize.Y / 2));

        ContentManager = monoGame.Content;
        GraphicsDevice = monoGame.GraphicsDevice;
        GraphicsRenderer = graphicsRenderer ?? new GraphicsRenderer(_spriteBatch, monoGame.GraphicsDevice, monoGame.Content, ScalableContainer, _camera);
        GraphicsRenderer.Camera = _camera;
        GameWindow = monoGame.Window;
        
        IsStarted = false;
    }

    public void Begin(Color clearColor, SpriteEffects spriteEffects = SpriteEffects.None)
    {
        _spriteEffects = spriteEffects;
        ScalableContainer.SetRenderTarget(GameScreen, GraphicsDevice);
        GraphicsDevice.Clear(clearColor);
        IsStarted = true;
    }

    public void End()
    {
        InitEndDraw();
        Draw();
        EndDraw();
        IsStarted = false;
    }

    public void InitEndDraw()
    {
        IsStarted = true;
        GraphicsDevice.SetRenderTarget(null);
        _spriteBatch.Begin();
    }

    public void Draw()
    {
        _gameScreen.Draw(GraphicsDevice, _spriteBatch, _spriteEffects);
    }

    public void EndDraw()
    {
        _spriteBatch.End();
        IsStarted = false;
    }

    public void Draw(GameTime gameTime, Drawable drawable)
    {
        drawable.VerifyGraphicsRenderer(this.GraphicsRenderer);
        if (drawable.Visible) drawable.Draw(gameTime);
    }

    public void Draw(GameTime gameTime, ICollection<Drawable> drawables)
    {
        DrawCollection(gameTime, drawables);
    }

    public void Draw(GameTime gameTime, params Drawable[] drawables)
    {
        DrawCollection(gameTime, drawables);
    }

    public void Dispose()
    {
        _spriteBatch?.Dispose();
        GC.SuppressFinalize(this);
    }

    private void DrawCollection(GameTime gameTime, ICollection<Drawable> drawables)
    {
        foreach (var d in drawables.OrderBy(d => d.DrawOrder)) Draw(gameTime, d);
    }
}
