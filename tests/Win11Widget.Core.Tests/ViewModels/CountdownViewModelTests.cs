namespace Win11Widget.Core.Tests.ViewModels;

using Xunit;
using Win11Widget.Core.Services;
using Win11Widget.Core.ViewModels;
using Win11Widget.Core.Models;
using Moq;

public class CountdownViewModelTests
{
    private readonly Mock<ICountdownCalculator> _mockCalculator;
    private readonly CountdownViewModel _viewModel;

    public CountdownViewModelTests()
    {
        _mockCalculator = new Mock<ICountdownCalculator>();
        var countdownDate = new CountdownDate("Test", new DateTime(2026, 12, 25));
        _viewModel = new CountdownViewModel(_mockCalculator.Object, countdownDate);
    }

    [Fact]
    public void Constructor_InitializesPropertiesCorrectly()
    {
        // Arrange & Act
        // Already done in ctor
        
        // Assert
        Assert.Equal("Test", _viewModel.Label);
        Assert.Equal(new DateTime(2026, 12, 25), _viewModel.TargetDate);
    }

    [Fact]
    public void UpdateTimeRemaining_CallsCalculator()
    {
        // Arrange
        var timeRemaining = new TimeRemaining(5, 12, 30, 45, false);
        _mockCalculator.Setup(c => c.CalculateTimeRemaining(It.IsAny<DateTime>()))
            .Returns(timeRemaining);

        // Act
        _viewModel.UpdateTimeRemaining();

        // Assert
        _mockCalculator.Verify(c => c.CalculateTimeRemaining(It.IsAny<DateTime>()), Times.AtLeastOnce);
        Assert.Equal(timeRemaining, _viewModel.TimeRemaining);
    }

    [Fact]
    public void FormattedTime_ReturnsFormattedString()
    {
        // Arrange
        var timeRemaining = new TimeRemaining(2, 1, 0, 0, false);
        _mockCalculator.Setup(c => c.CalculateTimeRemaining(It.IsAny<DateTime>()))
            .Returns(timeRemaining);
        _viewModel.UpdateTimeRemaining();

        // Act
        var formatted = _viewModel.FormattedTime;

        // Assert
        Assert.Equal("2d 1h", formatted);
    }

    [Fact]
    public void PropertyChanged_RaisedWhenLabelChanges()
    {
        // Arrange
        var propertyChangedRaised = false;
        _viewModel.PropertyChanged += (s, e) => 
        {
            if (e.PropertyName == nameof(CountdownViewModel.Label))
                propertyChangedRaised = true;
        };

        // Act
        _viewModel.Label = "New Label";

        // Assert
        Assert.True(propertyChangedRaised);
    }
}
