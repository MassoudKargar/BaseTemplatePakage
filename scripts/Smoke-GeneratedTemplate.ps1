[CmdletBinding()]
param([Parameter(Mandatory=$true)][string]$ProjectDirectory, [Parameter(Mandatory=$true)][string]$BasePackageDirectory, [string]$PackagesDirectory)
$ErrorActionPreference = 'Stop'
$root = Split-Path $PSScriptRoot -Parent
. (Join-Path $PSScriptRoot 'NuGetPaths.ps1')
$paths = Resolve-TemplateNuGetPaths -Root $root -BasePackageDirectory $BasePackageDirectory -PackagesDirectory $PackagesDirectory
$fixture = Join-Path $root ('artifacts\startup-' + [guid]::NewGuid().ToString('N'))
New-Item -ItemType Directory -Force $fixture | Out-Null
$apiDirectory = Join-Path $ProjectDirectory 'src\3.Endpoints\TradingSmoke.Endpoints.API'
$apiProject = [Security.SecurityElement]::Escape((Join-Path $apiDirectory 'TradingSmoke.Endpoints.API.csproj'))
@"
<Project Sdk="Microsoft.NET.Sdk.Web">
<PropertyGroup><TargetFramework>net10.0</TargetFramework><ImplicitUsings>enable</ImplicitUsings><Nullable>enable</Nullable><IsPackable>false</IsPackable></PropertyGroup>
<ItemGroup><ProjectReference Include="$apiProject" /></ItemGroup>
</Project>
"@ | Set-Content (Join-Path $fixture 'TemplateStartup.csproj') -Encoding utf8
Copy-Item (Join-Path $PSScriptRoot 'TemplateSmoke.cs') (Join-Path $fixture 'Program.cs')
Invoke-WithTemplateNuGetFeed -Paths $paths -Action {
& dotnet restore (Join-Path $fixture 'TemplateStartup.csproj') --configfile $paths.ConfigFile --packages $paths.PackagesDirectory --disable-parallel -m:1
if ($LASTEXITCODE) { throw 'Template startup fixture restore failed.' }
& dotnet run --project (Join-Path $fixture 'TemplateStartup.csproj') --no-restore -p:UseSharedCompilation=false -p:BuildInParallel=false -- $apiDirectory
if ($LASTEXITCODE) { throw 'Generated API startup/HTTP smoke failed.' }
}
