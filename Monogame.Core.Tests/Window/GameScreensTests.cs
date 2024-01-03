using System;
using Xunit;
using Monogame.Core;
using Monogame.Core.Windows;
using Monogame.Core.Windows.GameScreens;
using Monogame.Core.Windows.Structs;

namespace Monogame.Core.Tests.Window;

public class GameScreensTests
{
    [Fact]
    public void VerticalSplitScreenChangingScreenCountToInvalidValue()
    {
        var screen = new VerticalSplitScreen(null, 4, 3);
        Action act = () => { screen.ScreenCount = 3; };
        Assert.Throws<ArgumentException>(act);
    }
    
    [Fact]
    public void VerticalSplitScreenInitializeWithBadIndexes()
    {
        Action act = () =>
        {
            var screen = new VerticalSplitScreen(null, 4, 3, 2);
        };
        Assert.Throws<ArgumentException>(act);
    }
    
    [Fact]
    public void VerticalSplitScreenChangingScreenIndexToOutOfRangeValue()
    {
        var screen = new VerticalSplitScreen(null, 4, 3);
        Action act = () => { screen.StartScreenIndex = 4; };
        Assert.Throws<ArgumentOutOfRangeException>(act);
    }
    
    [Fact]
    public void VerticalSplitScreenChangingScreenIndexToInvalidValue()
    {
        var screen = new VerticalSplitScreen(null, 4, 2, 3);
        Action act = () => { screen.EndScreenIndex = 1; };
        Assert.Throws<ArgumentException>(act);
    }
    
    [Fact]
    public void GridScreenChangingGridDefinitionToInvalidValue()
    {
        var screen = new GridScreen(null, 
            new GridDefinition(2, 2), new GridIndex(1, 1));
        Action act = () => { screen.GridDefinition = new GridDefinition(1, 2); };
        Assert.Throws<ArgumentException>(act);
    }
    
    [Fact]
    public void GridScreenChangingGridIndexToInvalidValue()
    {
        var screen = new GridScreen(null, 
            new GridDefinition(2, 2), new GridIndex(1, 1));
        Action act = () => { screen.EndGridIndex = new GridIndex(1, 0); };
        Assert.Throws<ArgumentException>(act);
    }
    
    [Fact]
    public void GridScreenChangingGridIndexToOutOfRangeValue()
    {
        var screen = new GridScreen(null, 
            new GridDefinition(2, 2), new GridIndex(1, 1));
        Action act = () => { screen.EndGridIndex = new GridIndex(1, 2); };
        Assert.Throws<ArgumentOutOfRangeException>(act);
    }
    
    [Fact]
    public void SmartGridScreenChangingScreenCountToInvalidValue()
    {
        var screen = new SmartGridScreen(null, 4, 3, 16, 9);
        Action act = () => { screen.ScreenCount = 3; };
        Assert.Throws<ArgumentException>(act);
    }
    
    [Fact]
    public void SmartGridScreenChangingScreenIndexToOutOfRangeValue()
    {
        var screen = new SmartGridScreen(null, 4, 3, 16, 9);
        Action act = () => { screen.ScreenIndex = 5; };
        Assert.Throws<ArgumentOutOfRangeException>(act);
    }
}