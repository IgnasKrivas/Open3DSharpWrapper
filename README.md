# Open3D C# Wrapper

[![CI](https://github.com/IgnasKrivas/Open3DSharpWrapper/actions/workflows/ci.yml/badge.svg)](https://github.com/IgnasKrivas/Open3DSharpWrapper/actions/workflows/ci.yml)
[![Open3D compatibility check](https://github.com/IgnasKrivas/Open3DSharpWrapper/actions/workflows/open3d-compat-check.yml/badge.svg)](https://github.com/IgnasKrivas/Open3DSharpWrapper/actions/workflows/open3d-compat-check.yml)
[![NuGet](https://img.shields.io/nuget/v/Open3DSharpWrapper.svg)](https://www.nuget.org/packages/Open3DSharpWrapper)

A C# wrapper around [Open3D](https://www.open3d.org/)'s point cloud processing API. Open3D itself only ships Python and C++ bindings — this project exposes a large chunk of `open3d::geometry::PointCloud` as a P/Invoke-friendly C# API, so you can load, process, and inspect point clouds directly from a C#/.NET project.

## Getting started

**Using it in your own project**: `dotnet add package Open3DSharpWrapper`. The package bundles the Open3D win-x64 native runtime (`Open3DLibrary.dll`, `Open3D.dll`, `tbb12.dll`, and the resource files the viewer needs), so there's nothing else to install.

> [!NOTE]
> Prerelease versions (`-preview.<open3d-version>`, auto-published weekly against whatever Open3D release is currently latest — see [Testing](#testing) below) are never resolved by a plain `dotnet add package`; you'd need `--prerelease` or an exact version to get one. The default, stable version is only published when a [GitHub Release](https://github.com/IgnasKrivas/Open3DSharpWrapper/releases) is manually cut.

**Working on the wrapper itself**: no manual setup needed. Clone the repo, open `Open3DSharpWrapper.sln` in Visual Studio (or run `msbuild`/`dotnet build`), and build — the first build automatically downloads the Open3D 0.19.0 Windows devel SDK (both release and debug variants) into a repo-local `third_party\` folder and copies the runtime DLLs next to the built executable. Subsequent builds skip the download since it's already present.

Project is currently configured to use Open3D 0.19.0. To use a different version, override the `Open3DVersion` MSBuild property (e.g. `msbuild /p:Open3DVersion=0.20.0`) — it must match a real tag at the [Open3D releases page](https://github.com/isl-org/Open3D/releases).

### Manual / offline install
If you're offline or want to pre-fetch the SDK yourself, run:
```powershell
tools\setup-open3d.ps1 -Root third_party -Version 0.19.0
```
It's idempotent — safe to re-run, and skips any variant that's already present.

> [!NOTE]
> The Debug build of `Open3DLibrary` links against the **debug** Open3D binaries, and Release links against the **release** binaries — this is handled automatically by `Open3DLibrary\Open3DSdk.props`/`.targets`, don't hand-edit the include/library paths to point both configs at the same variant. Open3D's `ReadPointCloudOption` carries a `std::function` member, and linking a debug-CRT build of this project against a release Open3D.dll (or vice versa) silently corrupts that struct across the DLL boundary, causing `ReadPointCloud` to fail with "unknown file extension" even for a valid path.

## Usage

```csharp
using Open3DWrapper;

using var cloud = new PointCloud("scan.ply");
Console.WriteLine(cloud.Size);

using var downsampled = cloud.VoxelDownSample(0.05);
downsampled.EstimateNormals();
downsampled.NormalizeNormals();

var (result, kept) = downsampled.RemoveStatisticalOutliers(nbNeighbors: 20, stdRatio: 2.0);
result.WritePointCloud("cleaned.ply");

result.Show(); // opens an Open3D viewer window
```

## What's covered

`PointCloud` exposes most of `open3d::geometry::PointCloud`'s public API:

- **Point data**: points/normals/colors/covariances (get/set per-index, resize, `Has*`)
- **Bounds**: min/max/center, axis-aligned and oriented bounding boxes
- **Transforms**: `Transform`, `Translate`, `Scale`, `Rotate`, `Append`
- **Downsampling & filtering**: `VoxelDownSample` (+`AndTrace`), `UniformDownSample`, `RandomDownSample`, `FarthestPointDownSample`, `SelectByIndex`, `CropAxisAligned`/`CropOriented`, `RemoveNonFinitePoints`, `RemoveDuplicatedPoints`, `RemoveRadiusOutliers`, `RemoveStatisticalOutliers`
- **Analysis**: `EstimateNormals`/`EstimateCovariances` (+ orientation variants), `ComputeMeanAndCovariance`, `ComputePointCloudDistance`, `ComputeMahalanobisDistance`, `ComputeNearestNeighborDistance`, `ComputeConvexHull`, `HiddenPointRemoval`, `ClusterDBSCAN`, `SegmentPlane`, `DetectPlanarPatches`
- **IO**: read on construction, `WritePointCloud`

Methods that hand back a different geometry type (`ComputeConvexHull`/`HiddenPointRemoval` → `TriangleMesh`, `GetOrientedBoundingBox`/`DetectPlanarPatches` → `OrientedBoundingBox`) return a disposable handle you can `Show()` or pass back into other calls — their own vertex/triangle/extent accessors aren't wrapped yet.

**Not yet covered**: `CreateFromDepthImage`/`CreateFromRGBDImage`/`CreateFromVoxelGrid` (these take an `Image`/`RGBDImage`/`VoxelGrid`/camera-intrinsics as *input*, and none of those types are wrapped yet, so there's nothing to construct one from on the C# side), and full method surfaces for `TriangleMesh`/`OrientedBoundingBox` beyond handle lifetime + visualization.

## Testing
`Open3DWrapper.Tests` is a headless xUnit suite (no `Show()`/viewer windows, so it runs without a GPU) covering the API above against both the bundled sample scan and small hand-built synthetic point clouds:
```powershell
dotnet test Open3DWrapper.Tests
```
CI runs this suite on every push/PR, and a second, separately-scheduled workflow re-runs it weekly against whatever Open3D release is *currently* latest — independent of the version pinned above — specifically to catch a new Open3D release breaking this wrapper before anyone hits it manually. When that weekly check passes against a new Open3D version, it also publishes a prerelease NuGet package for it automatically (see [Getting started](#getting-started)) — the stable package is unaffected and still only moves on a manual release.

## Known toolchain gotcha
Open3D's bundled `fmt` header uses a deprecated STL extension (`stdext::checked_array_iterator`) that newer MSVC/Windows SDK versions treat as a hard error under `/sdl`. The project defines `_SILENCE_STDEXT_ARR_ITERS_DEPRECATION_WARNING` and `_SILENCE_ALL_MS_EXT_DEPRECATION_WARNINGS` to work around this — if you hit `error C4996` referencing `stdext::checked_array_iterator`, make sure those defines are still present in the project's preprocessor definitions.

If you have multiple Visual Studio installs, make sure you build with the one that has the C++ desktop workload installed (`vswhere -latest -requires Microsoft.VisualStudio.Component.VC.Tools.x86.x64` to find it) — `PlatformToolset` is set to `v143`.

## Acknowledgments
This project is a thin C# wrapper and wouldn't exist without [Open3D](https://www.open3d.org/) ([isl-org/Open3D](https://github.com/isl-org/Open3D)) — an excellent, actively-maintained open-source library for 3D data processing, released under the [MIT license](https://github.com/isl-org/Open3D/blob/main/LICENSE). All of the actual point cloud processing here is Open3D doing the work; this repo just makes it callable from C#.
