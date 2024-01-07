using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame.Core.Demo.Window;

static class WindowResizer
{
    public static Point TargetResolution => _resolutions[0];

    private static readonly Point[] _resolutions =
    {
        new Point(3200, 1800),
        new Point(2400, 1350),
        new Point(1600, 900),
        new Point(1200, 675),
        new Point(800, 450),
        new Point(600, 337),
        new Point(400, 225)
    };

    public static void SetWindowSize(GraphicsDeviceManager graphicsDeviceManager)
    {
        //User screen resolution
        GraphicsAdapter graphicsAdapter = GraphicsAdapter.DefaultAdapter;
        DisplayMode currentDisplayMode = graphicsAdapter.CurrentDisplayMode;
        var userScreenResolution = new Point(currentDisplayMode.Width, currentDisplayMode.Height);

        //Searching for optimal resolution
        int resolutionIndex = 0;
        while (resolutionIndex < _resolutions.Length &&
            userScreenResolution.X < _resolutions[resolutionIndex].X &&
            userScreenResolution.Y < _resolutions[resolutionIndex].Y)
            resolutionIndex++;

        graphicsDeviceManager.PreferredBackBufferWidth = _resolutions[resolutionIndex].X;
        graphicsDeviceManager.PreferredBackBufferHeight = _resolutions[resolutionIndex].Y;
        graphicsDeviceManager.ApplyChanges();
    }
}
