namespace Win11Widget.Core.Tests.Services;

using Xunit;
using Win11Widget.Core.Services;
using Win11Widget.Core.Models;
using Moq;

public class CountdownCalculatorTests
{
    private readonly Mock<IClock> _mockClock;
    private readonly CountdownCalculator _calculator;

    public CountdownCalculatorTests()
    {
        _mockClock = new Mock<IClock>();
        _calculator = new CountdownCalculator(_mockClock.Object);
    }

    [Fact]
    public void CalculateTimeRemaining_WithFutureDate_ReturnsCorrectDaysHoursMinutesSeconds()
    {
        // Arrange
        var now = new DateTime(2026, 2, 7, 12, 0, 0, DateTimeKind.Utc);
        var targetDate = new DateTime(2026, 2, 10, 14, 30, 45, DateTimeKind.Utc);
        _mockClock.Setup(c => c.UtcNow).Returns(now);

        // Act
        var result = _calculator.CalculateTimeRemaining(targetDate);

        // Assert
        Assert.False(result.IsExpired);
        Assert.Equal(3, result.Days);
        Assert.Equal(2, result.Hours);
        Assert.Equal(30, result.Minutes);
        Assert.Equal(45, result.Seconds);
    }

    [Fact]
    public void CalculateTimeRemaining_WithExpiredDate_ReturnsExpired()
    {
        // Arrange
        var now = new DateTime(2026, 2, 10, 12, 0, 0, DateTimeKind.Utc);
        var targetDate = new DateTime(2026, 2, 7, 12, 0, 0, DateTimeKind.Utc);
        _mockClock.Setup(c => c.UtcNow).Returns(now);

        // Act
        var result = _calculator.CalculateTimeRemaining(targetDate);

        // Assert
        Assert.True(result.IsExpired);
        Assert.Equal(0, result.Days);
        Assert.Equal(0, result.Hours);
        Assert.Equal(0, result.Minutes);
        Assert.Equal(0, result.Seconds);
    }

    [Fact]
    public void CalculateTimeRemaining_WithExactlyNow_ReturnsExpired()
    {
        // Arrange
        var now = new DateTime(2026, 2, 7, 12, 0, 0, DateTimeKind.Utc);
        var targetDate = new DateTime(2026, 2, 7, 12, 0, 0, DateTimeKind.Utc);
        _mockClock.Setup(c => c.UtcNow).Returns(now);

        // Act
        var result = _calculator.CalculateTimeRemaining(targetDate);

        // Assert
        Assert.True(result.IsExpired);
    }

    [Fact]
    public void CalculateTimeRemaining_WithLessThanOneDay_ReturnsCorrectValues()
    {
        // Arrange
        var now = new DateTime(2026, 2, 7, 12, 0, 0, DateTimeKind.Utc);
        var targetDate = new DateTime(2026, 2, 7, 15, 45, 30, DateTimeKind.Utc);
        _mockClock.Setup(c => c.UtcNow).Returns(now);

        // Act
        var result = _calculator.CalculateTimeRemaining(targetDate);

        // Assert
        Assert.False(result.IsExpired);
        Assert.Equal(0, result.Days);
        Assert.Equal(3, result.Hours);
        Assert.Equal(45, result.Minutes);
        Assert.Equal(30, result.Seconds);
    }

    [Fact]
    public void TimeRemaining_ToFormattedString_WithMultipleParts_FormatsCorrectly()
    {
        // Arrange
        var timeRemaining = new TimeRemaining(3, 2, 30, 45, false);

        // Act
        var formatted = timeRemaining.ToFormattedString();

        // Assert
        Assert.Equal("3d 2h 30m 45s", formatted);
    }

    [Fact]
    public void TimeRemaining_ToFormattedString_WithOnlyDays_FormatsCorrectly()
    {
        // Arrange
        var timeRemaining = new TimeRemaining(5, 0, 0, 0, false);

        // Act
        var formatted = timeRemaining.ToFormattedString();

        // Assert
        Assert.Equal("5d", formatted);
    }

    [Fact]
    public void TimeRemaining_ToFormattedString_Expired_ReturnsExpired()
    {
        // Arrange
        var timeRemaining = new TimeRemaining(0, 0, 0, 0, true);

        // Act
        var formatted = timeRemaining.ToFormattedString();

        // Assert
        Assert.Equal("Expired", formatted);
    }

    [Fact]
    public void TimeRemaining_TotalSeconds_CalculatesCorrectly()
    {
        // Arrange
        var timeRemaining = new TimeRemaining(1, 2, 30, 45, false);

        // Act
        var totalSeconds = timeRemaining.TotalSeconds;

        // Assert
        // 1 day = 86400, 2 hours = 7200, 30 minutes = 1800, 45 seconds = 45
        // Total = 86400 + 7200 + 1800 + 45 = 95445
        Assert.Equal(95445L, totalSeconds);
    }
}
