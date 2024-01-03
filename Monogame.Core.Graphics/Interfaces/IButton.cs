using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonoGame.Core.Graphics;

public interface IButton
{
    delegate void ButtonEvent(IButton button);
    event ButtonEvent ButtonClicked;
    event ButtonEvent ButtonReleased;
    event ButtonEvent MouseEnter;
    event ButtonEvent MouseLeave;

    bool IsHovered { get; }
    bool IsClicked { get; }
    void Update();
}