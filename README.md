# BaseTemplatePakage

The repository already targets .NET 10. It packages the canonical source under BaseTemplate/templates; the package does not bundle the Base framework binaries.

## Explicit local feed and separate cache

Use a locally supplied, verified Base package feed. Set BASE_PACKAGE_FEED before direct dotnet operations, or pass -BasePackageDirectory to the packaging/verification scripts. NuGet.Config maps Base.* exclusively to that feed; other dependencies use nuget.org. An unresolved/missing feed is an error, not a public-feed fallback.

Configurable example (replace the placeholder with your verified local feed path; verify its manifest and package hashes before use):

```powershell
$env:BASE_PACKAGE_FEED = 'C:\path\to\verified-base-feed'
```

The supplied feed's own NuGet.Config may contain paths from the machine that generated it. The scripts use this repository's NuGet.Config instead. They do not edit or generate packages in the feed.

The default writable package cache is artifacts/current-base-packages under this repository. Use -PackagesDirectory to select another cache. Equal, nested, ancestor-overlapping, and reparse-point feed/cache paths are rejected. This validation does not create directories. Feed selection is scoped to the script invocation and restores the previous BASE_PACKAGE_FEED value.

Use a disposable source workspace when running the commands below: packaging and verification still write obj/bin, artifacts, generated projects and custom template hives. Explicit package output and cache options do not relocate every script output. No execution is implied by these examples.

## Package and verify (requires separate execution approval in the staged workflow)

Build only the template package:

```powershell
.\build-package.bat
```

Verify a generated application against the supplied Base packages, with an explicit cache:

```powershell
.\build-package.bat -Verify -BasePackageDirectory $env:BASE_PACKAGE_FEED -PackagesDirectory 'C:\path\to\isolated-package-cache'
```

Supplying -BasePackageDirectory directly retains the existing behavior of selecting verification. Setting only BASE_PACKAGE_FEED does not turn a plain package command into verification. -OutputDirectory continues to control package output; relative supplied paths retain their existing current-working-directory interpretation.

Verification packs the template, inspects its archive, installs it into an isolated custom hive, generates TradingSmoke, and restores/builds the generated solution. The optional publish-manifest.csv check validates referenced Base IDs/versions; it is not a replacement for offline SHA-256/provenance checks. Startup/DI/HTTP smoke remains a separate script and expects TradingSmoke naming.

To run that separate script later:

```powershell
.\scripts\Smoke-GeneratedTemplate.ps1 -ProjectDirectory 'C:\path\to\TradingSmoke' -BasePackageDirectory $env:BASE_PACKAGE_FEED -PackagesDirectory 'C:\path\to\isolated-package-cache'
```

All script restore/implicit-pack restore operations use the repository config and selected cache, never <feed>/packages. No packages are pushed or generated in the Base feed by these scripts. Pack itself still restores/builds as needed, so do not run it during a static-only phase.

## Building template source directly

BaseTemplate/templates/BaseTemplate/BaseTemplate.sln contains the eight application projects. The outer BaseTemplatePakage.sln contains only the packager, so packaging success does not establish application compilation.

For a later approved direct source build, supply BASE_PACKAGE_FEED and the required dependencies through this repository's configuration. Its cache is separate from the feed. Generated projects do not include this development NuGet.Config and need their own reviewed feed/config/cache settings.

Do not regenerate or modify a protected Base checkout as an implicit setup step. Missing packages or unresolved transitive dependencies are dependency blockers; do not invent replacement packages or upgrade versions automatically. Historical results in README-VALIDATION.md are not current execution evidence.
