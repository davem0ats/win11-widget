namespace Win11Widget.Core.Models;

/// <summary>
/// Represents the complete configuration of the widget.
/// </summary>
public class WidgetConfiguration
{
    public WidgetConfiguration(
        IEnumerable<CountdownDate> dates,
        DisplaySettings? displaySettings = null,
        WidgetPosition? position = null,
        bool soundsEnabled = true,
        NotificationSettings? notificationSettings = null)
    {
        Dates = new List<CountdownDate>(dates ?? throw new ArgumentNullException(nameof(dates)));
        DisplaySettings = displaySettings ?? DisplaySettings.CreateDefault();
        Position = position ?? WidgetPosition.CreateDefault();
        SoundsEnabled = soundsEnabled;
        NotificationSettings = notificationSettings ?? NotificationSettings.CreateDefault();
    }

    public IList<CountdownDate> Dates { get; }
    public DisplaySettings DisplaySettings { get; }
    public WidgetPosition Position { get; }
    public bool SoundsEnabled { get; set; }
    public NotificationSettings NotificationSettings { get; }

    public static WidgetConfiguration CreateDefault()
    {
        return new WidgetConfiguration(new List<CountdownDate>());
    }
}
