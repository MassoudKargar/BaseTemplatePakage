[CmdletBinding()]
param([string]$BasePackageDirectory, [string]$Configuration = 'Release')
$ErrorActionPreference = 'Stop'
$root = Split-Path $PSScriptRoot -Parent
$artifacts = Join-Path $root 'artifacts'
$packages = Join-Path $artifacts 'nuget'
& dotnet pack (Join-Path $root 'BaseTemplate\BaseTemplate.csproj') -c $Configuration -o $packages -m:1 -p:UseSharedCompilation=false -p:BuildInParallel=false --nologo
if ($LASTEXITCODE) { throw 'Template pack failed.' }
[xml]$project = Get-Content (Join-Path $root 'BaseTemplate\BaseTemplate.csproj')
$version = [string]$project.Project.PropertyGroup.Version
$package = Join-Path $packages "Base.Solution.Templates.$version.nupkg"
Add-Type -AssemblyName System.IO.Compression.FileSystem
$archive = [IO.Compression.ZipFile]::OpenRead($package)
try {
    if (!@($archive.Entries | Where-Object FullName -Match 'content/templates/BaseTemplate/\.template.config/template.json$').Count) { throw 'Template metadata missing from package.' }
    if (@($archive.Entries | Where-Object FullName -Match '/(bin|obj|\.vs|\.idea)/|\.(user|suo)$').Count) { throw 'Build or IDE artifacts leaked into template package.' }
} finally { $archive.Dispose() }
$run = Join-Path $artifacts ('verify-' + [guid]::NewGuid().ToString('N'))
$hive = Join-Path $run 'hive'
$output = Join-Path $run 'TradingSmoke'
& dotnet new install $package --debug:custom-hive $hive --force
if ($LASTEXITCODE) { throw 'Template install failed.' }
& dotnet new basetemplate -n TradingSmoke -o $output --debug:custom-hive $hive
if ($LASTEXITCODE) { throw 'Template generation failed.' }
$solution = Join-Path $output 'TradingSmoke.sln'
if (!(Test-Path $solution)) { throw 'Renamed solution missing.' }
if (!(Test-Path (Join-Path $output 'src\3.Endpoints\TradingSmoke.Endpoints.API\Properties\launchSettings.json'))) { throw 'CLI launch settings missing.' }
$restoreArguments = @('restore', $solution, '--disable-parallel', '-m:1', '-p:BuildInParallel=false')
if ($BasePackageDirectory) {
    $restoreArguments += @('--configfile', (Join-Path $BasePackageDirectory 'NuGet.Config'), '--packages', (Join-Path $BasePackageDirectory 'packages'))
}
& dotnet @restoreArguments
if ($LASTEXITCODE) { throw 'Generated solution restore failed.' }
& dotnet build $solution -c $Configuration --no-restore -m:1 -p:BuildInParallel=false -p:UseSharedCompilation=false --nologo
if ($LASTEXITCODE) { throw 'Generated solution build failed.' }
Write-Host "Verified package: $package"
Write-Host "Generated solution: $solution"
