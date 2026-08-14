using System;

namespace Open3DWrapper
{
    /// <summary>
    /// Base class for types that own a native Open3D object via an opaque
    /// pointer. Implements the standard Dispose(bool) pattern so the native
    /// object is freed deterministically via Dispose(), with the finalizer
    /// as a safety net if the caller forgets.
    /// </summary>
    public abstract class NativeHandle : IDisposable
    {
        private bool _disposed;

        protected NativeHandle(IntPtr handle)
        {
            Handle = handle;
        }

        // protected internal (not just protected): sibling wrapper types in
        // this assembly (e.g. PointCloud passing an OrientedBoundingBox's
        // handle to a native Crop call) need each other's handles, and plain
        // `protected` only permits access through an inheritance relationship
        // - PointCloud and OrientedBoundingBox share a base but aren't related
        // to each other, so `protected` alone wouldn't compile there.
        protected internal IntPtr Handle { get; private set; }

        /// <summary>Frees the native object referenced by <paramref name="handle"/>.</summary>
        protected abstract void ReleaseHandle(IntPtr handle);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (Handle != IntPtr.Zero)
            {
                ReleaseHandle(Handle);
                Handle = IntPtr.Zero;
            }

            _disposed = true;
        }

        ~NativeHandle()
        {
            Dispose(false);
        }
    }
}
