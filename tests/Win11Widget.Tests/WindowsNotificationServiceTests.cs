namespace Win11Widget.Tests.Services;

using System.Windows.Forms;
using Xunit;
using Moq;
using Win11Widget.Core.Models;
using Win11Widget.Core.Services;
using Win11Widget.Services;

public class WindowsNotificationServiceTests
{
    private readonly Mock<INotificationIcon> _mockNotificationIcon;
    private readonly Mock<IClock> _mockClock;
    private readonly WindowsNotificationService _service;

    public WindowsNotificationServiceTests()
    {
        _mockNotificationIcon = new Mock<INotificationIcon>();
        _mockNotificationIcon.Setup(ni => ni.Visible).Returns(true);
        _mockClock = new Mock<IClock>();
        _service = new WindowsNotificationService(_mockNotificationIcon.Object);
    }

    [Fact]
    public void Constructor_WithoutNotifyIcon_DoesNotThrow()
    {
        // Act & Assert
        var service = new WindowsNotificationService(null);
        Assert.NotNull(service);
    }

    [Fact]
    public void CheckAndNotify_WithDisabledSettings_DoesNotShowNotifications()
    {
        // Arrange
        var now = new DateTime(2026, 2, 7, 12, 0, 0, DateTimeKind.Utc);
        var targetDate = new DateTime(2026, 2, 8, 12, 0, 0, DateTimeKind.Utc); // 1 day away
        var countdowns = new[] { new CountdownDate("Test", targetDate) };
        var settings = new NotificationSettings(enabled: false, true, true, true);
        _mockClock.Setup(c => c.UtcNow).Returns(now);

        // Act
        _service.CheckAndNotify(countdowns, settings, _mockClock.Object);

        // Assert - no balloon tip shown
        _mockNotificationIcon.Verify(ni => ni.ShowBalloonTip(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ToolTipIcon>()), Times.Never);
    }

    [Fact]
    public void CheckAndNotify_WithAtEventThreshold_ShowsNotificationWhenExpired()
    {
        // Arrange
        var now = new DateTime(2026, 2, 7, 12, 0, 0, DateTimeKind.Utc);
        var targetDate = new DateTime(2026, 2, 7, 12, 0, 0, DateTimeKind.Utc); // Exactly now
        var countdowns = new[] { new CountdownDate("Test Event", targetDate) };
        var settings = new NotificationSettings(true, false, false, true); // Only at event
        _mockClock.Setup(c => c.UtcNow).Returns(now);

        // Act
        _service.CheckAndNotify(countdowns, settings, _mockClock.Object);

        // Assert
        _mockNotificationIcon.Verify(
            ni => ni.ShowBalloonTip(5000, "Event Now!", "Test Event is happening now!", ToolTipIcon.Info),
            Times.Once);
    }

    [Fact]
    public void CheckAndNotify_WithOneHourThreshold_ShowsNotificationWithinThreshold()
    {
        // Arrange
        var now = new DateTime(2026, 2, 7, 12, 0, 0, DateTimeKind.Utc);
        var targetDate = new DateTime(2026, 2, 7, 12, 58, 0, DateTimeKind.Utc); // 58 minutes away
        var countdowns = new[] { new CountdownDate("Meeting", targetDate) };
        var settings = new NotificationSettings(true, false, true, false); // Only 1 hour before
        _mockClock.Setup(c => c.UtcNow).Returns(now);

        // Act
        _service.CheckAndNotify(countdowns, settings, _mockClock.Object);

        // Assert
        _mockNotificationIcon.Verify(
            ni => ni.ShowBalloonTip(5000, "Event in 1 Hour!", "Meeting is coming up in 1 hour.", ToolTipIcon.Info),
            Times.Once);
    }

    [Fact]
    public void CheckAndNotify_WithOneHourThreshold_DoesNotShowOutsideThreshold()
    {
        // Arrange
        var now = new DateTime(2026, 2, 7, 12, 0, 0, DateTimeKind.Utc);
        var targetDate = new DateTime(2026, 2, 7, 13, 31, 0, DateTimeKind.Utc); // 1.5 hours away
        var countdowns = new[] { new CountdownDate("Meeting", targetDate) };
        var settings = new NotificationSettings(true, false, true, false);
        _mockClock.Setup(c => c.UtcNow).Returns(now);

        // Act
        _service.CheckAndNotify(countdowns, settings, _mockClock.Object);

        // Assert - no notification shown (outside 1 hour threshold)
        _mockNotificationIcon.Verify(ni => ni.ShowBalloonTip(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ToolTipIcon>()), Times.Never);
    }

