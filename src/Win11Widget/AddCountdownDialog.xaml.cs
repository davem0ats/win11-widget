using System.Windows;

namespace Win11Widget;

public partial class AddCountdownDialog : Window
{
    public string CountdownLabel { get; private set; } = string.Empty;
    public DateTime TargetDate { get; private set; }

    public AddCountdownDialog()
    {
        InitializeComponent();
        TargetDatePicker.SelectedDate = DateTime.Today.AddDays(7);
    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(LabelTextBox.Text))
        {
            MessageBox.Show("Please enter a label for the countdown.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (!TargetDatePicker.SelectedDate.HasValue)
        {
            MessageBox.Show("Please select a target date.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        CountdownLabel = LabelTextBox.Text;
        TargetDate = TargetDatePicker.SelectedDate.Value;
        DialogResult = true;
        Close();
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        Close();
    }
}
