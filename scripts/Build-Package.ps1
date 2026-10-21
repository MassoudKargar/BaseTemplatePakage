[CmdletBinding()]
param(
    [string]$Configuration = 'Release',
    [string]$OutputDirectory,
    [switch]$Verify,
    [string]$BasePackageDirectory,
    [string]$PackagesDirectory
)
$ErrorActionPreference = 'Stop'
$root = Split-Path $PSScriptRoot -Parent
if (!$OutputDirectory) { $OutputDirectory = Join-Path $root 'artifacts\nuget' }
$output = if ([IO.Path]::IsPathRooted($OutputDirectory)) { [IO.Path]::GetFullPath($OutputDirectory) } else { [IO.Path]::GetFullPath((Join-Path $root $OutputDirectory)) }
if ($Verify -or $BasePackageDirectory) {
    & (Join-Path $PSScriptRoot 'Verify-Template.ps1') -Configuration $Configuration -OutputDirectory $output -BasePackageDirectory $BasePackageDirectory -PackagesDirectory $PackagesDirectory
    return
}
. (Join-Path $PSScriptRoot 'NuGetPaths.ps1')
$paths = Resolve-TemplateNuGetPaths -Root $root -BasePackageDirectory $BasePackageDirectory -PackagesDirectory $PackagesDirectory
Invoke-WithTemplateNuGetFeed -Paths $paths -Action {
& dotnet pack (Join-Path $root 'BaseTemplate\BaseTemplate.csproj') -c $Configuration -o $output -m:1 -p:UseSharedCompilation=false -p:BuildInParallel=false "-p:RestoreConfigFile=$($paths.ConfigFile)" "-p:RestorePackagesPath=$($paths.PackagesDirectory)" --nologo
if ($LASTEXITCODE) { throw 'Template packaging failed.' }
}
Write-Host "Template packages: $output"
Write-Host 'Run with -Verify -BasePackageDirectory <Base package directory> to test a generated solution.'
