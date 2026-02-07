namespace Win11Widget.Core.Services;

using System.Text.Json;
using Win11Widget.Core.Models;

/// <summary>
/// Manages persistence of widget configuration using JSON files in LocalAppData.
/// </summary>
public class JsonConfigurationService : IConfigurationService
{
    private readonly string _configPath;

    public JsonConfigurationService(string? configDirectory = null)
    {
        var appDataPath = configDirectory ?? Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "Win11Widget");

        Directory.CreateDirectory(appDataPath);
        _configPath = Path.Combine(appDataPath, "config.json");
    }

    public async Task<WidgetConfiguration> LoadConfigurationAsync()
    {
        try
        {
            if (!File.Exists(_configPath))
            {
                return WidgetConfiguration.CreateDefault();
            }

            var json = await File.ReadAllTextAsync(_configPath);
            var data = JsonSerializer.Deserialize<ConfigurationData>(json);

            if (data == null)
            {
                return WidgetConfiguration.CreateDefault();
            }

            var dates = data.Dates?.Select(d => new CountdownDate(d.Label, d.TargetDate)).ToList()
                ?? new List<CountdownDate>();
            
            var displaySettings = new DisplaySettings(
                data.DisplaySettings?.FontSize ?? 14,
                data.DisplaySettings?.ForegroundColor ?? "#FFFFFF",
                data.DisplaySettings?.BackgroundColor ?? "#000000",
                data.DisplaySettings?.Opacity ?? 0.9,
                data.DisplaySettings?.AlwaysOnTop ?? false);

            var position = new WidgetPosition(
                data.Position?.X ?? 0,
                data.Position?.Y ?? 0,
                data.Position?.Width ?? 300,
                data.Position?.Height ?? 100);

            var notificationSettings = new NotificationSettings(
                data.NotificationSettings?.Enabled ?? true,
                data.NotificationSettings?.NotifyOneDayBefore ?? true,
                data.NotificationSettings?.NotifyOneHourBefore ?? true,
                data.NotificationSettings?.NotifyAtEvent ?? true);

            return new WidgetConfiguration(dates, displaySettings, position, data.SoundsEnabled ?? true, notificationSettings);
        }
        catch
        {
            // Return default configuration if loading fails
            return WidgetConfiguration.CreateDefault();
        }
    }

    public async Task SaveConfigurationAsync(WidgetConfiguration configuration)
    {
        var data = new ConfigurationData
        {
            Dates = configuration.Dates.Select(d => new DateData { Label = d.Label, TargetDate = d.TargetDate }).ToList(),
            DisplaySettings = new DisplaySettingsData
            {
                FontSize = configuration.DisplaySettings.FontSize,
                ForegroundColor = configuration.DisplaySettings.ForegroundColor,
                BackgroundColor = configuration.DisplaySettings.BackgroundColor,
                Opacity = configuration.DisplaySettings.Opacity,
                AlwaysOnTop = configuration.DisplaySettings.AlwaysOnTop
            },
            Position = new PositionData
            {
                X = configuration.Position.X,
                Y = configuration.Position.Y,
                Width = configuration.Position.Width,
                Height = configuration.Position.Height
            },
            SoundsEnabled = configuration.SoundsEnabled,
            NotificationSettings = new NotificationSettingsData
            {
                Enabled = configuration.NotificationSettings.Enabled,
                NotifyOneDayBefore = configuration.NotificationSettings.NotifyOneDayBefore,
                NotifyOneHourBefore = configuration.NotificationSettings.NotifyOneHourBefore,
                NotifyAtEvent = configuration.NotificationSettings.NotifyAtEvent
            }
        };

        var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(_configPath, json);
    }

    // Data transfer objects for serialization
    private class ConfigurationData
    {
        public List<DateData>? Dates { get; set; }
        public DisplaySettingsData? DisplaySettings { get; set; }
        public PositionData? Position { get; set; }
        public bool? SoundsEnabled { get; set; }
        public NotificationSettingsData? NotificationSettings { get; set; }
    }

    private class DateData
    {
        public string Label { get; set; } = string.Empty;
        public DateTime TargetDate { get; set; }
    }

    private class DisplaySettingsData
    {
        public int FontSize { get; set; }
        public string ForegroundColor { get; set; } = string.Empty;
        public string BackgroundColor { get; set; } = string.Empty;
        public double Opacity { get; set; }
        public bool AlwaysOnTop { get; set; }
    }

    private class PositionData
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
    }

    private class NotificationSettingsData
    {
        public bool Enabled { get; set; }
        public bool NotifyOneDayBefore { get; set; }
        public bool NotifyOneHourBefore { get; set; }
        public bool NotifyAtEvent { get; set; }
    }
}
