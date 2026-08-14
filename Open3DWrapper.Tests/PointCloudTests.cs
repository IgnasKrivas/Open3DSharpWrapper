using Open3DWrapper;
using Xunit;

namespace Open3DWrapper.Tests
{
    /// <summary>
    /// Headless regression suite - no Show()/DrawGeometries anywhere in this
    /// project, so it runs without a GPU/display (CI-safe). This is the
    /// suite the compat-check workflow runs against newly-released Open3D
    /// versions.
    /// </summary>
    public class PointCloudTests : IClassFixture<PointCloudFixture>
    {
        private readonly PointCloud _cloud;

        public PointCloudTests(PointCloudFixture fixture)
        {
            _cloud = fixture.Cloud;
        }

        [Fact]
        public void Load_ReportsExpectedPointCount()
        {
            Assert.Equal(PointCloudFixture.ExpectedPointCount, _cloud.Size);
            Assert.True(_cloud.HasPoints);
        }

        [Fact]
        public void Bounds_MinCenterMaxAreOrderedPerAxis()
        {
            var min = _cloud.GetMinBound();
            var max = _cloud.GetMaxBound();
            var center = _cloud.GetCenter();

            for (var axis = 0; axis < 3; axis++)
            {
                Assert.True(min[axis] <= center[axis], $"axis {axis}: min <= center");
                Assert.True(center[axis] <= max[axis], $"axis {axis}: center <= max");
            }
        }

        [Fact]
        public void Translate_RoundTripsExactly()
        {
            using var copy = _cloud.SelectByIndex(new long[] { 0 });
            var before = copy.GetX(0);

            copy.Translate(10, 0, 0);
            var afterForward = copy.GetX(0);

            copy.Translate(-10, 0, 0);
            var afterBack = copy.GetX(0);

            Assert.Equal(10, afterForward - before, precision: 9);
            Assert.Equal(before, afterBack, precision: 9);
        }

        [Theory]
        [InlineData(2.0)]
        [InlineData(5.0)]
        public void VoxelDownSample_ShrinksPointCount(double voxelSize)
        {
            using var down = _cloud.VoxelDownSample(voxelSize);
            Assert.True(down.Size > 0);
            Assert.True(down.Size < _cloud.Size);
        }

        [Fact]
        public void UniformDownSample_ApproximatesOneOverK()
        {
            const long k = 10;
            using var down = _cloud.UniformDownSample(k);
            var expected = _cloud.Size / k;
            Assert.InRange(down.Size, expected - 1, expected + 1);
        }

        [Fact]
        public void RandomDownSample_ApproximatesRatio()
        {
            using var down = _cloud.RandomDownSample(0.1);
            var expected = _cloud.Size * 0.1;
            Assert.InRange(down.Size, expected * 0.9, expected * 1.1);
        }

        [Fact]
        public void FarthestPointDownSample_ReturnsExactCount()
        {
            using var pre = _cloud.VoxelDownSample(2.0);
            using var down = pre.FarthestPointDownSample(500);
            Assert.Equal(500, down.Size);
        }

        [Fact]
        public void SelectByIndex_ReturnsExactlyRequestedCount()
        {
            using var selected = _cloud.SelectByIndex(new long[] { 0, 1, 2, 3, 4 });
            Assert.Equal(5, selected.Size);
        }

        [Fact]
        public void CropAxisAligned_ShrinksToHalfTheVolume()
        {
            var min = _cloud.GetMinBound();
            var max = _cloud.GetMaxBound();
            var halfMax = new[] { (min[0] + max[0]) / 2, max[1], max[2] };

            using var cropped = _cloud.CropAxisAligned(min, halfMax);
            Assert.True(cropped.Size > 0);
            Assert.True(cropped.Size < _cloud.Size);
        }

        [Fact]
        public void RemoveStatisticalOutliers_DropsTheDeliberateOutlier()
        {
            using var synthetic = new PointCloud();
            for (var i = 0; i < 50; i++)
            {
                synthetic.Push(i * 0.01, 0, 0);
            }
            const long outlierIndex = 50;
            synthetic.Push(1000, 1000, 1000);

            var (result, kept) = synthetic.RemoveStatisticalOutliers(nbNeighbors: 5, stdRatio: 1.0);
            using (result)
            {
                Assert.Equal(50, result.Size);
                Assert.Equal(kept.Length, result.Size);
                Assert.DoesNotContain(outlierIndex, kept);
            }
        }

        [Fact]
        public void RemoveRadiusOutliers_DropsTheDeliberateOutlier()
        {
            using var synthetic = new PointCloud();
            for (var i = 0; i < 50; i++)
            {
                synthetic.Push(i * 0.01, 0, 0);
            }
            const long outlierIndex = 50;
            synthetic.Push(1000, 1000, 1000);

            var (result, kept) = synthetic.RemoveRadiusOutliers(nbPoints: 3, searchRadius: 5.0);
            using (result)
            {
                Assert.Equal(50, result.Size);
                Assert.Equal(kept.Length, result.Size);
                Assert.DoesNotContain(outlierIndex, kept);
            }
        }

