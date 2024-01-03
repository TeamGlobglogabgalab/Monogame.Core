using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monogame.Core.Tweening.Structs;

namespace Monogame.Core.Tweening;

public interface ITweenBase<TTween>
{
    delegate void TweenEvent(TTween tween);
    event TweenEvent? AnimationEnded;

    /// <summary>
    /// Is Tween started (go back to false when animation ended)
    /// </summary>
    bool IsStarted { get; }
    /// <summary>
    /// Is Tween reversed (by calling Reverse() method)
    /// </summary>
    bool IsReversed { get; }
    /// <summary>
    /// Start the tween from current time elapsed
    /// </summary>
    void Start();
    /// <summary>
    /// Reverse tween if reversed, then start
    /// </summary>
    void GoForward();
    /// <summary>
    /// Reverse tween if not reversed, then start
    /// </summary>
    void GoBackward();
    /// <summary>
    /// Update tween step using current time in milliseconds and returns current value
    /// </summary>
    TweenValue Update(double elapsedTimeMs);
    /// <summary>
    /// Update tween step using Monogame GameTime and returns current value
    /// </summary>
    TweenValue Update(GameTime gameTime);
    /// <summary>
    /// Reset time elapsed
    /// </summary>
    void Reset(bool resetLoops = true);
    /// <summary>
    /// Reset time elapsed then start the tween
    /// </summary>
    void Restart();
    /// <summary>
    /// Swap endValues and startvalues
    /// </summary>
    void Reverse();
    /// <summary>
    /// Pause the tween (doesn't reset time elapsed)
    /// </summary>
    void Pause();
    /// <summary>
    /// Stop the tween (reset time elapsed)
    /// </summary>
    void Stop();
    /// <summary>
    /// Toggle the start/stop state of the tween
    /// </summary>
    void ToggleState();
}
