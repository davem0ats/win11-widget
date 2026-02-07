namespace Win11Widget.Core.Services;

/// <summary>
/// Provides an abstraction for system time to enable testable countdown calculations.
/// </summary>
public interface IClock
{
    DateTime UtcNow { get; }
}
