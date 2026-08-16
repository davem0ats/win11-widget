namespace Win11Widget.Core.Tests.Services;

using Xunit;
using Win11Widget.Core.Services;
using Win11Widget.Core.Models;

public class JsonConfigurationServiceTests : IDisposable
{
    private readonly string _tempDir;
    private readonly JsonConfigurationService _service;

    public JsonConfigurationServiceTests()
    {
        _tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(_tempDir);
        _service = new JsonConfigurationService(_tempDir);
    }

    public void Dispose()
    {
        if (Directory.Exists(_tempDir))
        {
            Directory.Delete(_tempDir, true);
        }
    }

    [Fact]
    public async Task LoadConfigurationAsync_WithNoFile_ReturnsDefaultConfiguration()
    {
        // Act
        var config = await _service.LoadConfigurationAsync();

        // Assert
        Assert.NotNull(config);
        Assert.Empty(config.Dates);
        Assert.Equal(14, config.DisplaySettings.FontSize);
        Assert.True(config.SoundsEnabled);
    }

    [Fact]
    public async Task SaveConfigurationAsync_SavesConfiguration()
    {
        // Arrange
        var dates = new List<CountdownDate>
        {
            new CountdownDate("Christmas", new DateTime(2026, 12, 25)),
            new CountdownDate("New Year", new DateTime(2027, 1, 1))
        };
        var displaySettings = new DisplaySettings(16, "#FF0000", "#00FF00", 0.8, true);
        var position = new WidgetPosition(100, 200, 400, 150);
        var config = new WidgetConfiguration(dates, displaySettings, position, false);

        // Act
        await _service.SaveConfigurationAsync(config);

        // Assert
        Assert.True(File.Exists(Path.Combine(_tempDir, "config.json")));
    }

    [Fact]
    public async Task SaveAndLoad_PreservesConfiguration()
    {
        // Arrange
        var dates = new List<CountdownDate>
        {
            new CountdownDate("Christmas", new DateTime(2026, 12, 25))
        };
        var displaySettings = new DisplaySettings(20, "#FF0000", "#00FF00", 0.7, true);
        var position = new WidgetPosition(50, 100, 500, 200);
        var originalConfig = new WidgetConfiguration(dates, displaySettings, position, false);

        // Act
        await _service.SaveConfigurationAsync(originalConfig);
        var loadedConfig = await _service.LoadConfigurationAsync();

        // Assert
        Assert.Single(loadedConfig.Dates);
        Assert.Equal("Christmas", loadedConfig.Dates.First().Label);
        Assert.Equal(new DateTime(2026, 12, 25), loadedConfig.Dates.First().TargetDate);
        Assert.Equal(20, loadedConfig.DisplaySettings.FontSize);
        Assert.Equal("#FF0000", loadedConfig.DisplaySettings.ForegroundColor);
        Assert.Equal(0.7, loadedConfig.DisplaySettings.Opacity);
        Assert.True(loadedConfig.DisplaySettings.AlwaysOnTop);
        Assert.Equal(50, loadedConfig.Position.X);
        Assert.False(loadedConfig.SoundsEnabled);
    }

    [Fact]
    public async Task LoadConfigurationAsync_WithCorruptedFile_ReturnsDefaultConfiguration()
    {
        // Arrange
        var configPath = Path.Combine(_tempDir, "config.json");
        await File.WriteAllTextAsync(configPath, "{ invalid json", TestContext.Current.CancellationToken);

        // Act
        var config = await _service.LoadConfigurationAsync();

        // Assert
        Assert.NotNull(config);
        Assert.Empty(config.Dates);
    }

    [Fact]
    public async Task SaveAndLoad_PreservesNotificationSettings()
    {
        // Arrange
        var dates = new List<CountdownDate> { new CountdownDate("Test", new DateTime(2026, 12, 25)) };
        var notificationSettings = new NotificationSettings(true, false, true, false);
        var config = new WidgetConfiguration(dates, null, null, true, notificationSettings);

        // Act
        await _service.SaveConfigurationAsync(config);
        var loadedConfig = await _service.LoadConfigurationAsync();

        // Assert
        Assert.NotNull(loadedConfig.NotificationSettings);
        Assert.True(loadedConfig.NotificationSettings.Enabled);
        Assert.False(loadedConfig.NotificationSettings.NotifyOneDayBefore);
        Assert.True(loadedConfig.NotificationSettings.NotifyOneHourBefore);
        Assert.False(loadedConfig.NotificationSettings.NotifyAtEvent);
    }

    [Fact]
    public async Task SaveAndLoad_PreservesAllNotificationSettingsDisabled()
    {
        // Arrange
        var dates = new List<CountdownDate> { new CountdownDate("Test", new DateTime(2026, 12, 25)) };
        var notificationSettings = new NotificationSettings(false, false, false, false);
        var config = new WidgetConfiguration(dates, null, null, true, notificationSettings);

        // Act
        await _service.SaveConfigurationAsync(config);
        var loadedConfig = await _service.LoadConfigurationAsync();

        // Assert
        Assert.False(loadedConfig.NotificationSettings.Enabled);
        Assert.False(loadedConfig.NotificationSettings.NotifyOneDayBefore);
        Assert.False(loadedConfig.NotificationSettings.NotifyOneHourBefore);
        Assert.False(loadedConfig.NotificationSettings.NotifyAtEvent);
    }

    [Fact]
    public async Task SaveAndLoad_WithCompleteConfiguration_PreservesEverything()
    {
        // Arrange
        var dates = new List<CountdownDate>
        {
            new CountdownDate("Christmas", new DateTime(2026, 12, 25)),
            new CountdownDate("New Year", new DateTime(2027, 1, 1))
        };
        var displaySettings = new DisplaySettings(18, "#AABBCC", "#112233", 0.85, true);
        var position = new WidgetPosition(150, 250, 450, 175);
        var notificationSettings = new NotificationSettings(true, true, false, true);
        var originalConfig = new WidgetConfiguration(dates, displaySettings, position, false, notificationSettings);

        // Act
        await _service.SaveConfigurationAsync(originalConfig);
        var loadedConfig = await _service.LoadConfigurationAsync();

        // Assert - All settings preserved
        Assert.Equal(2, loadedConfig.Dates.Count);
        Assert.Equal(18, loadedConfig.DisplaySettings.FontSize);
        Assert.Equal("#AABBCC", loadedConfig.DisplaySettings.ForegroundColor);
        Assert.Equal(150, loadedConfig.Position.X);
        Assert.False(loadedConfig.SoundsEnabled);
        Assert.True(loadedConfig.NotificationSettings.Enabled);
        Assert.True(loadedConfig.NotificationSettings.NotifyOneDayBefore);
        Assert.False(loadedConfig.NotificationSettings.NotifyOneHourBefore);
        Assert.True(loadedConfig.NotificationSettings.NotifyAtEvent);
    }

    [Fact]
    public async Task LoadConfigurationAsync_WithNoFile_ReturnsDefaultNotificationSettings()
    {
        // Act
        var config = await _service.LoadConfigurationAsync();

        // Assert - Default notification settings should be present
        Assert.NotNull(config.NotificationSettings);
        Assert.True(config.NotificationSettings.Enabled);
        Assert.True(config.NotificationSettings.NotifyOneDayBefore);
        Assert.True(config.NotificationSettings.NotifyOneHourBefore);
        Assert.True(config.NotificationSettings.NotifyAtEvent);
    }
}
