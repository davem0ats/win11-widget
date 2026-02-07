using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Win11Widget.Core.Services;
using Win11Widget.Core.ViewModels;
using Win11Widget.Services;

namespace Win11Widget;

public partial class MainWindow : Window
{
    private WidgetViewModel _viewModel;
    private SystemClock _clock;
    private WindowsNotificationService? _notificationService;

    public MainWindow(WindowsNotificationService? notificationService = null)
    {
        InitializeComponent();

        // Setup dependency injection
        _clock = new SystemClock();
        var calculator = new CountdownCalculator(_clock);
        var configService = new JsonConfigurationService();
        _notificationService = notificationService;
        
        _viewModel = new WidgetViewModel(configService, calculator);
        DataContext = _viewModel;

        // Load configuration on startup
        Loaded += async (s, e) => 
        {
            await _viewModel.LoadConfigurationAsync();
            ApplySettings();
        };

        // Enable dragging
        MouseLeftButtonDown += Window_MouseLeftButtonDown;
    }
    
    public void CheckNotifications()
    {
        if (_notificationService != null)
        {
            _notificationService.CheckAndNotify(
                _viewModel.Countdowns.Select(cd => new Win11Widget.Core.Models.CountdownDate(cd.Label, cd.TargetDate)),
                _viewModel.NotificationSettings,
                _clock);
        }
    }

    private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left)
        {
            DragMove();
            // Save new position
            _viewModel.Position = new Win11Widget.Core.Models.WidgetPosition((int)Left, (int)Top);
            _ = _viewModel.SaveConfigurationAsync();
        }
    }

    private void ApplySettings()
    {
        // Apply position
        Left = _viewModel.Position.X;
        Top = _viewModel.Position.Y;

        // Apply display settings
        Topmost = _viewModel.DisplaySettings.AlwaysOnTop;
        Opacity = _viewModel.DisplaySettings.Opacity;
        
        // Apply background color
        try
        {
            Background = new System.Windows.Media.SolidColorBrush(
                (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(
                    _viewModel.DisplaySettings.BackgroundColor));
        }
        catch { /* Invalid color, keep default */ }
    }

    private void AddCountdownButton_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new AddCountdownDialog { Owner = this };
        if (dialog.ShowDialog() == true)
        {
            _viewModel.AddCountdown(dialog.CountdownLabel, dialog.TargetDate);
            _ = _viewModel.SaveConfigurationAsync();
        }
    }

    private void SettingsButton_Click(object sender, RoutedEventArgs e)
    {
        ShowSettings();
    }

    public void ShowSettings()
    {
        var dialog = new SettingsDialog(_viewModel) { Owner = this };
        if (dialog.ShowDialog() == true)
        {
            ApplySettings();
            _ = _viewModel.SaveConfigurationAsync();
        }
    }

    private void RemoveCountdownButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is CountdownViewModel countdown)
        {
            var result = MessageBox.Show(
                $"Remove countdown '{countdown.Label}'?",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _viewModel.RemoveCountdown(countdown);
                _ = _viewModel.SaveConfigurationAsync();
            }
        }
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void MinimizeButton_Click(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }
}
