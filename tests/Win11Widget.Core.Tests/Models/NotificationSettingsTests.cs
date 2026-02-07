namespace Win11Widget.Core.Tests.Models;

using Xunit;
using Win11Widget.Core.Models;

public class NotificationSettingsTests
{
    [Fact]
    public void Constructor_WithDefaultParameters_CreatesInstanceWithAllEnabled()
    {
        // Act
        var settings = new NotificationSettings();

        // Assert
        Assert.True(settings.Enabled);
        Assert.True(settings.NotifyOneDayBefore);
        Assert.True(settings.NotifyOneHourBefore);
        Assert.True(settings.NotifyAtEvent);
    }

    [Fact]
    public void Constructor_WithAllDisabled_CreatesInstanceWithAllDisabled()
    {
        // Act
        var settings = new NotificationSettings(false, false, false, false);

        // Assert
        Assert.False(settings.Enabled);
        Assert.False(settings.NotifyOneDayBefore);
        Assert.False(settings.NotifyOneHourBefore);
        Assert.False(settings.NotifyAtEvent);
    }

    [Fact]
    public void Constructor_WithPartiallyDisabled_CreatesInstanceWithCorrectStates()
    {
        // Act
        var settings = new NotificationSettings(true, false, true, false);

        // Assert
        Assert.True(settings.Enabled);
        Assert.False(settings.NotifyOneDayBefore);
        Assert.True(settings.NotifyOneHourBefore);
        Assert.False(settings.NotifyAtEvent);
    }

    [Fact]
    public void Constructor_WithEnabledFalse_AllThresholdsDisabledIgnored()
    {
        // Act - even though individual thresholds are true, Enabled is false
        var settings = new NotificationSettings(false, true, true, true);

        // Assert
        Assert.False(settings.Enabled);
        Assert.True(settings.NotifyOneDayBefore); // Individual settings still exist
        Assert.True(settings.NotifyOneHourBefore);
        Assert.True(settings.NotifyAtEvent);
    }

    [Fact]
    public void Enabled_PropertyCanBeSet()
    {
        // Arrange
        var settings = new NotificationSettings(true);

        // Act
        settings.Enabled = false;

        // Assert
        Assert.False(settings.Enabled);
    }

    [Fact]
    public void NotifyOneDayBefore_PropertyCanBeSet()
    {
        // Arrange
        var settings = new NotificationSettings();

        // Act
        settings.NotifyOneDayBefore = false;

        // Assert
        Assert.False(settings.NotifyOneDayBefore);
    }

    [Fact]
    public void NotifyOneHourBefore_PropertyCanBeSet()
    {
        // Arrange
        var settings = new NotificationSettings();

        // Act
        settings.NotifyOneHourBefore = false;

        // Assert
        Assert.False(settings.NotifyOneHourBefore);
    }

    [Fact]
    public void NotifyAtEvent_PropertyCanBeSet()
    {
        // Arrange
        var settings = new NotificationSettings();

        // Act
        settings.NotifyAtEvent = false;

        // Assert
        Assert.False(settings.NotifyAtEvent);
    }

    [Fact]
    public void Multiple_PropertyChanges_PersistCorrectly()
    {
        // Arrange
        var settings = new NotificationSettings();

        // Act
        settings.Enabled = false;
        settings.NotifyOneDayBefore = false;
        settings.NotifyOneHourBefore = true;
        settings.NotifyAtEvent = false;

        // Assert
        Assert.False(settings.Enabled);
        Assert.False(settings.NotifyOneDayBefore);
        Assert.True(settings.NotifyOneHourBefore);
        Assert.False(settings.NotifyAtEvent);
    }

    [Fact]
    public void CreateDefault_ReturnsDefaultInstance()
    {
        // Act
        var settings = NotificationSettings.CreateDefault();

        // Assert
        Assert.NotNull(settings);
        Assert.True(settings.Enabled);
        Assert.True(settings.NotifyOneDayBefore);
        Assert.True(settings.NotifyOneHourBefore);
        Assert.True(settings.NotifyAtEvent);
    }

    [Fact]
    public void CreateDefault_MultipleCallsReturnIndependentInstances()
    {
        // Act
        var settings1 = NotificationSettings.CreateDefault();
        var settings2 = NotificationSettings.CreateDefault();

        // Modify one
        settings1.Enabled = false;

        // Assert - should not affect the other
        Assert.False(settings1.Enabled);
        Assert.True(settings2.Enabled);
    }

    [Fact]
    public void Toggling_ThresholdWhileDisabled_StillAllowsToggle()
    {
        // Arrange
        var settings = new NotificationSettings(false, true, true, true);

        // Act
        settings.NotifyOneDayBefore = false;
        settings.NotifyOneHourBefore = false;

        // Assert
        Assert.False(settings.Enabled); // Master still false
        Assert.False(settings.NotifyOneDayBefore);
        Assert.False(settings.NotifyOneHourBefore);
        Assert.True(settings.NotifyAtEvent);
    }
}
