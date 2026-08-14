using Open3DWrapper;

namespace Open3DWrapper.Tests
{
    /// <summary>
    /// Loads high_def0.ply once and shares it across the whole test class -
    /// it's a 1.9M-point real scan, reloading it per-test would make the
    /// suite unnecessarily slow.
    /// </summary>
    public sealed class PointCloudFixture : IDisposable
    {
        public const int ExpectedPointCount = 1926681;

        public PointCloudFixture()
        {
            var path = Path.Combine(AppContext.BaseDirectory, "high_def0.ply");
            Cloud = new PointCloud(path);
        }

        public PointCloud Cloud { get; }

        public void Dispose()
        {
            Cloud.Dispose();
        }
    }
}
