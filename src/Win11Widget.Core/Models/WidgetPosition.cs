namespace Win11Widget.Core.Models;

/// <summary>
/// Represents the position of the widget on the screen.
/// </summary>
public class WidgetPosition
{
    public WidgetPosition(double x = 0, double y = 0, double width = 300, double height = 100)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    public double X { get; set; }
    public double Y { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }

    public static WidgetPosition CreateDefault()
    {
        return new WidgetPosition();
    }

    public override bool Equals(object? obj)
    {
        return obj is WidgetPosition position &&
               X == position.X &&
               Y == position.Y &&
               Width == position.Width &&
               Height == position.Height;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Width, Height);
    }
}
