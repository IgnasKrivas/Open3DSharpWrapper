using System;
using System.Runtime.InteropServices;

namespace Open3DWrapper
{
    /// <summary>
    /// Copies native heap-allocated arrays (returned as a pointer + count by
    /// convention across this wrapper) into managed arrays, then frees the
    /// native buffer via the matching Free*Array export.
    /// </summary>
    internal static class NativeArrayMarshal
    {
        public static double[] CopyAndFreeDoubles(IntPtr nativeArray, long count)
        {
            if (nativeArray == IntPtr.Zero)
            {
                return Array.Empty<double>();
            }

            var result = count > 0 ? new double[count] : Array.Empty<double>();
            if (count > 0)
            {
                Marshal.Copy(nativeArray, result, 0, checked((int)count));
            }

            IPointCloud.FreeDoubleArray(nativeArray);
            return result;
        }

        public static long[] CopyAndFreeInt64s(IntPtr nativeArray, long count)
        {
            if (nativeArray == IntPtr.Zero)
            {
                return Array.Empty<long>();
            }

            var result = count > 0 ? new long[count] : Array.Empty<long>();
            if (count > 0)
            {
                Marshal.Copy(nativeArray, result, 0, checked((int)count));
            }

            IPointCloud.FreeInt64Array(nativeArray);
            return result;
        }

        public static int[] CopyAndFreeInt32s(IntPtr nativeArray, long count)
        {
            if (nativeArray == IntPtr.Zero)
            {
                return Array.Empty<int>();
            }

            var result = count > 0 ? new int[count] : Array.Empty<int>();
            if (count > 0)
            {
                Marshal.Copy(nativeArray, result, 0, checked((int)count));
            }

            IPointCloud.FreeInt32Array(nativeArray);
            return result;
        }
    }
}
