namespace Win11Widget.Core.Models;

/// <summary>
/// Settings for countdown event notifications.
/// </summary>
public class NotificationSettings
{
    public NotificationSettings(
        bool enabled = true,
        bool notifyOneDayBefore = true,
        bool notifyOneHourBefore = true,
        bool notifyAtEvent = true)
    {
        Enabled = enabled;
        NotifyOneDayBefore = notifyOneDayBefore;
        NotifyOneHourBefore = notifyOneHourBefore;
        NotifyAtEvent = notifyAtEvent;
    }

    public bool Enabled { get; set; }
    public bool NotifyOneDayBefore { get; set; }
    public bool NotifyOneHourBefore { get; set; }
    public bool NotifyAtEvent { get; set; }

    public static NotificationSettings CreateDefault()
    {
        return new NotificationSettings();
    }
}
