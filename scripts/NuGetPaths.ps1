# Shared path/configuration handling only; this file does not invoke dotnet.
function Resolve-TemplateNuGetPaths {
    param(
        [Parameter(Mandatory=$true)][string]$Root,
        [string]$BasePackageDirectory,
        [string]$PackagesDirectory
    )
    if (!$BasePackageDirectory) { $BasePackageDirectory = $env:BASE_PACKAGE_FEED }
    if (!$BasePackageDirectory -or $BasePackageDirectory.Contains('%')) {
        throw 'Set BASE_PACKAGE_FEED or supply -BasePackageDirectory with a verified local package feed.'
    }
    $feed = [IO.Path]::GetFullPath($BasePackageDirectory).TrimEnd([char[]]@('\', '/'))
    if (!(Test-Path -LiteralPath $feed -PathType Container)) {
        throw "Base package feed directory does not exist: $feed"
    }
    if (!$PackagesDirectory) { $PackagesDirectory = Join-Path $Root 'artifacts\current-base-packages' }
    $cache = [IO.Path]::GetFullPath($PackagesDirectory).TrimEnd([char[]]@('\', '/'))
    $comparison = [StringComparison]::OrdinalIgnoreCase
    $separator = [IO.Path]::DirectorySeparatorChar
    if ($cache.Equals($feed, $comparison) -or
        $cache.StartsWith($feed + $separator, $comparison) -or
        $feed.StartsWith($cache + $separator, $comparison)) {
        throw 'Package feed and package cache must be separate, non-overlapping directories.'
    }
    if (Test-Path -LiteralPath $cache -PathType Leaf) { throw 'Package cache path must be a directory.' }
    # Refuse junction/symlink aliases that could bypass the overlap check.
    foreach ($path in @($feed, $cache)) {
        $cursor = $path
        while ($cursor) {
            if (Test-Path -LiteralPath $cursor) {
                $item = Get-Item -LiteralPath $cursor -Force
                if ($item.Attributes -band [IO.FileAttributes]::ReparsePoint) {
                    throw 'Feed/cache paths through reparse points are not supported.'
                }
            }
            $parent = [IO.Path]::GetDirectoryName($cursor)
            if ($parent -eq $cursor) { break }
            $cursor = $parent
        }
    }
    $config = Join-Path $Root 'NuGet.Config'
    if (!(Test-Path -LiteralPath $config -PathType Leaf)) { throw 'Repository NuGet.Config is missing.' }
    [pscustomobject]@{ FeedDirectory = $feed; PackagesDirectory = $cache; ConfigFile = $config }
}

function Invoke-WithTemplateNuGetFeed {
    param(
        [Parameter(Mandatory=$true)]$Paths,
        [Parameter(Mandatory=$true)][scriptblock]$Action
    )
    $previousFeed = $env:BASE_PACKAGE_FEED
    try {
        $env:BASE_PACKAGE_FEED = $Paths.FeedDirectory
        & $Action
    } finally {
        $env:BASE_PACKAGE_FEED = $previousFeed
    }
}
