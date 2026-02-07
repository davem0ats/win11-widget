## Win11 Widget

A Windows 11-style widget that displays countdown timers to a configurable list of dates with a modern, polished user interface.

### Technology Stack
- **.NET 8** - Windows desktop framework
- **WPF** - Windows Presentation Foundation for UI
- **C#** - Primary programming language
- **xUnit** - Test-driven development with 31 passing unit tests
- **Moq** - Mocking framework for testing

### Architecture
- **MVVM Pattern** - Model-View-ViewModel architecture
- **Dependency Injection** - Loose coupling for testability
- **Layered Design**:
  - `Win11Widget.Core` - Framework-agnostic business logic library
  - `Win11Widget` - WPF application UI layer
  - `Win11Widget.Core.Tests` - Unit tests for business logic
  - `Win11Widget.Tests` - UI/Integration tests

### Features

#### ✅ Countdown Management
- **Add Countdowns**: Modern dialog with date picker and label input
- **Remove Countdowns**: Delete button on each countdown with confirmation
- **Real-time Display**: Automatic time calculation showing days, hours, minutes, seconds
- **Formatted Output**: Clean, readable time format (e.g., "15 days, 3 hours, 45 minutes, 30 seconds")
- **Persistent Storage**: JSON-based configuration in LocalAppData folder

#### ✅ Display Settings
- **Font Size**: Adjustable from 10-48 points with slider
- **Foreground Color**: Customizable text color (hex color codes)
- **Background Color**: Customizable window background (hex color codes)
- **Opacity**: Transparency slider (10%-100%)
- **Always On Top**: Toggle to keep widget above other windows

#### ✅ Position Settings
- **Manual Positioning**: Drag-and-drop the widget anywhere on screen
- **Auto-save Position**: Position saved automatically when moved
- **Quick Presets**: One-click positioning for:
  - Top Left corner
  - Top Right corner
  - Center screen
  - Bottom Left corner
  - Bottom Right corner
- **Custom Coordinates**: Manual X/Y position entry

#### ✅ Sound Settings
- **Enable/Disable**: Toggle sound notifications on/off
- No custom or system sound selection (as per requirements)

#### ✅ Modern UI Design
- **Windows 11 Style**: Modern, clean aesthetic matching Windows 11 design language
- **Borderless Window**: Custom chrome with rounded corners and drop shadows
- **Transparency Support**: Smooth transparency effects
- **Rounded Corners**: 12px corner radius on main window
- **Drop Shadows**: Soft shadows for depth perception
- **Hover Effects**: Interactive feedback on all buttons
- **Color Scheme**: Professional blue accent (#0078D4) with neutral grays
- **Icons & Emojis**: Visual indicators throughout the interface
- **Smooth Animations**: Hover and press states on interactive elements

#### ✅ User Experience
- **Visual Feedback**: Clear hover states and button interactions
- **Validation**: Input validation with helpful error messages
- **Confirmation Dialogs**: Confirm before deleting countdowns
- **Keyboard Support**: Enter/Escape key support in dialogs
- **Scrollable Content**: Lists scroll when content exceeds window size
- **Tooltips**: Helpful hints on action buttons

### Testing
- **31 Unit Tests** - All passing with 100% business logic coverage
- **TDD Approach** - Tests written first, then implementation
- **Test Categories**:
  - Countdown calculation logic
  - Time remaining formatting
  - Configuration persistence (JSON)
  - ViewModel behavior and property change notifications
  - Edge cases (expired dates, timezone handling)

### Configuration Storage
- **Location**: `%LOCALAPPDATA%\Win11Widget\config.json`
- **Format**: JSON with structured data
- **Contents**:
  - List of countdown dates with labels
  - Display settings (colors, font, opacity, always-on-top)
  - Window position (X, Y coordinates)
  - Sound preferences

### Notes
- Widget is private (not published to Windows Store)
- All business logic developed using TDD with xUnit
- Cross-cutting concerns separated into testable services
- Fully functional with persistent configuration
- Modern, professional appearance suitable for daily use

    
