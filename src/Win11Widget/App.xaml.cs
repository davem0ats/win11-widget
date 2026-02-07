using System.Windows;
using System.Drawing;
using System.Windows.Forms;
using Win11Widget.Services;
using Application = System.Windows.Application;

namespace Win11Widget;

public partial class App : Application
{
    private NotifyIcon? _notifyIcon;
    private MainWindow? _mainWindow;
    private WindowsNotificationService? _notificationService;
    private System.Threading.Timer? _notificationTimer;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        
        // Create system tray icon using Windows Forms NotifyIcon
        _notifyIcon = new NotifyIcon
        {
            Icon = CreateTrayIcon(),
            Visible = true,
            Text = "Win11 Widget - Countdown Timers"
        };
        
        _notifyIcon.DoubleClick += NotifyIcon_DoubleClick;
        _notifyIcon.ContextMenuStrip = CreateContextMenu();
        
        // Create notification service with wrapped notification icon
        var notificationIcon = new NotificationIconWrapper(_notifyIcon);
        _notificationService = new WindowsNotificationService(notificationIcon);
        
        // Create main window with notification service
        _mainWindow = new MainWindow(_notificationService);
        _mainWindow.Closing += MainWindow_Closing;
        _mainWindow.StateChanged += MainWindow_StateChanged;
        _mainWindow.Show();
        
        // Start notification check timer (check every 5 minutes)
        _notificationTimer = new System.Threading.Timer(CheckNotifications, null, TimeSpan.Zero, TimeSpan.FromMinutes(5));
    }
    
    private void CheckNotifications(object? state)
    {
        // Run notification check on UI thread
        Dispatcher.InvokeAsync(() =>
        {
            _mainWindow?.CheckNotifications();
        });
    }

    private Icon CreateTrayIcon()
    {
        // Create a simple icon with a clock face
        var bitmap = new Bitmap(32, 32);
        using (var g = Graphics.FromImage(bitmap))
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            
            // Draw blue background circle
            using (var brush = new SolidBrush(System.Drawing.Color.FromArgb(0, 120, 212)))
            {
                g.FillEllipse(brush, 2, 2, 28, 28);
            }
            
            // Draw white clock outline
            using (var pen = new Pen(System.Drawing.Color.White, 2))
            {
                g.DrawEllipse(pen, 4, 4, 24, 24);
            }
            
            // Draw clock hands
            using (var pen = new Pen(System.Drawing.Color.White, 2))
            {
                // Hour hand (pointing to 10)
                g.DrawLine(pen, 16, 16, 11, 10);
                // Minute hand (pointing to 2)
                g.DrawLine(pen, 16, 16, 22, 10);
            }
            
            // Draw center dot
            using (var brush = new SolidBrush(System.Drawing.Color.White))
            {
                g.FillEllipse(brush, 14, 14, 4, 4);
            }
        }
        
        return Icon.FromHandle(bitmap.GetHicon());
    }

    private ContextMenuStrip CreateContextMenu()
    {
        var contextMenu = new ContextMenuStrip();
        
        var openMenuItem = new ToolStripMenuItem("Open");
        openMenuItem.Click += (s, e) => ShowMainWindow();
        openMenuItem.Font = new Font(openMenuItem.Font, System.Drawing.FontStyle.Bold);
        contextMenu.Items.Add(openMenuItem);
        
        contextMenu.Items.Add(new ToolStripSeparator());
        
        var settingsMenuItem = new ToolStripMenuItem("Settings");
        settingsMenuItem.Click += (s, e) =>
        {
            ShowMainWindow();
            _mainWindow?.ShowSettings();
        };
        contextMenu.Items.Add(settingsMenuItem);
        
        contextMenu.Items.Add(new ToolStripSeparator());
        
        var exitMenuItem = new ToolStripMenuItem("Exit");
        exitMenuItem.Click += (s, e) =>
        {
            _notifyIcon!.Visible = false;
            _notifyIcon?.Dispose();
            Current.Shutdown();
        };
        contextMenu.Items.Add(exitMenuItem);
        
        return contextMenu;
    }

    private void MainWindow_StateChanged(object? sender, EventArgs e)
    {
        // Hide window when minimized
        if (_mainWindow?.WindowState == WindowState.Minimized)
        {
            _mainWindow.Hide();
        }
    }

    private void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
    {
        // Minimize to tray instead of closing
        e.Cancel = true;
        _mainWindow?.Hide();
    }

    private void NotifyIcon_DoubleClick(object? sender, EventArgs e)
    {
        ShowMainWindow();
    }

    private void ShowMainWindow()
    {
        if (_mainWindow != null)
        {
            _mainWindow.Show();
            _mainWindow.WindowState = WindowState.Normal;
            _mainWindow.Activate();
        }
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _notificationTimer?.Dispose();
        _notifyIcon?.Dispose();
        base.OnExit(e);
    }
}
