using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monogame.Core.Graphics.Display;
using MonoGame.Core.Graphics.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame.Core.Demo.UI;

class BgTest : Drawable
{
    private Texture2D _bgTexture;

    public BgTest(DisplayManager displayManager, Point position, int drawOrder) : base(displayManager, position, drawOrder)
    {
        _bgTexture = Content.Load<Texture2D>("bg");
    }

    public override void Draw(GameTime gameTime)
    {
        Graphics.Draw(this, _bgTexture, new Rectangle(0, 0, 1024, 600), gameTime);
    }
}
