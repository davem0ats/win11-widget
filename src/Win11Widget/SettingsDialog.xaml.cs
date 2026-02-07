using System.Windows;
using Win11Widget.Core.ViewModels;
using Win11Widget.Core.Models;

namespace Win11Widget;

public partial class SettingsDialog : Window
{
    private readonly WidgetViewModel _viewModel;

    public SettingsDialog(WidgetViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        LoadCurrentSettings();
    }

    private void LoadCurrentSettings()
    {
        // Display settings
        FontSizeSlider.Value = _viewModel.DisplaySettings.FontSize;
        ForegroundColorTextBox.Text = _viewModel.DisplaySettings.ForegroundColor;
        BackgroundColorTextBox.Text = _viewModel.DisplaySettings.BackgroundColor;
        OpacitySlider.Value = _viewModel.DisplaySettings.Opacity;
        AlwaysOnTopCheckBox.IsChecked = _viewModel.DisplaySettings.AlwaysOnTop;

        // Position settings
        PositionXTextBox.Text = _viewModel.Position.X.ToString();
        PositionYTextBox.Text = _viewModel.Position.Y.ToString();

        // Sound settings
        SoundsEnabledCheckBox.IsChecked = _viewModel.SoundsEnabled;
        
        // Notification settings
        NotificationsEnabledCheckBox.IsChecked = _viewModel.NotificationSettings.Enabled;
        NotifyOneDayBeforeCheckBox.IsChecked = _viewModel.NotificationSettings.NotifyOneDayBefore;
        NotifyOneHourBeforeCheckBox.IsChecked = _viewModel.NotificationSettings.NotifyOneHourBefore;
        NotifyAtEventCheckBox.IsChecked = _viewModel.NotificationSettings.NotifyAtEvent;
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        // Validate inputs
        if (!int.TryParse(PositionXTextBox.Text, out int posX))
        {
            MessageBox.Show("X Position must be a valid number.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (!int.TryParse(PositionYTextBox.Text, out int posY))
        {
            MessageBox.Show("Y Position must be a valid number.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        // Update display settings
        _viewModel.DisplaySettings = new DisplaySettings(
            fontSize: (int)FontSizeSlider.Value,
            foregroundColor: ForegroundColorTextBox.Text,
            backgroundColor: BackgroundColorTextBox.Text,
            opacity: OpacitySlider.Value,
            alwaysOnTop: AlwaysOnTopCheckBox.IsChecked ?? false
        );

        // Update position
        _viewModel.Position = new WidgetPosition(posX, posY);

        // Update sounds
        _viewModel.SoundsEnabled = SoundsEnabledCheckBox.IsChecked ?? false;
        
        // Update notification settings
        _viewModel.NotificationSettings.Enabled = NotificationsEnabledCheckBox.IsChecked ?? true;
        _viewModel.NotificationSettings.NotifyOneDayBefore = NotifyOneDayBeforeCheckBox.IsChecked ?? true;
        _viewModel.NotificationSettings.NotifyOneHourBefore = NotifyOneHourBeforeCheckBox.IsChecked ?? true;
        _viewModel.NotificationSettings.NotifyAtEvent = NotifyAtEventCheckBox.IsChecked ?? true;

        DialogResult = true;
        Close();
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        Close();
    }

    private void TopLeft_Click(object sender, RoutedEventArgs e)
    {
        PositionXTextBox.Text = "20";
        PositionYTextBox.Text = "20";
    }

    private void TopRight_Click(object sender, RoutedEventArgs e)
    {
        PositionXTextBox.Text = ((int)(SystemParameters.PrimaryScreenWidth - 500)).ToString();
        PositionYTextBox.Text = "20";
    }

    private void Center_Click(object sender, RoutedEventArgs e)
    {
        PositionXTextBox.Text = ((int)(SystemParameters.PrimaryScreenWidth / 2 - 250)).ToString();
        PositionYTextBox.Text = ((int)(SystemParameters.PrimaryScreenHeight / 2 - 150)).ToString();
    }

    private void BottomLeft_Click(object sender, RoutedEventArgs e)
    {
        PositionXTextBox.Text = "20";
        PositionYTextBox.Text = ((int)(SystemParameters.PrimaryScreenHeight - 350)).ToString();
    }

    private void BottomRight_Click(object sender, RoutedEventArgs e)
    {
        PositionXTextBox.Text = ((int)(SystemParameters.PrimaryScreenWidth - 500)).ToString();
        PositionYTextBox.Text = ((int)(SystemParameters.PrimaryScreenHeight - 350)).ToString();
    }
}
