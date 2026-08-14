using System;
using System.Runtime.InteropServices;

namespace Open3DWrapper
{
    internal static class IOrientedBoundingBox
    {
        [DllImport("Open3DLibrary.dll", EntryPoint = "DeleteOrientedBoundingBox", CharSet = CharSet.Auto)]
        public static extern void Delete(IntPtr obb);

        [DllImport("Open3DLibrary.dll", EntryPoint = "ShowOrientedBoundingBox", CharSet = CharSet.Auto)]
        public static extern void Show(IntPtr obb);
    }
}
