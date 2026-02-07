namespace Win11Widget.Core.Services;

/// <summary>
/// Provides the current system time in UTC.
/// </summary>
public class SystemClock : IClock
{
    public DateTime UtcNow => DateTime.UtcNow;
}
