namespace Win11Widget.Core.Tests.ViewModels;

using Xunit;
using Win11Widget.Core.Services;
using Win11Widget.Core.ViewModels;
using Win11Widget.Core.Models;
using Moq;

public class WidgetViewModelTests
{
    private readonly Mock<IConfigurationService> _mockConfigService;
    private readonly Mock<ICountdownCalculator> _mockCalculator;
    private readonly WidgetViewModel _viewModel;

    public WidgetViewModelTests()
    {
        _mockConfigService = new Mock<IConfigurationService>();
        _mockCalculator = new Mock<ICountdownCalculator>();

        var config = WidgetConfiguration.CreateDefault();
        _viewModel = new WidgetViewModel(_mockConfigService.Object, _mockCalculator.Object, config);
    }

    [Fact]
    public void Constructor_InitializesCountdowns()
    {
        // Assert
        Assert.NotNull(_viewModel.Countdowns);
    }

    [Fact]
    public void AddCountdown_AddsToCollection()
    {
        // Arrange
        var label = "New Countdown";
        var targetDate = new DateTime(2026, 12, 25);
        var timeRemaining = new TimeRemaining(10, 0, 0, 0, false);
        _mockCalculator.Setup(c => c.CalculateTimeRemaining(It.IsAny<DateTime>()))
            .Returns(timeRemaining);

        // Act
        _viewModel.AddCountdown(label, targetDate);

        // Assert
        Assert.NotEmpty(_viewModel.Countdowns);
        var added = _viewModel.Countdowns.Last();
        Assert.Equal(label, added.Label);
    }

    [Fact]
    public void SoundsEnabled_PropertyChangedRaised()
    {
        // Arrange
        var propertyChangedRaised = false;
        _viewModel.PropertyChanged += (s, e) => 
        {
            if (e.PropertyName == nameof(WidgetViewModel.SoundsEnabled))
                propertyChangedRaised = true;
        };

        // Act
        _viewModel.SoundsEnabled = false;

        // Assert
        Assert.True(propertyChangedRaised);
        Assert.False(_viewModel.SoundsEnabled);
    }

    [Fact]
    public async Task LoadConfigurationAsync_CallsConfigService()
    {
        // Arrange
        var config = WidgetConfiguration.CreateDefault();
        _mockConfigService.Setup(c => c.LoadConfigurationAsync())
            .ReturnsAsync(config);

        // Act
        await _viewModel.LoadConfigurationAsync();

        // Assert
        _mockConfigService.Verify(c => c.LoadConfigurationAsync(), Times.Once);
    }
}
