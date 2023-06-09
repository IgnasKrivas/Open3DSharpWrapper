# Open3D C# Wrapper

Open3D wrapper for C#. Main reason for this to be able to manipulate Pointcloud object data directly from your C# projects. Currently Open3D is only implemented for Python and C++ languages.

## Open3D installation
In order to build Open3DLibrary project Open3D is needed to be configured for the project.
Right now project is configured to use Open3D 0.15.1 version.
- Download source code from [Open3D](http://www.open3d.org/)
- Extract files "open3d-devel-windows-amd64" to C drive.

In order to compile Demo project library .dll files needs to be placed in according folders.
- Open3D.dll from "open3d-devel-windows-amd64/bin" folder to "Open3DSharpWrapper\Demo\bin\Debug\net6.0"

## Open3D folder location
In case you want to change Open3D library file location you need to change Open3DLibrary project settings.
In Visual Studio include locations can be found:
- Open3DLibrary -> Properties -> C/C++ -> General -> Additional Include Directories. Locations to add:
  1. open3d-devel-windows-amd64\include\open3d\3rdparty
  2. open3d-devel-windows-amd64\include
- Open3DLibrary -> Properties -> Linker -> General -> Additional Library Directories. Locations to add:
  1. open3d-devel-windows-amd64\lib
