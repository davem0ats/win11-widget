# NuGet Package Licenses

All direct and transitive NuGet dependencies for the Win11Widget solution, including test projects.

Generated with [dotnet-project-licenses](https://github.com/tomchavakis/nuget-license) v2.7.1.
Machine-readable data: [licenses.json](licenses.json).

> Last updated: 2026-08-16 (.NET 10 / xUnit v3 baseline)

## License Summary

| License      | Count |
|--------------|------:|
| MIT          | 13    |
| Apache-2.0   | 9     |
| BSD-3-Clause | 1     |

## Full Package List

| Package | Version | License | URL |
|---------|---------|---------|-----|
| Castle.Core | 5.1.1 | Apache-2.0 | https://licenses.nuget.org/Apache-2.0 |
| Microsoft.ApplicationInsights | 2.23.0 | MIT | https://licenses.nuget.org/MIT |
| Microsoft.Bcl.AsyncInterfaces | 6.0.0 | MIT | https://licenses.nuget.org/MIT |
| Microsoft.CodeCoverage | 18.9.0 | MIT | https://licenses.nuget.org/MIT |
| Microsoft.NET.Test.Sdk | 18.9.0 | MIT | https://licenses.nuget.org/MIT |
| Microsoft.Testing.Extensions.Telemetry | 1.9.0 | MIT | https://licenses.nuget.org/MIT |
| Microsoft.Testing.Extensions.TrxReport.Abstractions | 1.9.0 | MIT | https://licenses.nuget.org/MIT |
| Microsoft.Testing.Platform | 1.9.0 | MIT | https://licenses.nuget.org/MIT |
| Microsoft.Testing.Platform.MSBuild | 1.9.0 | MIT | https://licenses.nuget.org/MIT |
| Microsoft.TestPlatform.ObjectModel | 18.9.0 | MIT | https://licenses.nuget.org/MIT |
| Microsoft.TestPlatform.TestHost | 18.9.0 | MIT | https://licenses.nuget.org/MIT |
| Microsoft.Win32.Registry | 5.0.0 | MIT | https://licenses.nuget.org/MIT |
| Moq | 4.20.72 | BSD-3-Clause | https://licenses.nuget.org/BSD-3-Clause |
| System.Diagnostics.EventLog | 6.0.0 | MIT | https://licenses.nuget.org/MIT |
| xunit.analyzers | 1.25.0 | Apache-2.0 | https://licenses.nuget.org/Apache-2.0 |
| xunit.runner.visualstudio | 4.0.0 | Apache-2.0 | https://licenses.nuget.org/Apache-2.0 |
| xunit.v3 | 3.2.0 | Apache-2.0 | https://licenses.nuget.org/Apache-2.0 |
| xunit.v3.assert | 3.2.0 | Apache-2.0 | https://licenses.nuget.org/Apache-2.0 |
| xunit.v3.common | 3.2.0 | Apache-2.0 | https://licenses.nuget.org/Apache-2.0 |
| xunit.v3.core.mtp-v1 | 3.2.0 | Apache-2.0 | https://licenses.nuget.org/Apache-2.0 |
| xunit.v3.extensibility.core | 3.2.0 | Apache-2.0 | https://licenses.nuget.org/Apache-2.0 |
| xunit.v3.mtp-v1 | 3.2.0 | Apache-2.0 | https://licenses.nuget.org/Apache-2.0 |
| xunit.v3.runner.common | 3.2.0 | Apache-2.0 | https://licenses.nuget.org/Apache-2.0 |
| xunit.v3.runner.inproc.console | 3.2.0 | Apache-2.0 | https://licenses.nuget.org/Apache-2.0 |

## Notes

- All licenses (MIT, Apache-2.0, BSD-3-Clause) are permissive open-source licenses compatible with private distribution.
- Test-only packages (xunit.\*, Moq, Microsoft.NET.Test.Sdk, Microsoft.CodeCoverage, Microsoft.TestPlatform.\*, Microsoft.Testing.\*) are not shipped with the application binary.
- To regenerate: `dotnet-project-licenses --input Win11Widget.sln --include-transitive --output-directory docs --json --use-project-assets-json`
