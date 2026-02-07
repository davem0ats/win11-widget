using System.Windows.Forms;
using Win11Widget.Core.Models;
using Win11Widget.Core.Services;

namespace Win11Widget.Services;

/// <summary>
/// Windows implementation of notification service using System.Windows.Forms.NotifyIcon for balloon tips.
/// </summary>
public class WindowsNotificationService : INotificationService
{
    private readonly INotificationIcon? _notificationIcon;
    private readonly HashSet<string> _shownNotifications = new();

    public WindowsNotificationService(INotificationIcon? notificationIcon = null)
    {
        _notificationIcon = notificationIcon;
    }

    public Task SendNotificationAsync(string title, string message)
    {
        if (_notificationIcon != null && _notificationIcon.Visible)
        {
            _notificationIcon.ShowBalloonTip(5000, title, message, ToolTipIcon.Info);
        }
        return Task.CompletedTask;
    }

    public Task PlayNotificationSoundAsync(bool soundsEnabled)
    {
        if (soundsEnabled)
        {
            System.Media.SystemSounds.Asterisk.Play();
        }
        return Task.CompletedTask;
    }

    public void CheckAndNotify(IEnumerable<CountdownDate> countdowns, NotificationSettings settings, IClock clock)
    {
        if (!settings.Enabled)
        {
            return;
        }

        var now = clock.UtcNow;

        foreach (var countdown in countdowns)
        {
            var timeRemaining = countdown.TargetDate - now;
            
            // Check if event is in the past - show notification once
            if (timeRemaining.TotalSeconds <= 0)
            {
                if (settings.NotifyAtEvent)
                {
                    var key = $"{countdown.Id}_at";
                    if (!_shownNotifications.Contains(key))
                    {
                        _shownNotifications.Add(key);
                        _ = SendNotificationAsync("Event Now!", $"{countdown.Label} is happening now!");
                    }
                }
                continue;
            }

            // Check 1 hour threshold
            if (settings.NotifyOneHourBefore && 
                timeRemaining.TotalHours <= 1 && timeRemaining.TotalHours > 0.95)
            {
                var key = $"{countdown.Id}_1h";
                if (!_shownNotifications.Contains(key))
                {
                    _shownNotifications.Add(key);
                    _ = SendNotificationAsync("Event in 1 Hour!", $"{countdown.Label} is coming up in 1 hour.");
                }
            }

            // Check 1 day threshold (24 hours)
            if (settings.NotifyOneDayBefore && 
                timeRemaining.TotalHours <= 24 && timeRemaining.TotalHours > 23.5)
            {
                var key = $"{countdown.Id}_1d";
                if (!_shownNotifications.Contains(key))
                {
                    _shownNotifications.Add(key);
                    _ = SendNotificationAsync("Event Tomorrow!", $"{countdown.Label} is coming up in 1 day.");
                }
            }
        }
    }

    /// <summary>
    /// Clears notification history (useful when countdowns are removed).
    /// </summary>
    public void ClearNotificationHistory()
    {
        _shownNotifications.Clear();
    }
}
