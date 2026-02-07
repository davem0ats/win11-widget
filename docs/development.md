# Win11 Widget Development Setup

## Prerequisites

- Windows 11
- .NET 10 SDK or later
- Visual Studio 2022 (Community Edition or higher) with:
  - Windows desktop development workload
  - Windows App SDK extension
  - .NET desktop development workload

## Project Structure

```
win11-widget/
├── src/
│   ├── Win11Widget.Core/          # Business logic and domain models
│   │   ├── Models/                # Domain models (CountdownDate, TimeRemaining, etc.)
│   │   ├── Services/              # Service interfaces and implementations
│   │   └── ViewModels/            # MVVM ViewModels
│   └── Win11Widget/               # WinUI 3 application
│       ├── App.xaml(.cs)          # Application entry point
│       ├── MainWindow.xaml(.cs)   # Main UI window
│       └── Program.cs             # Entry point
└── tests/
    ├── Win11Widget.Core.Tests/    # Unit tests for business logic
    └── Win11Widget.Tests/         # Integration/UI tests
```

## Building the Project

### From Command Line

```powershell
cd c:\Users\davem\OneDrive\code\win11-widget
dotnet build
```

### Running Tests

```powershell
dotnet test
```

### Running the Application

```powershell
dotnet run --project src/Win11Widget/Win11Widget.csproj
```

## Development Guidelines

All code follows the guidance in `copilot-instructions.md`:

1. **TDD Approach**: Write tests first, then implementation
2. **Layered Architecture**: Models → ViewModels → Views
3. **MVVM Pattern**: UI logic separated from presentation
4. **Dependency Injection**: Decouple components
5. **SOLID Principles**: Maintain code quality and maintainability

## Key Features in Development

- ✓ Domain models and services
- ✓ Unit tests (TDD)
- ✓ MVVM ViewModels
- ✓ WinUI 3 application structure
- ⏳ UI implementation details
- ⏳ Settings dialog
- ⏳ Add/Edit countdown dialog
- ⏳ Notification integration

## Code Coverage

Run tests with coverage:

```powershell
dotnet test /p:CollectCoverage=true
```

## Next Steps

1. Complete WinUI 3 UI implementation
2. Implement settings and countdown addition dialogs
3. Integrate Windows notifications
4. Add unit tests for remaining features
5. Performance optimization and polishing
