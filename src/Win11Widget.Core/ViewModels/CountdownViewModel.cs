namespace Win11Widget.Core.ViewModels;

using System.ComponentModel;
using Win11Widget.Core.Models;
using Win11Widget.Core.Services;

/// <summary>
/// ViewModel for managing countdown displays.
/// </summary>
public class CountdownViewModel : INotifyPropertyChanged
{
    private readonly ICountdownCalculator _calculator;
    private DateTime _targetDate;
    private string _label;
    private TimeRemaining _timeRemaining;

    public CountdownViewModel(ICountdownCalculator calculator, CountdownDate countdownDate)
    {
        _calculator = calculator ?? throw new ArgumentNullException(nameof(calculator));
        _targetDate = countdownDate.TargetDate;
        _label = countdownDate.Label ?? string.Empty;
        _timeRemaining = _calculator.CalculateTimeRemaining(_targetDate);
    }

    public string Label
    {
        get => _label;
        set
        {
            if (_label != value)
            {
                _label = value;
                OnPropertyChanged(nameof(Label));
            }
        }
    }

    public DateTime TargetDate
    {
        get => _targetDate;
        set
        {
            if (_targetDate != value)
            {
                _targetDate = value;
                UpdateTimeRemaining();
                OnPropertyChanged(nameof(TargetDate));
            }
        }
    }

    public TimeRemaining TimeRemaining
    {
        get => _timeRemaining;
        private set
        {
            if (_timeRemaining == null || !_timeRemaining.Equals(value))
            {
                _timeRemaining = value;
                OnPropertyChanged(nameof(TimeRemaining));
                OnPropertyChanged(nameof(FormattedTime));
            }
        }
    }

    public string FormattedTime => TimeRemaining.ToFormattedString();

    public void UpdateTimeRemaining()
    {
        TimeRemaining = _calculator.CalculateTimeRemaining(_targetDate);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
