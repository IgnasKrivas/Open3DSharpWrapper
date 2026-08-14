using System;

namespace Open3DWrapper
{
    /// <summary>
    /// A bounding box oriented along an arbitrary frame of reference,
    /// returned by <see cref="PointCloud.GetOrientedBoundingBox"/> and
    /// <see cref="PointCloud.GetMinimalOrientedBoundingBox"/>. Only handle
    /// lifetime and visualization are exposed this phase - not center/
    /// extent/rotation.
    /// </summary>
    public sealed class OrientedBoundingBox : NativeHandle
    {
        internal OrientedBoundingBox(IntPtr handle) : base(handle)
        {
        }

        /// <summary>Displays this bounding box in a viewer window.</summary>
        public void Show()
        {
            IOrientedBoundingBox.Show(Handle);
        }

        protected override void ReleaseHandle(IntPtr handle)
        {
            IOrientedBoundingBox.Delete(handle);
        }
    }
}
