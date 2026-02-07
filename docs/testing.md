# Creating and Running Tests

## Running All Tests

```powershell
cd c:\Users\davem\OneDrive\code\win11-widget
dotnet test
```

## Running Tests for a Specific Project

```powershell
dotnet test tests/Win11Widget.Core.Tests/Win11Widget.Core.Tests.csproj
```

## Running Tests with Verbose Output

```powershell
dotnet test --verbosity detailed
```

## Running a Specific Test Class

```powershell
dotnet test --filter FullyQualifiedName~CountdownCalculatorTests
```

## Running a Specific Test Method

```powershell
dotnet test --filter FullyQualifiedName~CountdownCalculatorTests.CalculateTimeRemaining_WithFutureDate_ReturnsCorrectDaysHoursMinutesSeconds
```

## Code Coverage

```powershell
dotnet test /p:CollectCoverage=true /p:CoverletOutput=coverage/ /p:CoverletOutputFormat=lcov
```

## Building the Solution

```powershell
dotnet build
```

## Building with Release Configuration

```powershell
dotnet build --configuration Release
```

## Cleaning Build Artifacts

```powershell
dotnet clean
```

## Running the Application (when WinUI 3 changes are complete)

```powershell
dotnet run --project src/Win11Widget/Win11Widget.csproj
```

## Adding NuGet Packages

```powershell
dotnet add src/Win11Widget.Core/Win11Widget.Core.csproj package <PackageName>
```

## Restore Dependencies

```powershell
dotnet restore
```
