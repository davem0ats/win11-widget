namespace Win11Widget.Core.Tests.Models;

using Xunit;
using Win11Widget.Core.Models;

public class DisplaySettingsTests
{
    [Fact]
    public void Constructor_WithDefaultValues_CreatesInstance()
    {
        // Act
        var settings = new DisplaySettings();

        // Assert
        Assert.Equal(14, settings.FontSize);
        Assert.Equal("#FFFFFF", settings.ForegroundColor);
        Assert.Equal("#000000", settings.BackgroundColor);
        Assert.Equal(0.9, settings.Opacity);
        Assert.False(settings.AlwaysOnTop);
    }

    [Fact]
    public void Constructor_WithCustomValues_CreatesInstance()
    {
        // Act
        var settings = new DisplaySettings(20, "#FF0000", "#00FF00", 0.5, true);

        // Assert
        Assert.Equal(20, settings.FontSize);
        Assert.Equal("#FF0000", settings.ForegroundColor);
        Assert.Equal("#00FF00", settings.BackgroundColor);
        Assert.Equal(0.5, settings.Opacity);
        Assert.True(settings.AlwaysOnTop);
    }

    [Fact]
    public void CreateDefault_ReturnsDefaultInstance()
    {
        // Act
        var settings = DisplaySettings.CreateDefault();

        // Assert
        Assert.NotNull(settings);
        Assert.Equal(14, settings.FontSize);
    }
}
