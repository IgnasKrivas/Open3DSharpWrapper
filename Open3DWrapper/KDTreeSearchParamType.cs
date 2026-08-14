namespace Open3DWrapper
{
    /// <summary>
    /// Selects which Open3D KDTreeSearchParam subtype to build for neighbor
    /// search (normal/covariance estimation, planar patch detection). There
    /// is no C# wrapper for KDTreeSearchParam itself - the native side builds
    /// the right subtype from this tag plus the numeric parameters.
    /// </summary>
    public enum KDTreeSearchParamType
    {
        /// <summary>Fixed number of nearest neighbors (uses knn).</summary>
        Knn = 0,

        /// <summary>All neighbors within a radius (uses radius).</summary>
        Radius = 1,

        /// <summary>Up to maxNn neighbors within a radius (uses radius + maxNn).</summary>
        Hybrid = 2,
    }
}
