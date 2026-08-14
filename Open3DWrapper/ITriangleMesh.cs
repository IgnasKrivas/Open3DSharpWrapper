using System;
using System.Runtime.InteropServices;

namespace Open3DWrapper
{
    internal static class ITriangleMesh
    {
        [DllImport("Open3DLibrary.dll", EntryPoint = "DeleteTriangleMesh", CharSet = CharSet.Auto)]
        public static extern void Delete(IntPtr mesh);

        [DllImport("Open3DLibrary.dll", EntryPoint = "ShowTriangleMesh", CharSet = CharSet.Auto)]
        public static extern void Show(IntPtr mesh);
    }
}
