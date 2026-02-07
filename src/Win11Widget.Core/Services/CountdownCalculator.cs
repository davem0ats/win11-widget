namespace Win11Widget.Core.Services;

using Win11Widget.Core.Models;

/// <summary>
/// Calculates the time remaining until a target date.
/// </summary>
public class CountdownCalculator : ICountdownCalculator
{
    private readonly IClock _clock;

    public CountdownCalculator(IClock clock)
    {
        _clock = clock ?? throw new ArgumentNullException(nameof(clock));
    }

    public TimeRemaining CalculateTimeRemaining(DateTime targetDate)
    {
        var now = _clock.UtcNow;

        // Ensure both dates are in UTC for comparison
        var target = targetDate.Kind == DateTimeKind.Unspecified
            ? DateTime.SpecifyKind(targetDate, DateTimeKind.Utc)
            : targetDate.ToUniversalTime();

        if (now >= target)
        {
            return new TimeRemaining(0, 0, 0, 0, true);
        }

        var remaining = target - now;
        var days = (int)remaining.TotalDays;
        var hours = remaining.Hours;
        var minutes = remaining.Minutes;
        var seconds = remaining.Seconds;

        return new TimeRemaining(days, hours, minutes, seconds, false);
    }
}
