using Win11Widget.Core.Models;

namespace Win11Widget.Core.Services;

/// <summary>
/// Handles sending notifications and sounds.
/// </summary>
public interface INotificationService
{
    /// <summary>
    /// Sends a notification with the given title and message.
    /// </summary>
    Task SendNotificationAsync(string title, string message);

    /// <summary>
    /// Plays a notification sound if sounds are enabled.
    /// </summary>
    Task PlayNotificationSoundAsync(bool soundsEnabled);
    
    /// <summary>
    /// Checks all countdowns and sends notifications if thresholds are met.
    /// </summary>
    void CheckAndNotify(IEnumerable<CountdownDate> countdowns, NotificationSettings settings, IClock clock);
}
