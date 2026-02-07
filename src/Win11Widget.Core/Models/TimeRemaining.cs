namespace Win11Widget.Core.Models;

/// <summary>
/// Represents the calculated time remaining for a countdown.
/// </summary>
public class TimeRemaining
{
    public TimeRemaining(int days, int hours, int minutes, int seconds, bool isExpired)
    {
        Days = days;
        Hours = hours;
        Minutes = minutes;
        Seconds = seconds;
        IsExpired = isExpired;
    }

    public int Days { get; }
    public int Hours { get; }
    public int Minutes { get; }
    public int Seconds { get; }
    public bool IsExpired { get; }

    /// <summary>
    /// Gets the total seconds remaining (for display as a single value).
    /// </summary>
    public long TotalSeconds => (long)Days * 86400 + Hours * 3600 + Minutes * 60 + Seconds;

    public string ToFormattedString()
    {
        if (IsExpired)
            return "Expired";

        var parts = new List<string>();
        if (Days > 0) parts.Add($"{Days}d");
        if (Hours > 0) parts.Add($"{Hours}h");
        if (Minutes > 0) parts.Add($"{Minutes}m");
        if (Seconds > 0 || parts.Count == 0) parts.Add($"{Seconds}s");

        return string.Join(" ", parts);
    }

    public override bool Equals(object? obj)
    {
        return obj is TimeRemaining remaining &&
               Days == remaining.Days &&
               Hours == remaining.Hours &&
               Minutes == remaining.Minutes &&
               Seconds == remaining.Seconds &&
               IsExpired == remaining.IsExpired;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Days, Hours, Minutes, Seconds, IsExpired);
    }
}
