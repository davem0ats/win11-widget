namespace Win11Widget.Core.Models;

/// <summary>
/// Represents a countdown target date with an optional label.
/// </summary>
public class CountdownDate
{
    public CountdownDate(string label, DateTime targetDate)
    {
        Label = label ?? throw new ArgumentNullException(nameof(label));
        TargetDate = targetDate;
    }

    public string Label { get; }
    public DateTime TargetDate { get; }
    public Guid Id { get; } = Guid.NewGuid();

    public override bool Equals(object? obj)
    {
        return obj is CountdownDate date && Id == date.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
