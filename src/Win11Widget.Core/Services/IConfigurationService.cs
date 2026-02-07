namespace Win11Widget.Core.Services;

using Win11Widget.Core.Models;

/// <summary>
/// Manages persistence and loading of widget configuration.
/// </summary>
public interface IConfigurationService
{
    /// <summary>
    /// Loads the widget configuration. Returns default configuration if not found.
    /// </summary>
    Task<WidgetConfiguration> LoadConfigurationAsync();

    /// <summary>
    /// Saves the widget configuration.
    /// </summary>
    Task SaveConfigurationAsync(WidgetConfiguration configuration);
}
