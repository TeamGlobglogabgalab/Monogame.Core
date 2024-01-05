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
using MonoGame.Core.Graphics.Components;

namespace Monogame.Core.Graphics.Display;

public class DisplayManager : IDisposable
{
    public ContentManager ContentManager { get; private set; }
    public GraphicsDevice GraphicsDevice { get; private set; }
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
    public IScalableContainer ScalableContainer
    {
        get => _scalableContainer;
        set
        {
            _scalableContainer?.Dispose();
            _scalableContainer = value;
            GraphicsRenderer.ScalableContainer = value;
        }
    }

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
    private IScalableContainer _scalableContainer;

    public DisplayManager(Game monoGame)
    {
        _spriteBatch = new SpriteBatch(monoGame.GraphicsDevice);
        var container = new KeepRatioContainer(monoGame.GraphicsDevice, monoGame.Window.ClientBounds.Width, monoGame.Window.ClientBounds.Height);
        var gameScreen = new FullScreen(monoGame.Window);
        Initialize(monoGame.Content, monoGame.GraphicsDevice,
            gameScreen, container,
            new GraphicsRenderer(_spriteBatch, monoGame.GraphicsDevice, monoGame.Content, container, gameScreen));
    }

    public DisplayManager(Game monoGame, IScalableContainer scalableContainer)
    {
        _spriteBatch = new SpriteBatch(monoGame.GraphicsDevice);
        var gameScreen = new FullScreen(monoGame.Window);
        Initialize(monoGame.Content, monoGame.GraphicsDevice,
            gameScreen, scalableContainer, 
            new GraphicsRenderer(_spriteBatch, monoGame.GraphicsDevice, monoGame.Content, scalableContainer, gameScreen));
    }

    public DisplayManager(Game monoGame, IGameScreen gameScreen, IScalableContainer scalableContainer)
    {
        _spriteBatch = new SpriteBatch(monoGame.GraphicsDevice);
        Initialize(monoGame.Content, monoGame.GraphicsDevice, gameScreen, scalableContainer, 
            new GraphicsRenderer(_spriteBatch, monoGame.GraphicsDevice, monoGame.Content, scalableContainer, gameScreen));
    }

    public DisplayManager(Game monoGame, IGameScreen gameScreen, IScalableContainer scalableContainer, IGraphicsRenderer graphicsRenderer)
    {
        _spriteBatch = new SpriteBatch(monoGame.GraphicsDevice);
        Initialize(monoGame.Content, monoGame.GraphicsDevice, gameScreen, scalableContainer, graphicsRenderer);
    }

    public void Begin(Color clearColor, SpriteEffects spriteEffects = SpriteEffects.None)
    {
        IsStarted = true;
        _spriteEffects = spriteEffects;
        ScalableContainer.SetRenderTarget(GameScreen, GraphicsDevice);
        GraphicsDevice.Clear(clearColor);
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
        ScalableContainer.Draw(GameScreen, GraphicsDevice, _spriteBatch, _spriteEffects);
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

    private void Initialize(ContentManager contentManager, GraphicsDevice graphicsDevice, IGameScreen gameScreen, IScalableContainer scalableContainer, IGraphicsRenderer graphicsRenderer)
    {
        IsStarted = false;
        ContentManager = contentManager;
        GraphicsDevice = graphicsDevice;
        GraphicsRenderer = graphicsRenderer;
        _gameScreen = gameScreen;
        _scalableContainer = scalableContainer;
        if (!_scalableContainer.IsReady) _scalableContainer.UpdateTargetSize(GraphicsDevice, _gameScreen.TargetSize.X, _gameScreen.TargetSize.Y);
    }

    private void DrawCollection(GameTime gameTime, ICollection<Drawable> drawables)
    {
        foreach (var d in drawables.OrderBy(d => d.DrawOrder)) Draw(gameTime, d);
    }
}
