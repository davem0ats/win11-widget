namespace Win11Widget.Core.ViewModels;

using System.Collections.ObjectModel;
using System.ComponentModel;
using Win11Widget.Core.Models;
using Win11Widget.Core.Services;

/// <summary>
/// Main ViewModel for the Widget application.
/// </summary>
public class WidgetViewModel : INotifyPropertyChanged
{
    private readonly IConfigurationService _configService;
    private readonly ICountdownCalculator _calculator;
    private WidgetConfiguration _configuration;
    private ObservableCollection<CountdownViewModel> _countdowns;
    private DisplaySettings _displaySettings;
    private WidgetPosition _position;
    private bool _soundsEnabled;

    public WidgetViewModel(IConfigurationService configService, ICountdownCalculator calculator, WidgetConfiguration? initialConfig = null)
    {
        _configService = configService ?? throw new ArgumentNullException(nameof(configService));
        _calculator = calculator ?? throw new ArgumentNullException(nameof(calculator));
        _configuration = initialConfig ?? WidgetConfiguration.CreateDefault();
        _countdowns = new ObservableCollection<CountdownViewModel>();
        _displaySettings = _configuration.DisplaySettings;
        _position = _configuration.Position;
        _soundsEnabled = _configuration.SoundsEnabled;

        InitializeCountdowns();
    }

    private void InitializeCountdowns()
    {
        _countdowns.Clear();
        foreach (var date in _configuration.Dates)
        {
            _countdowns.Add(new CountdownViewModel(_calculator, date));
        }
    }

    public ObservableCollection<CountdownViewModel> Countdowns
    {
        get => _countdowns;
    }

    public DisplaySettings DisplaySettings
    {
        get => _displaySettings;
        set
        {
            if (_displaySettings != value)
            {
                _displaySettings = value;
                _configuration = new WidgetConfiguration(_configuration.Dates, value, _position, _soundsEnabled, _configuration.NotificationSettings);
                OnPropertyChanged(nameof(DisplaySettings));
            }
        }
    }

    public WidgetPosition Position
    {
        get => _position;
        set
        {
            if (!_position.Equals(value))
            {
                _position = value;
                _configuration = new WidgetConfiguration(_configuration.Dates, _displaySettings, value, _soundsEnabled, _configuration.NotificationSettings);
                OnPropertyChanged(nameof(Position));
            }
        }
    }

    public bool SoundsEnabled
    {
        get => _soundsEnabled;
        set
        {
            if (_soundsEnabled != value)
            {
                _soundsEnabled = value;
                _configuration.SoundsEnabled = value;
                OnPropertyChanged(nameof(SoundsEnabled));
            }
        }
    }

    public NotificationSettings NotificationSettings
    {
        get => _configuration.NotificationSettings;
    }

    public async Task LoadConfigurationAsync()
    {
        _configuration = await _configService.LoadConfigurationAsync();
        _displaySettings = _configuration.DisplaySettings;
        _position = _configuration.Position;
        _soundsEnabled = _configuration.SoundsEnabled;
        InitializeCountdowns();

        OnPropertyChanged(nameof(Countdowns));
        OnPropertyChanged(nameof(DisplaySettings));
        OnPropertyChanged(nameof(Position));
        OnPropertyChanged(nameof(SoundsEnabled));
        OnPropertyChanged(nameof(NotificationSettings));
    }

    public async Task SaveConfigurationAsync()
    {
        await _configService.SaveConfigurationAsync(_configuration);
    }

    public void AddCountdown(string label, DateTime targetDate)
    {
        var countdownDate = new CountdownDate(label, targetDate);
        var dates = new List<CountdownDate>(_configuration.Dates) { countdownDate };
        _configuration = new WidgetConfiguration(dates, _displaySettings, _position, _soundsEnabled, _configuration.NotificationSettings);
        _countdowns.Add(new CountdownViewModel(_calculator, countdownDate));
    }

    public void RemoveCountdown(CountdownViewModel countdown)
    {
        var dateToRemove = _configuration.Dates.FirstOrDefault(d => d.Label == countdown.Label && d.TargetDate == countdown.TargetDate);
        if (dateToRemove != null)
        {
            var dates = new List<CountdownDate>(_configuration.Dates);
            dates.Remove(dateToRemove);
            _configuration = new WidgetConfiguration(dates, _displaySettings, _position, _soundsEnabled, _configuration.NotificationSettings);
            _countdowns.Remove(countdown);
        }
    }

    public void UpdateCountdowns()
    {
        foreach (var countdown in _countdowns)
        {
            countdown.UpdateTimeRemaining();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
