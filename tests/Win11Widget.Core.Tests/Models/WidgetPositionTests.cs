namespace Win11Widget.Core.Tests.Models;

using Xunit;
using Win11Widget.Core.Models;

public class WidgetPositionTests
{
    [Fact]
    public void Constructor_WithDefaultValues_CreatesInstance()
    {
        // Act
        var position = new WidgetPosition();

        // Assert
        Assert.Equal(0, position.X);
        Assert.Equal(0, position.Y);
        Assert.Equal(300, position.Width);
        Assert.Equal(100, position.Height);
    }

    [Fact]
    public void Constructor_WithCustomValues_CreatesInstance()
    {
        // Act
        var position = new WidgetPosition(100, 200, 400, 150);

        // Assert
        Assert.Equal(100, position.X);
        Assert.Equal(200, position.Y);
        Assert.Equal(400, position.Width);
        Assert.Equal(150, position.Height);
    }

    [Fact]
    public void Equals_WithSameValues_ReturnsTrue()
    {
        // Arrange
        var position1 = new WidgetPosition(100, 200, 400, 150);
        var position2 = new WidgetPosition(100, 200, 400, 150);

        // Act & Assert
        Assert.Equal(position1, position2);
    }

    [Fact]
    public void Equals_WithDifferentValues_ReturnsFalse()
    {
        // Arrange
        var position1 = new WidgetPosition(100, 200, 400, 150);
        var position2 = new WidgetPosition(100, 200, 400, 160);

        // Act & Assert
        Assert.NotEqual(position1, position2);
    }

    [Fact]
    public void CreateDefault_ReturnsDefaultPosition()
    {
        // Act
        var position = WidgetPosition.CreateDefault();

        // Assert
        Assert.Equal(0, position.X);
        Assert.Equal(0, position.Y);
        Assert.Equal(300, position.Width);
        Assert.Equal(100, position.Height);
    }

    [Fact]
    public void CreateDefault_MultipleCallsReturnIndependentInstances()
    {
        // Act
        var position1 = WidgetPosition.CreateDefault();
        var position2 = WidgetPosition.CreateDefault();

        // Assert
        Assert.NotSame(position1, position2);
        Assert.Equal(position1, position2);
    }
}
