namespace Win11Widget.Core.Models;

/// <summary>
/// Represents the display settings for the widget.
/// </summary>
public class DisplaySettings
{
    public DisplaySettings(
        int fontSize = 14,
        string foregroundColor = "#FFFFFF",
        string backgroundColor = "#000000",
        double opacity = 0.9,
        bool alwaysOnTop = false)
    {
        FontSize = fontSize;
        ForegroundColor = foregroundColor;
        BackgroundColor = backgroundColor;
        Opacity = opacity;
        AlwaysOnTop = alwaysOnTop;
    }

    public int FontSize { get; set; }
    public string ForegroundColor { get; set; }
    public string BackgroundColor { get; set; }
    public double Opacity { get; set; }
    public bool AlwaysOnTop { get; set; }

    public static DisplaySettings CreateDefault()
    {
        return new DisplaySettings();
    }
}
