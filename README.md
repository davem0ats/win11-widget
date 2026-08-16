# Win11 Widget 📅⏱️

A modern Windows 11-style countdown timer widget with a beautiful, polished user interface. Track multiple important dates with customizable display settings and position options.

![Windows 11 Style](https://img.shields.io/badge/Style-Windows%2011-0078D4?style=flat-square)
![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat-square)
![WPF](https://img.shields.io/badge/UI-WPF-512BD4?style=flat-square)
![Tests Passing](https://img.shields.io/badge/Tests-31%20Passing-success?style=flat-square)

## ✨ Features

- 📅 **Multiple Countdowns** - Track unlimited important dates simultaneously
- 🎨 **Customizable Appearance** - Font size, colors, transparency, and more
- 📍 **Flexible Positioning** - Drag anywhere or use quick position presets
- 💾 **Auto-Save** - All settings and countdowns persist automatically
- 🎯 **Modern UI** - Windows 11-inspired design with rounded corners and shadows
- ⌨️ **Keyboard Support** - Full keyboard navigation in dialogs
- 🧪 **Well Tested** - 31 unit tests with 100% business logic coverage

## 🖼️ Screenshots

**Main Widget**
- Modern borderless window with transparency
- Real-time countdown display (days, hours, minutes, seconds)
- Individual countdown cards with icons and actions
- Smooth hover effects and animations

**Settings Dialog**
- Comprehensive display customization
- Quick position presets (corners, center)
- Sound notification toggles
- Professional grouped interface

## 🚀 Getting Started

### Prerequisites
- Windows 10/11 (64-bit)
- .NET 10 SDK or Runtime

### Building from Source

```powershell
# Clone the repository
git clone https://github.com/davem0ats/win11-widget.git
cd win11-widget

# Build the solution
dotnet build

# Run the widget
dotnet run --project src/Win11Widget/Win11Widget.csproj
```

### Running Tests

```powershell
# Run all unit tests
dotnet test

# Run with detailed output
dotnet test --verbosity normal
```

## 🏗️ Architecture

### Technology Stack
- **.NET 10** - Modern cross-platform framework
- **WPF** - Windows Presentation Foundation for rich UI
- **C# 12** - Latest language features
- **xUnit** - Testing framework
- **Moq** - Mocking for unit tests

### Project Structure
```
win11-widget/
├── src/
│   ├── Win11Widget.Core/          # Business logic library
│   │   ├── Models/                 # Domain models
│   │   ├── Services/               # Business services
│   │   └── ViewModels/             # MVVM view models
│   └── Win11Widget/                # WPF application
│       ├── Views/                  # XAML windows/dialogs
│       └── Resources/              # Styles and assets
├── tests/
│   ├── Win11Widget.Core.Tests/    # Business logic tests (31 tests)
│   └── Win11Widget.Tests/          # UI integration tests
└── docs/                           # Documentation
```

### Design Pattern
- **MVVM (Model-View-ViewModel)** - Separation of concerns
- **Dependency Injection** - Loose coupling for testability
- **Repository Pattern** - JSON-based configuration persistence

##  Customization

All settings are accessible through the modern Settings dialog ( button):

### Display Settings
- **Font Size**: 10-48 points
- **Foreground Color**: Hex color codes (e.g., #FFFFFF)
- **Background Color**: Hex color codes (e.g., #0078D4)
- **Opacity**: 10%-100%
- **Always On Top**: Keep widget above other windows

### Position Settings
- **Drag & Drop**: Click and drag to reposition
- **Manual Entry**: Specify exact X/Y coordinates
- **Quick Presets**:
  -  Top Left
  -  Top Right
  -  Center
  -  Bottom Left
  -  Bottom Right

### Sound Settings
- Toggle sound notifications on/off

##  Usage

1. **Add a Countdown**
   - Click " Add Countdown" button
   - Enter a label (e.g., "Birthday", "Vacation")
   - Select a target date
   - Click "Add"

2. **Remove a Countdown**
   - Click the  button on any countdown card
   - Confirm the deletion

3. **Adjust Settings**
   - Click " Settings" button
   - Customize appearance, position, and sounds
   - Click "Save" to apply

4. **Reposition Widget**
   - Click and drag the window anywhere
   - Or use Quick Preset buttons in Settings
   - Position saves automatically

##  Configuration

Settings are stored in: `%LOCALAPPDATA%\Win11Widget\config.json`

The configuration file includes:
- List of countdowns with labels and target dates
- Display preferences (colors, font size, opacity)
- Window position (X, Y coordinates)
- Sound notification preferences

##  Testing

Developed using **Test-Driven Development (TDD)**:
-  31 unit tests passing
-  100% business logic coverage
-  Countdown calculation algorithms
-  Time formatting logic
-  Configuration persistence
-  ViewModel behavior
-  Edge cases (expired dates, timezones)

## 📚 Documentation

Additional documentation is available:
- [**instructions.md**](docs/instructions.md) - Detailed project requirements and specifications
- [**development.md**](docs/development.md) - Development guidelines and workflow
- [**testing.md**](docs/testing.md) - Testing strategy and test coverage details
- [**copilot-instructions.md**](.github/copilot-instructions.md) - GitHub Copilot configuration (in `.github/`)
- [**licenses.md**](docs/licenses.md) - All NuGet package licenses including transitive dependencies

##  Distribution

This widget is **private** and not published to the public Windows Store. It's designed for personal use.

##  Contributing

This is a personal project, but suggestions and feedback are welcome through issues.

##  License

See LICENSE file for details.

##  Acknowledgments

- Design inspired by Windows 11 UI guidelines
- Built with modern .NET and WPF technologies
- Test-driven development methodology
