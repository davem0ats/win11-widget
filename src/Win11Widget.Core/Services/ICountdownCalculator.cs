namespace Win11Widget.Core.Services;

using Win11Widget.Core.Models;

/// <summary>
/// Calculates the time remaining until a target date.
/// </summary>
public interface ICountdownCalculator
{
    /// <summary>
    /// Calculates the time remaining until the target date.
    /// </summary>
    TimeRemaining CalculateTimeRemaining(DateTime targetDate);
}
