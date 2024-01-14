using Microsoft.Xna.Framework;
using Monogame.Core.Graphics.Display;
using MonoGame.Core.Graphics;
using MonoGame.Core.Graphics.Components;
using MonoGame.Core.Graphics.Origins;
using System;

namespace Monogame.Core.Graphics.Components;

public abstract class CheckBox : Button
{
    public delegate void CheckBoxEvent(CheckBox checkBox);
    public event CheckBoxEvent CheckChanged;

    public bool Checked
    {
        get => _checked;
        set 
        {
            _checked = value;
            OnCheckChanged();
        }
    }

    private bool _checked = false;

    protected CheckBox(DisplayManager displayManager, Point position, int drawOrder) : 
        base(displayManager, position, drawOrder)
    {
    }

    protected CheckBox(DisplayManager displayManager, Point position, Point size, Origin origin, float rotation, int drawOrder) : 
        base(displayManager, position, size, origin, rotation, drawOrder)
    {
    }

    protected CheckBox(DisplayManager displayManager, Point position, Point size, Origin scaleOrigin, Vector2 scale, int drawOrder) : 
        base(displayManager, position, size, scaleOrigin, scale, drawOrder)
    {
    }

    protected CheckBox(DisplayManager displayManager, Point position, Point size, Origin origin, float rotation, Origin scaleOrigin, Vector2 scale, int drawOrder) : 
        base(displayManager, position, size, origin, rotation, scaleOrigin, scale, drawOrder)
    {
    }

    private void OnCheckChanged() => CheckChanged?.Invoke(this);
}
