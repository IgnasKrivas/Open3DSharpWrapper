using System;

namespace Open3DWrapper
{
    /// <summary>
    /// A triangle mesh, returned by <see cref="PointCloud.ComputeConvexHull"/>
    /// and <see cref="PointCloud.HiddenPointRemoval"/>. Only handle lifetime
    /// and visualization are exposed this phase - not vertex/triangle
    /// accessors.
    /// </summary>
    public sealed class TriangleMesh : NativeHandle
    {
        internal TriangleMesh(IntPtr handle) : base(handle)
        {
        }

        /// <summary>Displays this mesh in a viewer window.</summary>
        public void Show()
        {
            ITriangleMesh.Show(Handle);
        }

        protected override void ReleaseHandle(IntPtr handle)
        {
            ITriangleMesh.Delete(handle);
        }
    }
}