        [Fact]
        public void EstimateNormals_ProducesUnitLengthNormals()
        {
            using var small = _cloud.VoxelDownSample(5.0);
            small.EstimateNormals(knn: 20);
            small.NormalizeNormals();

            for (long i = 0; i < small.Size; i++)
            {
                var x = small.GetNormalX(i);
                var y = small.GetNormalY(i);
                var z = small.GetNormalZ(i);
                var magnitude = Math.Sqrt(x * x + y * y + z * z);
                Assert.Equal(1.0, magnitude, precision: 3);
            }
        }

        [Fact]
        public void ComputeMeanAndCovariance_MatchesHandComputedMean()
        {
            using var synthetic = new PointCloud();
            synthetic.Push(0, 0, 0);
            synthetic.Push(2, 0, 0);
            synthetic.Push(0, 2, 0);
            synthetic.Push(2, 2, 0);

            var (mean, covariance) = synthetic.ComputeMeanAndCovariance();

            Assert.Equal(1.0, mean[0], precision: 9);
            Assert.Equal(1.0, mean[1], precision: 9);
            Assert.Equal(0.0, mean[2], precision: 9);
            Assert.Equal(9, covariance.Length);
        }

        [Fact]
        public void SegmentPlane_RecoversAKnownFlatGrid()
        {
            using var flat = new PointCloud();
            var rand = new Random(42);
            for (var i = 0; i < 500; i++)
            {
                flat.Push(rand.NextDouble() * 10, rand.NextDouble() * 10, 0);
            }

            var (model, inliers) = flat.SegmentPlane(distanceThreshold: 0.01);

            // Plane normal should be ~(0, 0, +-1) and pass through z=0.
            Assert.Equal(0.0, model[0], precision: 2);
            Assert.Equal(0.0, model[1], precision: 2);
            Assert.Equal(1.0, Math.Abs(model[2]), precision: 2);
            Assert.Equal(0.0, model[3], precision: 1);
            Assert.True(inliers.Length >= 490);
        }

        [Fact]
        public void ClusterDBSCAN_FindsExactlyTwoSeparatedBlobs()
        {
            using var clusters = new PointCloud();
            var rand = new Random(7);
            for (var i = 0; i < 100; i++)
            {
                clusters.Push(rand.NextDouble() * 0.5, rand.NextDouble() * 0.5, 0);
            }
            for (var i = 0; i < 100; i++)
            {
                clusters.Push(1000 + rand.NextDouble() * 0.5, 1000 + rand.NextDouble() * 0.5, 0);
            }

            var labels = clusters.ClusterDBSCAN(eps: 1.0, minPoints: 5);
            var distinctNonNoise = labels.Where(l => l >= 0).Distinct().Count();

            Assert.Equal(2, distinctNonNoise);
        }

        [Fact]
        public void ComputeConvexHull_ReturnsNonEmptyMeshAndIndices()
        {
            using var small = _cloud.VoxelDownSample(3.0);
            var (hull, hullIndices) = small.ComputeConvexHull();
            using (hull)
            {
                Assert.True(hullIndices.Length > 0);
            }
        }

        [Fact]
        public void HiddenPointRemoval_KeepsASubsetOfPoints()
        {
            using var small = _cloud.VoxelDownSample(3.0);
            var center = small.GetCenter();
            var (mesh, visible) = small.HiddenPointRemoval(center[0], center[1], center[2] + 1000, 10000);
            using (mesh)
            {
                Assert.True(visible.Length > 0);
                Assert.True(visible.Length <= small.Size);
            }
        }

        [Fact]
        public void DetectPlanarPatches_DisposesCleanly()
        {
            using var small = _cloud.VoxelDownSample(3.0);
            var patches = small.DetectPlanarPatches();
            foreach (var patch in patches)
            {
                patch.Dispose();
            }
            // No crash on dispose is the assertion - patch count on real-world
            // scan data isn't asserted since it's environment/version-sensitive.
        }

        [Fact]
        public void WritePointCloud_RoundTripsPointCount()
        {
            using var down = _cloud.VoxelDownSample(2.0);
            var tempPath = Path.Combine(Path.GetTempPath(), $"open3dwrapper_test_{Guid.NewGuid():N}.ply");
            try
            {
                Assert.True(down.WritePointCloud(tempPath));
                using var reloaded = new PointCloud(tempPath);
                Assert.Equal(down.Size, reloaded.Size);
            }
            finally
            {
                File.Delete(tempPath);
            }
        }
    }
}
