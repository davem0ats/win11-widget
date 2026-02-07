using System.Windows.Forms;

namespace Win11Widget.Services;

/// <summary>
/// Abstraction for notification icon functionality to enable testing.
/// </summary>
public interface INotificationIcon
{
    bool Visible { get; }
    void ShowBalloonTip(int timeout, string title, string text, ToolTipIcon icon);
}
