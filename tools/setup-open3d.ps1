<#
.SYNOPSIS
  Downloads and unpacks the Open3D Windows devel SDK (release + debug) into a
  repo-local folder, so Open3DLibrary.vcxproj never needs hardcoded C:\ paths.

.PARAMETER Root
  Destination folder to hold the extracted SDKs (e.g. <repo>\third_party).

.PARAMETER Version
  Open3D release version to fetch (matches a tag at
  https://github.com/isl-org/Open3D/releases).

.PARAMETER Force
  Re-download and re-extract even if the target already looks present.
#>
param(
    [Parameter(Mandatory = $true)]
    [string]$Root,

    [string]$Version = "0.19.0",

    [switch]$Force
)

$ErrorActionPreference = "Stop"
$ProgressPreference = "SilentlyContinue"

function Install-Open3DVariant {
    param(
        [string]$Url,
        [string]$DestName
    )

    $destPath = Join-Path $Root $DestName
    $libMarker = Join-Path $destPath "lib\Open3D.lib"

    if ((Test-Path $libMarker) -and -not $Force) {
        Write-Host "[setup-open3d] $DestName already present, skipping."
        return
    }

    Write-Host "[setup-open3d] Fetching $DestName ..."
    New-Item -ItemType Directory -Force -Path $Root | Out-Null

    $zipPath = Join-Path ([System.IO.Path]::GetTempPath()) "$DestName.zip"
    Invoke-WebRequest -Uri $Url -OutFile $zipPath -UseBasicParsing

    $stagingDir = Join-Path ([System.IO.Path]::GetTempPath()) "open3d-staging-$DestName"
    if (Test-Path $stagingDir) { Remove-Item $stagingDir -Recurse -Force }
    Expand-Archive -Path $zipPath -DestinationPath $stagingDir -Force

    # Don't assume the zip's internal top-level folder name matches $DestName -
    # locate include\open3d\Open3D.h and derive the real SDK root from it.
    $marker = Get-ChildItem -Path $stagingDir -Recurse -Filter "Open3D.h" -ErrorAction SilentlyContinue |
        Where-Object { $_.FullName -match '\\include\\open3d\\Open3D\.h$' } |
        Select-Object -First 1

    if (-not $marker) {
        throw "[setup-open3d] Could not find include\open3d\Open3D.h inside the downloaded package for $DestName - Open3D's package layout may have changed."
    }

    $sourceRoot = Split-Path (Split-Path (Split-Path $marker.FullName -Parent) -Parent) -Parent

    if (Test-Path $destPath) { Remove-Item $destPath -Recurse -Force }
    Move-Item -Path $sourceRoot -Destination $destPath

    Remove-Item $zipPath -Force -ErrorAction SilentlyContinue
    Remove-Item $stagingDir -Recurse -Force -ErrorAction SilentlyContinue

    $requiredPaths = @(
        (Join-Path $destPath "lib\Open3D.lib"),
        (Join-Path $destPath "bin\Open3D.dll"),
        (Join-Path $destPath "bin\resources")
    )
    foreach ($p in $requiredPaths) {
        if (-not (Test-Path $p)) {
            throw "[setup-open3d] Verification failed after extracting $DestName - expected path missing: $p"
        }
    }

    Write-Host "[setup-open3d] $DestName ready at $destPath"
}

$releaseUrl = "https://github.com/isl-org/Open3D/releases/download/v$Version/open3d-devel-windows-amd64-$Version.zip"
$debugUrl = "https://github.com/isl-org/Open3D/releases/download/v$Version/open3d-devel-windows-amd64-$Version-dbg.zip"

Install-Open3DVariant -Url $releaseUrl -DestName "open3d-devel-windows-amd64-$Version"
Install-Open3DVariant -Url $debugUrl -DestName "open3d-devel-windows-amd64-$Version-dbg"
