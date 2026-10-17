# BaseTemplatePakage

Build and verify the .NET 10 `Base.Solution.Templates` NuGet template:

```powershell
.\scripts\Verify-Template.ps1 -BasePackageDirectory C:\Base\artifacts\nuget
```

First run `C:\Base\scripts\Build-Packages.ps1`. Verification packs the template, inspects its archive, installs it into an isolated template hive, generates `TradingSmoke`, and restores/builds the generated solution against the freshly built Base packages. Outputs are under `artifacts`; no packages are pushed.

Without `-BasePackageDirectory`, verification uses configured NuGet sources and requires the referenced framework packages to be published. Publish framework packages in `publish-manifest.csv` order, then `Base.Solution.Templates.10.0.0.nupkg` last.
