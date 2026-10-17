# BaseTemplatePakage

Build the .NET 10 template package (no framework restore or generated application required):

```powershell
.\build-package.bat
```

Optionally verify a generated application using the local Base packages:

```powershell
.\build-package.bat -Verify -BasePackageDirectory C:\Base\artifacts\branch-nuget
```

First run `C:\Base\scripts\Build-Packages.ps1`. Verification packs the template, inspects its archive, installs it into an isolated template hive, generates `TradingSmoke`, and restores/builds the generated solution against the freshly built Base packages. Outputs are under `artifacts`; no packages are pushed.

Without `-BasePackageDirectory`, verification uses configured NuGet sources and requires the referenced framework packages to be published. Publish framework packages in `publish-manifest.csv` order, then `Base.Solution.Templates.10.0.0.nupkg` last.

## Building the template source directly

The repository-root NuGet.Config is for local development against C:\Base. Build C:\Base\build-package.bat first, then build BaseTemplate/templates/BaseTemplate/BaseTemplate.sln normally in Visual Studio or with dotnet build. Base.* packages are restored from C:\Base\artifacts\branch-nuget using a repository-local cache under artifacts/current-base-packages, so older packages with the same version in the global NuGet cache cannot replace them. Other packages come from nuget.org. Keep this stable development cache while using the solution; it can be recreated by Restore.

This repository-level configuration is outside the packaged template and is not emitted into generated projects. If Base is checked out elsewhere, update the development feed path in NuGet.Config.