    [Fact]
    public void CheckAndNotify_WithOneDayThreshold_ShowsNotificationWithinThreshold()
    {
        // Arrange
        var now = new DateTime(2026, 2, 7, 12, 0, 0, DateTimeKind.Utc);
        var targetDate = new DateTime(2026, 2, 8, 11, 45, 0, DateTimeKind.Utc); // 23 hours 45 minutes away
        var countdowns = new[] { new CountdownDate("Anniversary", targetDate) };
        var settings = new NotificationSettings(true, true, false, false); // Only 1 day before
        _mockClock.Setup(c => c.UtcNow).Returns(now);

        // Act
        _service.CheckAndNotify(countdowns, settings, _mockClock.Object);

        // Assert
        _mockNotificationIcon.Verify(
            ni => ni.ShowBalloonTip(5000, "Event Tomorrow!", "Anniversary is coming up in 1 day.", ToolTipIcon.Info),
            Times.Once);
    }

    [Fact]
    public void CheckAndNotify_WithOneDayThreshold_DoesNotShowOutsideThreshold()
    {
        // Arrange
        var now = new DateTime(2026, 2, 7, 12, 0, 0, DateTimeKind.Utc);
        var targetDate = new DateTime(2026, 2, 9, 12, 1, 0, DateTimeKind.Utc); // 24 hours 1 minute away
        var countdowns = new[] { new CountdownDate("Anniversary", targetDate) };
        var settings = new NotificationSettings(true, true, false, false);
        _mockClock.Setup(c => c.UtcNow).Returns(now);

        // Act
        _service.CheckAndNotify(countdowns, settings, _mockClock.Object);

        // Assert - no notification shown (outside 24 hour threshold)
        _mockNotificationIcon.Verify(ni => ni.ShowBalloonTip(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ToolTipIcon>()), Times.Never);
    }

    [Fact]
    public void CheckAndNotify_PreventsDuplicateNotifications()
    {
        // Arrange
        var now = new DateTime(2026, 2, 7, 12, 0, 0, DateTimeKind.Utc);
        var targetDate = new DateTime(2026, 2, 7, 12, 0, 0, DateTimeKind.Utc);
        var countdowns = new[] { new CountdownDate("Test", targetDate) };
        var settings = new NotificationSettings(true, false, false, true);
        _mockClock.Setup(c => c.UtcNow).Returns(now);

        // Act - call twice
        _service.CheckAndNotify(countdowns, settings, _mockClock.Object);
        _service.CheckAndNotify(countdowns, settings, _mockClock.Object);

        // Assert - only shown once
        _mockNotificationIcon.Verify(
            ni => ni.ShowBalloonTip(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ToolTipIcon>()),
            Times.Once);
    }

    [Fact]
    public void CheckAndNotify_WithMultipleCountdowns_ShowsAllNotifications()
    {
        // Arrange
        var now = new DateTime(2026, 2, 7, 12, 0, 0, DateTimeKind.Utc);
        var countdowns = new[]
        {
            new CountdownDate("Event 1", new DateTime(2026, 2, 7, 12, 0, 0, DateTimeKind.Utc)), // At event
            new CountdownDate("Event 2", new DateTime(2026, 2, 7, 12, 58, 0, DateTimeKind.Utc))  // 1 hour
        };
        var settings = new NotificationSettings(true, false, true, true);
        _mockClock.Setup(c => c.UtcNow).Returns(now);

        // Act
        _service.CheckAndNotify(countdowns, settings, _mockClock.Object);

        // Assert - both notifications shown
        _mockNotificationIcon.Verify(ni => ni.ShowBalloonTip(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ToolTipIcon>()), Times.Exactly(2));
    }

    [Fact]
    public void CheckAndNotify_WithFutureDate_DoesNotShowNotification()
    {
        // Arrange
        var now = new DateTime(2026, 2, 7, 12, 0, 0, DateTimeKind.Utc);
        var targetDate = new DateTime(2026, 3, 7, 12, 0, 0, DateTimeKind.Utc); // 1 month away
        var countdowns = new[] { new CountdownDate("Future Event", targetDate) };
        var settings = new NotificationSettings(true, true, true, true);
        _mockClock.Setup(c => c.UtcNow).Returns(now);

        // Act
        _service.CheckAndNotify(countdowns, settings, _mockClock.Object);

        // Assert - no notification shown
        _mockNotificationIcon.Verify(ni => ni.ShowBalloonTip(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ToolTipIcon>()), Times.Never);
    }

