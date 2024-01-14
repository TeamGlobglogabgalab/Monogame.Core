using Microsoft.Xna.Framework;
using Monogame.Core.Graphics.Components;
using Monogame.Core.Graphics.Display;
using MonoGame.Core.Graphics.Components;
using MonoGame.Core.Graphics.Origins;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Monogame.Core.Demo.Events;

public class RadioGroupHandler
{
    public List<CheckBox> CheckBoxes;

    public RadioGroupHandler(params CheckBox[] checkBoxes)
    {
        CheckBoxes = checkBoxes.ToList();
        foreach (var cb in CheckBoxes)
        {
            cb.Checked = false;
            cb.ButtonClicked += (button) =>
            {
                var currentCheckBox = button as CheckBox;
                CheckBoxes.ForEach(c => c.Checked = c == currentCheckBox);
            };
        }
        checkBoxes[0].Checked = true;
    }
}
