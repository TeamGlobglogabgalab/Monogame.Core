using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monogame.Core.Graphics.Components;
using Monogame.Core.Graphics.Display;
using Monogame.Core.Tweening;
using Monogame.Core.Tweening.Builder;
using MonoGame.Core.Extensions;

namespace Monogame.Core.Demo.UI;

class CodePreviewTabs : CheckBox
{
    protected override Rectangle BoundingBox => _boundingBox;

    private Color _fontColor = Color.White;
    private Color _backgroundColorChecked = Demo.PrimaryColor;
    private Color _backgroundColorUnchecked = new Color().FromHex("#595959");
    private Color _backgroundColor;
    private string _text;
    private Vector2 _textPosition;
    private SpriteFont _font;
    private SpriteFont _fontChecked;
    private SpriteFont _fontUnchecked;
    private ITween _tweenOpacity;
    private Rectangle _boundingBox;

    public CodePreviewTabs(DisplayManager displayManager, string text, Point position, int drawOrder) : 
        base(displayManager, position, drawOrder)
    {
        _text = text;
        _fontChecked = Content.Load<SpriteFont>("Fonts/Roboto-Medium-56");
        _fontUnchecked = Content.Load<SpriteFont>("Fonts/Roboto-Light-56");

        Size = new Point(265, 88);
        Opacity = 0.8f;
        _boundingBox = new Rectangle(0, 0, Size.X, Size.Y);

        CheckChanged += (check) =>
        {
            if(check.Checked)
            {
                _font = _fontChecked;
                _backgroundColor = _backgroundColorChecked;
            }
            else
            {
                _font = _fontUnchecked;
                _backgroundColor = _backgroundColorUnchecked;
            }
            Vector2 textSize = _font.MeasureString(_text);
            _textPosition = new Vector2(Size.X / 2f - textSize.X / 2f, Size.Y / 2f - textSize.Y / 2f);
        };

        _tweenOpacity = TweenBuilder.OnOpacity(this).To(1).For(300f).Quart().EaseOut().Build();
        MouseEnter += (button) => _tweenOpacity.GoForward();
        MouseLeave += (button) => _tweenOpacity.GoBackward();
    }

    public override void Draw(GameTime gameTime)
    {
        Opacity = _tweenOpacity.Update(gameTime);
        Graphics.DrawRectangle(this, _backgroundColor, _boundingBox, gameTime);
        Graphics.DrawString(this, _font, _text, _textPosition, _fontColor, gameTime);
        base.Draw(gameTime);
    }
}
