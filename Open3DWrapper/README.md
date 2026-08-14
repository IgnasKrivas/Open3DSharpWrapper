# Open3DSharpWrapper

A C# wrapper around [Open3D](https://www.open3d.org/)'s point cloud processing API. Open3D itself only ships Python and C++ bindings — this package exposes most of `open3d::geometry::PointCloud` as a .NET API, so you can load, transform, filter, analyze, and visualize point clouds directly from a C# project, with no separate SDK download or C++ build step.

- **Platform**: Windows x64 only. The package bundles the native Open3D runtime for `win-x64` (`Open3D.dll`, `Open3DLibrary.dll`, `tbb12.dll`, and the resource files the visualizer needs) — nothing else to install.
- **Frameworks**: `net8.0` and `net10.0`.
- **Source / issues / full docs**: [github.com/IgnasKrivas/Open3DSharpWrapper](https://github.com/IgnasKrivas/Open3DSharpWrapper)

## Install

```
dotnet add package Open3DSharpWrapper
```

That's it — the native runtime comes with the package.

## Quick start

```csharp
using Open3DWrapper;

using var cloud = new PointCloud("scan.ply");
Console.WriteLine($"{cloud.Size} points");

using var downsampled = cloud.VoxelDownSample(0.05);
downsampled.EstimateNormals();
downsampled.NormalizeNormals();

var (result, kept) = downsampled.RemoveStatisticalOutliers(nbNeighbors: 20, stdRatio: 2.0);
result.WritePointCloud("cleaned.ply");

result.Show(); // opens an Open3D viewer window
```

Every native handle (`PointCloud`, `TriangleMesh`, `OrientedBoundingBox`) implements `IDisposable` — wrap them in `using` or call `Dispose()` when you're done with them.

## What's included

`PointCloud` covers most of `open3d::geometry::PointCloud`'s public API:

- **Point data**: points/normals/colors/covariances (get/set per-index, resize, `Has*`)
- **Bounds**: min/max/center, axis-aligned and oriented bounding boxes
- **Transforms**: `Transform`, `Translate`, `Scale`, `Rotate`, `Append`
- **Downsampling & filtering**: `VoxelDownSample` (+`AndTrace`), `UniformDownSample`, `RandomDownSample`, `FarthestPointDownSample`, `SelectByIndex`, `CropAxisAligned`/`CropOriented`, `RemoveNonFinitePoints`, `RemoveDuplicatedPoints`, `RemoveRadiusOutliers`, `RemoveStatisticalOutliers`
- **Analysis**: `EstimateNormals`/`EstimateCovariances` (+ orientation variants), `ComputeMeanAndCovariance`, `ComputePointCloudDistance`, `ComputeMahalanobisDistance`, `ComputeNearestNeighborDistance`, `ComputeConvexHull`, `HiddenPointRemoval`, `ClusterDBSCAN`, `SegmentPlane`, `DetectPlanarPatches`
- **IO**: read on construction, `WritePointCloud`

Methods that hand back a different geometry type (`ComputeConvexHull`/`HiddenPointRemoval` → `TriangleMesh`, `GetOrientedBoundingBox`/`DetectPlanarPatches` → `OrientedBoundingBox`) return a disposable handle you can `Show()` or pass into other calls — their own vertex/triangle/extent accessors aren't wrapped yet.

**Not yet covered**: `CreateFromDepthImage`/`CreateFromRGBDImage`/`CreateFromVoxelGrid` (these need `Image`/`RGBDImage`/`VoxelGrid`/camera-intrinsics types that aren't wrapped yet), and full method surfaces for `TriangleMesh`/`OrientedBoundingBox` beyond handle lifetime + visualization.

## Release channels

- **Stable** (default — what `dotnet add package` gives you): published only when a maintainer manually cuts a [GitHub Release](https://github.com/IgnasKrivas/Open3DSharpWrapper/releases). Bundles whichever Open3D version is the officially-supported one at that time.
- **Preview** (`X.Y.Z-preview.<open3d-version>`, e.g. `1.2.0-preview.0.19.0`): published automatically whenever the project's weekly compatibility check successfully validates a *new* Open3D release, ahead of it becoming the official stable target. Being a prerelease, it's never resolved by a plain `dotnet add package` — opt in explicitly if you want to try a newer Open3D version early:
  ```
  dotnet add package Open3DSharpWrapper --prerelease
  ```
  or pin an exact preview version.

## License & acknowledgments
MIT licensed. This package is a thin wrapper — all the actual point cloud processing is [Open3D](https://www.open3d.org/) ([isl-org/Open3D](https://github.com/isl-org/Open3D), also MIT) doing the work.
