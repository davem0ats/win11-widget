using System.Windows.Forms;

namespace Win11Widget.Services;

/// <summary>
/// Wrapper around System.Windows.Forms.NotifyIcon for testability.
/// </summary>
public class NotificationIconWrapper : INotificationIcon
{
    private readonly NotifyIcon _notifyIcon;

    public NotificationIconWrapper(NotifyIcon notifyIcon)
    {
        _notifyIcon = notifyIcon ?? throw new ArgumentNullException(nameof(notifyIcon));
    }

    public bool Visible => _notifyIcon.Visible;

    public void ShowBalloonTip(int timeout, string title, string text, ToolTipIcon icon)
    {
        _notifyIcon.ShowBalloonTip(timeout, title, text, icon);
    }
}
