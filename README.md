# Open3D C# Wrapper

Open3D wrapper for C#. Main reason for this to be able to manipulate Pointcloud object data directly from your C# projects. Currently Open3D is only implemented for Python and C++ languages.

## Open3D installation
In order to build Open3DLibrary project Open3D is needed to be configured for the project.
Right now project is configured to use Open3D 0.19.0 version.
- Download the Windows x64 devel packages from the [Open3D v0.19.0 release](https://github.com/isl-org/Open3D/releases/tag/v0.19.0):
  - `open3d-devel-windows-amd64-0.19.0.zip` (release)
  - `open3d-devel-windows-amd64-0.19.0-dbg.zip` (debug)
- Extract both to the C drive, so you end up with `C:\open3d-devel-windows-amd64-0.19.0` and `C:\open3d-devel-windows-amd64-0.19.0-dbg`.

In order to compile Demo project library .dll files needs to be placed in according folders.
- From `open3d-devel-windows-amd64-0.19.0-dbg\bin`, copy `Open3D.dll`, `tbb12_debug.dll` and the `resources` folder into `Open3DSharpWrapper\Demo\bin\Debug\net6.0`.

> [!NOTE]
> The Debug build of `Open3DLibrary` links against the **debug** Open3D binaries (`...-dbg`), and Release links against the **release** binaries. Don't mix them — Open3D's `ReadPointCloudOption` carries a `std::function` member, and linking a debug-CRT build of this project against a release Open3D.dll (or vice versa) silently corrupts that struct across the DLL boundary, causing `ReadPointCloud` to fail with "unknown file extension" even for a valid path.

## Open3D folder location
In case you want to change Open3D library file location you need to change Open3DLibrary project settings.
In Visual Studio include locations can be found:
- Open3DLibrary -> Properties -> C/C++ -> General -> Additional Include Directories. Locations to add:
  1. open3d-devel-windows-amd64-0.19.0\include\open3d\3rdparty
  2. open3d-devel-windows-amd64-0.19.0\include
- Open3DLibrary -> Properties -> Linker -> General -> Additional Library Directories. Locations to add:
  1. open3d-devel-windows-amd64-0.19.0-dbg\lib (Debug config) / open3d-devel-windows-amd64-0.19.0\lib (Release config)

## Known toolchain gotcha
Open3D's bundled `fmt` header uses a deprecated STL extension (`stdext::checked_array_iterator`) that newer MSVC/Windows SDK versions treat as a hard error under `/sdl`. The project defines `_SILENCE_STDEXT_ARR_ITERS_DEPRECATION_WARNING` and `_SILENCE_ALL_MS_EXT_DEPRECATION_WARNINGS` to work around this — if you hit `error C4996` referencing `stdext::checked_array_iterator`, make sure those defines are still present in the project's preprocessor definitions.

If you have multiple Visual Studio installs, make sure you build with the one that has the C++ desktop workload installed (`vswhere -latest -requires Microsoft.VisualStudio.Component.VC.Tools.x86.x64` to find it) — `PlatformToolset` is set to `v143`.
