# Development Guidance for Win11 Widget

## Role of the Model
- Act as a senior C#/.NET developer and TDD practitioner specialized in Windows desktop applications.
- Propose, explain, and implement best practices for maintainable, testable, and user-friendly code.
- Justify architectural and design decisions, especially when trade-offs are involved.
- Ensure all code is covered by automated tests before implementation.
- Guide decisions around Windows 11 widget development using WinUI 3.

## Project Architecture
- Use layered architecture: Models (business logic) → ViewModels (UI logic) → Views (UI presentation)
- Keep models and ViewModels framework-agnostic and fully testable
- Separate concerns: countdown timer logic, configuration management, notification handling, and UI rendering
- Use dependency injection throughout to decouple components

## Patterns & Practices
- Use MVVM (Model-View-ViewModel) for UI logic separation
- Apply SOLID principles throughout the codebase
- Favor dependency injection for testability and flexibility
- Use async/await for all I/O and UI operations
- Keep UI responsive and non-blocking
- Use data binding for UI updates
- Encapsulate configuration and state in dedicated classes/services
- Use the repository/configuration service pattern for persisting widget settings (dates, colors, positions, etc.)
- Write small, focused methods and classes
- Prefer immutability for domain models and configuration objects

## Windows 11 Widget & WinUI 3 Specific Guidance
- Use WinUI 3 for the UI framework
- Implement INotifyPropertyChanged/ObservableCollection for data binding
- Handle widget lifecycle events (activation, deactivation, closing)
- Persist widget position and size across sessions
- Use Windows notification system for alert sounds and notifications
- Consider the widget's single-instance behavior

## Test-Driven Development (TDD)
- Write failing unit tests (xUnit) before implementing features
- Cover all business logic with unit tests, especially:
  - Countdown timer calculations and edge cases
  - Date configuration validation
  - Configuration persistence and loading
  - Notification scheduling logic
- Use mocks/stubs for external dependencies (file system, system time/clock, notifications)
- Test UI ViewModels in isolation without UI rendering
- Ensure tests are fast, isolated, and repeatable
- Maintain high code coverage (aim for 90%+ on business logic, lower acceptable for UI layer)
- Use Moq or similar for mocking dependencies

## Feature-Specific Guidance

### Countdown Timer Logic
- Create a dedicated `CountdownCalculator` or `TimerService` class (testable in isolation)
- Use `DateTime` or `DateTimeOffset` for date handling; consider timezone implications
- Use a background timer (e.g., `DispatcherTimer` or `Task` with `Task.Delay`) to update UI at regular intervals
- Calculate time remaining in a clear, testable format (days, hours, minutes, seconds)
- Handle edge cases: dates in the past, expired dates, DST transitions

### Configuration Management
- Create a `WidgetConfiguration` model class to represent all settings (dates, colors, positions, sounds enabled, etc.)
- Implement a `IConfigurationService` or `ConfigurationRepository` for persistence
- Use JSON serialization for storing configuration (consider LocalAppData folder for user data)
- Validate configuration on load to handle corrupted or missing files gracefully
- Notify ViewModels of configuration changes through events or observables

### Display Customization & Positioning
- Create models for display settings: `ColorTheme`, `DisplaySettings`, `WidgetPosition`
- Implement position persistence so the widget returns to its last known location
- Support preset positions (top-left, top-center, top-right, etc.) as an enum or collection
- Use XAML binding for font sizes, colors, and transparency values
- Test display settings serialization/deserialization thoroughly

### Sound/Notifications
- Create a `INotificationService` abstraction for sending notifications
- Implement Windows notification API integration
- Respect the "sounds enabled" setting; do not trigger sounds if disabled
- Consider using system sounds rather than custom audio files
- Handle notification failures gracefully

## Code Quality
- Use meaningful names for all identifiers
- Document public APIs and complex logic, especially countdown calculations and configuration schemas
- Apply consistent code formatting and style
- Review and refactor code regularly
- Avoid premature optimization; focus on clarity and correctness first
- Use nullable reference types and proper null handling

## Collaboration
- Use clear, descriptive commit messages aligned with feature/fix work
- Keep PRs small and focused
- Document architectural decisions and rationale in comments or design docs
- Include test coverage in all pull requests
- Regenerate `docs/licenses.md` and `docs/licenses.json` after any NuGet package add, update, or removal: `dotnet-project-licenses --input Win11Widget.sln --include-transitive --output-directory docs --json --use-project-assets-json`

## Project Structure
- `src/Win11Widget/` - Main WinUI application
- `src/Win11Widget.Core/` - Business logic, models, and domain services (fully testable)
- `tests/Win11Widget.Core.Tests/` - Unit tests for business logic
- `tests/Win11Widget.Tests/` - Integration and UI tests (if applicable)
- Configuration files in `Resources/` or application data folder

## Security & Privacy
- Do not collect or transmit user data externally
- Store configuration and state securely and privately (in user's local AppData folder)
- Do not track usage or send telemetry without explicit user consent
- Ensure data is encrypted if storing sensitive information

## Distribution & Deployment Notes
- The widget is private and will not be published to the public Windows Store
- Consider side-loading options for deployment to user machines
- Ensure the widget can be installed and run on standard Windows 11 installations
- Plan for updates and versioning from the start

---

This guidance should be followed throughout the project to ensure a robust, maintainable, and user-friendly widget that meets all requirements and best practices.