namespace Win11Widget.Core.Tests.Models;

using Xunit;
using Win11Widget.Core.Models;

public class WidgetConfigurationTests
{
    [Fact]
    public void CreateDefault_ReturnsConfigurationWithDefaults()
    {
        // Act
        var config = WidgetConfiguration.CreateDefault();

        // Assert
        Assert.Empty(config.Dates);
        Assert.NotNull(config.DisplaySettings);
        Assert.NotNull(config.Position);
        Assert.True(config.SoundsEnabled);
        Assert.NotNull(config.NotificationSettings);
    }

    [Fact]
    public void CreateDefault_ReturnsDefaultDisplaySettings()
    {
        // Act
        var config = WidgetConfiguration.CreateDefault();

        // Assert
        var defaultDisplay = DisplaySettings.CreateDefault();
        Assert.Equal(defaultDisplay.FontSize, config.DisplaySettings.FontSize);
        Assert.Equal(defaultDisplay.ForegroundColor, config.DisplaySettings.ForegroundColor);
        Assert.Equal(defaultDisplay.BackgroundColor, config.DisplaySettings.BackgroundColor);
        Assert.Equal(defaultDisplay.Opacity, config.DisplaySettings.Opacity);
    }

    [Fact]
    public void CreateDefault_ReturnsDefaultWidgetPosition()
    {
        // Act
        var config = WidgetConfiguration.CreateDefault();

        // Assert
        var defaultPosition = WidgetPosition.CreateDefault();
        Assert.Equal(defaultPosition.X, config.Position.X);
        Assert.Equal(defaultPosition.Y, config.Position.Y);
        Assert.Equal(defaultPosition.Width, config.Position.Width);
        Assert.Equal(defaultPosition.Height, config.Position.Height);
    }

    [Fact]
    public void CreateDefault_ReturnsDefaultNotificationSettings()
    {
        // Act
        var config = WidgetConfiguration.CreateDefault();

        // Assert
        var defaultSettings = NotificationSettings.CreateDefault();
        Assert.Equal(defaultSettings.Enabled, config.NotificationSettings.Enabled);
        Assert.Equal(defaultSettings.NotifyOneDayBefore, config.NotificationSettings.NotifyOneDayBefore);
        Assert.Equal(defaultSettings.NotifyOneHourBefore, config.NotificationSettings.NotifyOneHourBefore);
        Assert.Equal(defaultSettings.NotifyAtEvent, config.NotificationSettings.NotifyAtEvent);
    }

    [Fact]
    public void Constructor_WithEmptyDates_CreatesConfiguration()
    {
        // Act
        var config = new WidgetConfiguration(new List<CountdownDate>());

        // Assert
        Assert.Empty(config.Dates);
        Assert.NotNull(config.DisplaySettings);
        Assert.NotNull(config.Position);
        Assert.True(config.SoundsEnabled);
    }

    [Fact]
    public void Constructor_WithCustomValues_CreatesConfiguration()
    {
        // Arrange
        var date = new CountdownDate("Test", new DateTime(2026, 12, 25));
        var dates = new[] { date };
        var displaySettings = new DisplaySettings(14, "#FFFFFF", "#000000", 0.8);
        var position = new WidgetPosition(100, 200, 400, 150);
        var soundsEnabled = false;
        var notifications = new NotificationSettings(true, false, true, false);

        // Act
        var config = new WidgetConfiguration(dates, displaySettings, position, soundsEnabled, notifications);

        // Assert
        Assert.Single(config.Dates);
        Assert.Equal(date, config.Dates.First());
        Assert.Equal(displaySettings, config.DisplaySettings);
        Assert.Equal(position, config.Position);
        Assert.False(config.SoundsEnabled);
        Assert.Equal(notifications, config.NotificationSettings);
    }

    [Fact]
    public void Constructor_WithNullDisplaySettings_UsesDefault()
    {
        // Act
        var config = new WidgetConfiguration(new List<CountdownDate>(), displaySettings: null);

        // Assert
        var defaultSettings = DisplaySettings.CreateDefault();
        Assert.Equal(defaultSettings.FontSize, config.DisplaySettings.FontSize);
        Assert.Equal(defaultSettings.ForegroundColor, config.DisplaySettings.ForegroundColor);
        Assert.Equal(defaultSettings.BackgroundColor, config.DisplaySettings.BackgroundColor);
        Assert.Equal(defaultSettings.Opacity, config.DisplaySettings.Opacity);
    }

    [Fact]
    public void Constructor_WithNullPosition_UsesDefault()
    {
        // Act
        var config = new WidgetConfiguration(new List<CountdownDate>(), position: null);

        // Assert
        var defaultPosition = WidgetPosition.CreateDefault();
        Assert.Equal(defaultPosition.X, config.Position.X);
        Assert.Equal(defaultPosition.Y, config.Position.Y);
        Assert.Equal(defaultPosition.Width, config.Position.Width);
        Assert.Equal(defaultPosition.Height, config.Position.Height);
    }

    [Fact]
    public void Constructor_WithNullNotificationSettings_UsesDefault()
    {
        // Act
        var config = new WidgetConfiguration(new List<CountdownDate>(), notificationSettings: null);

        // Assert
        var defaultSettings = NotificationSettings.CreateDefault();
        Assert.Equal(defaultSettings.Enabled, config.NotificationSettings.Enabled);
        Assert.Equal(defaultSettings.NotifyOneDayBefore, config.NotificationSettings.NotifyOneDayBefore);
        Assert.Equal(defaultSettings.NotifyOneHourBefore, config.NotificationSettings.NotifyOneHourBefore);
        Assert.Equal(defaultSettings.NotifyAtEvent, config.NotificationSettings.NotifyAtEvent);
    }

    [Fact]
    public void SoundsEnabled_CanBeSet()
    {
        // Arrange
        var config = WidgetConfiguration.CreateDefault();

        // Act
        config.SoundsEnabled = false;

        // Assert
        Assert.False(config.SoundsEnabled);
    }

    [Fact]
    public void Constructor_ThrowsOnNullDates()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new WidgetConfiguration(null!));
    }
}