    [Fact]
    public void CheckAndNotify_WithOneDayDisabled_DoesNotShowOneDayNotification()
    {
        // Arrange
        var now = new DateTime(2026, 2, 7, 12, 0, 0, DateTimeKind.Utc);
        var targetDate = new DateTime(2026, 2, 8, 11, 45, 0, DateTimeKind.Utc); // 23 hours 45 minutes (within 1 day)
        var countdowns = new[] { new CountdownDate("Event", targetDate) };
        var settings = new NotificationSettings(true, false, false, false); // Disabled one day
        _mockClock.Setup(c => c.UtcNow).Returns(now);

        // Act
        _service.CheckAndNotify(countdowns, settings, _mockClock.Object);

        // Assert
        _mockNotificationIcon.Verify(ni => ni.ShowBalloonTip(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ToolTipIcon>()), Times.Never);
    }

    [Fact]
    public void CheckAndNotify_WithEmptyCountdowns_DoesNotThrow()
    {
        // Arrange
        var countdowns = Enumerable.Empty<CountdownDate>();
        var settings = new NotificationSettings(true, true, true, true);
        _mockClock.Setup(c => c.UtcNow).Returns(DateTime.UtcNow);

        // Act & Assert
        _service.CheckAndNotify(countdowns, settings, _mockClock.Object);
        _mockNotificationIcon.Verify(ni => ni.ShowBalloonTip(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ToolTipIcon>()), Times.Never);
    }

    [Fact]
    public async Task SendNotificationAsync_CallsShowBalloonTip()
    {
        // Act
        await _service.SendNotificationAsync("Test Title", "Test Message");

        // Assert
        _mockNotificationIcon.Verify(
            ni => ni.ShowBalloonTip(5000, "Test Title", "Test Message", ToolTipIcon.Info),
            Times.Once);
    }

    [Fact]
    public async Task SendNotificationAsync_WithInvisibleIcon_DoesNotShowBalloonTip()
    {
        // Arrange
        _mockNotificationIcon.Setup(ni => ni.Visible).Returns(false);

        // Act
        await _service.SendNotificationAsync("Test Title", "Test Message");

        // Assert
        _mockNotificationIcon.Verify(
            ni => ni.ShowBalloonTip(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ToolTipIcon>()),
            Times.Never);
    }

    [Fact]
    public async Task SendNotificationAsync_WithNullIcon_DoesNotThrow()
    {
        // Arrange
        var service = new WindowsNotificationService(null);

        // Act & Assert
        await service.SendNotificationAsync("Test Title", "Test Message");
    }

    [Fact]
    public async Task PlayNotificationSoundAsync_WithSoundEnabled_PlaysSound()
    {
        // Act
        await _service.PlayNotificationSoundAsync(true);

        // Assert - no exception thrown (sound played)
    }

    [Fact]
    public async Task PlayNotificationSoundAsync_WithSoundDisabled_DoesNotPlaySound()
    {
        // Act
        await _service.PlayNotificationSoundAsync(false);

        // Assert - no exception thrown
    }

    [Fact]
    public void CheckAndNotify_DifferentThresholdsTrackSeparately()
    {
        // Arrange
        var now = new DateTime(2026, 2, 7, 12, 0, 0, DateTimeKind.Utc);
        var targetDate = new DateTime(2026, 2, 7, 12, 58, 0, DateTimeKind.Utc); // 58 minutes - within both 1hr and 1day
        var countdowns = new[] { new CountdownDate("Event", targetDate) };
        var settings = new NotificationSettings(true, true, true, false); // 1 day and 1 hour enabled
        _mockClock.Setup(c => c.UtcNow).Returns(now);

        // Act
        _service.CheckAndNotify(countdowns, settings, _mockClock.Object);

        // Assert - only 1 hour notification shown (1 day threshold applies to 23.5-24h range)
        _mockNotificationIcon.Verify(
            ni => ni.ShowBalloonTip(5000, "Event in 1 Hour!", "Event is coming up in 1 hour.", ToolTipIcon.Info),
            Times.Once);
    }
}

