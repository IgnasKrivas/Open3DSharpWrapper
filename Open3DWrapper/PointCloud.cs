using System;

namespace Open3DWrapper
{
    public class PointCloud : NativeHandle
    {
        ///@brief Constructor without parameters
        ///@details Initialize an empty point cloud object
        public PointCloud() : base(IPointCloud.CreatePointCloud())
        {
        }

        ///@brief Constructor with parameters
        ///@details Pass pointcloud file path and load pointcloud object into pointer
        ///@param Pointcloud file path
        public PointCloud(string path) : base(IPointCloud.LoadPcFile(path))
        {
        }

        ///@brief Wraps an already-existing native PointCloud handle (e.g. one
        ///returned by a downsampling/filtering call), without creating a new one.
        internal PointCloud(IntPtr handle) : base(handle)
        {
        }

        ///@brief Size
        ///@details Get the size attribute
        public int Size => IPointCloud.CountPointCloud(Handle);

        //There may be errors after destructuring, but it is currently running fine. If there is a problem check pointer value
        public IntPtr PointCloudXyzPointer => Handle;

        protected override void ReleaseHandle(IntPtr handle)
        {
            IPointCloud.DeletePointCloud(handle);
        }

        #region Points

        ///@brief X value
        ///@param Get X index number
        public double GetX(int index) => IPointCloud.GetX(Handle, index);

        ///@brief Y value
        ///@param Get Y index number
        public double GetY(int index) => IPointCloud.GetY(Handle, index);

        ///@brief Z value
        ///@param Get Z index number
        public double GetZ(int index) => IPointCloud.GetZ(Handle, index);

        ///@brief X value
        ///@param Set X index number
        ///@param index - index, x - value
        public void SetX(int index, double x) => IPointCloud.SetX(Handle, index, x);

        ///@brief Y value
        ///@param Set Y index number
        ///@param index - index, y - value
        public void SetY(int index, double y) => IPointCloud.SetY(Handle, index, y);

        ///@brief Z value
        ///@param Set Z index number
        ///@param index - index, z - value
        public void SetZ(int index, double z) => IPointCloud.SetZ(Handle, index, z);

        ///@brief Resize
        ///@param Resize pointcloud size
        public void Resize(int size) => IPointCloud.Resize(Handle, size);

        ///@brief Inserts a point to the end of pointcloud object
        ///@param x value of the pushed point
        ///@param y value of the pushed point
        ///@param z value of the pushed point
        public void Push(double x, double y, double z) => IPointCloud.Push(Handle, x, y, z);

        ///@brief Pop up a point, similar to popping the stack, the last point in the element is popped up
        public void Pop() => IPointCloud.Pop(Handle);

        ///@brief Clear all points in the point cloud object
        public void Clear() => IPointCloud.Clear(Handle);

        public void Show() => IPointCloud.Show(Handle);

        #endregion

        #region Has

        ///@brief Returns true if the point cloud contains points
        public bool HasPoints => IPointCloud.HasPoints(Handle) != 0;

        ///@brief Returns true if the point cloud contains point normals
        public bool HasNormals => IPointCloud.HasNormals(Handle) != 0;

        ///@brief Returns true if the point cloud contains point colors
        public bool HasColors => IPointCloud.HasColors(Handle) != 0;

        ///@brief Returns true if the point cloud contains per-point covariance matrices
        public bool HasCovariances => IPointCloud.HasCovariances(Handle) != 0;

        #endregion

        #region Normals

        ///@brief Get the normal X component at the given index
        public double GetNormalX(long index) => IPointCloud.GetNormalX(Handle, index);

        ///@brief Get the normal Y component at the given index
        public double GetNormalY(long index) => IPointCloud.GetNormalY(Handle, index);

        ///@brief Get the normal Z component at the given index
        public double GetNormalZ(long index) => IPointCloud.GetNormalZ(Handle, index);

        ///@brief Set the normal X component at the given index
        public void SetNormalX(long index, double x) => IPointCloud.SetNormalX(Handle, index, x);

        ///@brief Set the normal Y component at the given index
        public void SetNormalY(long index, double y) => IPointCloud.SetNormalY(Handle, index, y);

        ///@brief Set the normal Z component at the given index
        public void SetNormalZ(long index, double z) => IPointCloud.SetNormalZ(Handle, index, z);

        ///@brief Resize the normals array
        public void ResizeNormals(long size) => IPointCloud.ResizeNormals(Handle, size);

        ///@brief Normalize point normals to length 1
        public void NormalizeNormals() => IPointCloud.NormalizeNormals(Handle);

        #endregion

        #region Colors

        ///@brief Get the red channel at the given index
        public double GetColorR(long index) => IPointCloud.GetColorR(Handle, index);

        ///@brief Get the green channel at the given index
        public double GetColorG(long index) => IPointCloud.GetColorG(Handle, index);

        ///@brief Get the blue channel at the given index
        public double GetColorB(long index) => IPointCloud.GetColorB(Handle, index);

        ///@brief Set the red channel at the given index
        public void SetColorR(long index, double r) => IPointCloud.SetColorR(Handle, index, r);

        ///@brief Set the green channel at the given index
        public void SetColorG(long index, double g) => IPointCloud.SetColorG(Handle, index, g);

        ///@brief Set the blue channel at the given index
        public void SetColorB(long index, double b) => IPointCloud.SetColorB(Handle, index, b);

        ///@brief Resize the colors array
        public void ResizeColors(long size) => IPointCloud.ResizeColors(Handle, size);

        ///@brief Assigns each point in the point cloud the same RGB color
        public void PaintUniformColor(double r, double g, double b) =>
            IPointCloud.PaintUniformColor(Handle, new[] { r, g, b });

        #endregion

        #region Covariances

        ///@brief Get the 3x3 covariance matrix at the given index, row-major
        public double[] GetCovariance(long index)
        {
            var result = new double[9];
            IPointCloud.GetCovariance(Handle, index, result);
            return result;
        }

        ///@brief Set the 3x3 covariance matrix at the given index, row-major
        public void SetCovariance(long index, double[] covariance3X3)
        {
            if (covariance3X3 == null || covariance3X3.Length != 9)
            {
                throw new ArgumentException("Covariance must be a 9-element row-major 3x3 matrix.", nameof(covariance3X3));
            }

            IPointCloud.SetCovariance(Handle, index, covariance3X3);
        }

        ///@brief Resize the covariances array
        public void ResizeCovariances(long size) => IPointCloud.ResizeCovariances(Handle, size);

        #endregion

        #region Bounds

        ///@brief Minimum bound of the point cloud as (x, y, z)
        public double[] GetMinBound()
        {
            var result = new double[3];
            IPointCloud.GetMinBound(Handle, result);
            return result;
        }

        ///@brief Maximum bound of the point cloud as (x, y, z)
        public double[] GetMaxBound()
        {
            var result = new double[3];
            IPointCloud.GetMaxBound(Handle, result);
            return result;
        }

        ///@brief Center of the point cloud as (x, y, z)
        public double[] GetCenter()
        {
            var result = new double[3];
            IPointCloud.GetCenter(Handle, result);
            return result;
        }

        ///@brief Axis-aligned bounding box as (min[3], max[3])
        public (double[] Min, double[] Max) GetAxisAlignedBoundingBox()
        {
            var min = new double[3];
            var max = new double[3];
            IPointCloud.GetAxisAlignedBoundingBox(Handle, min, max);
            return (min, max);
        }

        ///@brief Oriented bounding box enclosing the point cloud
        public OrientedBoundingBox GetOrientedBoundingBox(bool robust = false) =>
            new OrientedBoundingBox(IPointCloud.GetOrientedBoundingBox(Handle, robust ? 1 : 0));

        ///@brief Minimal-volume oriented bounding box enclosing the point cloud
        public OrientedBoundingBox GetMinimalOrientedBoundingBox(bool robust = false) =>
            new OrientedBoundingBox(IPointCloud.GetMinimalOrientedBoundingBox(Handle, robust ? 1 : 0));

        #endregion

        #region Transform

        ///@brief Applies a 4x4 transformation matrix (row-major, 16 elements) to the point cloud
        public void Transform(double[] matrix4X4)
        {
            if (matrix4X4 == null || matrix4X4.Length != 16)
            {
                throw new ArgumentException("Transformation matrix must have 16 elements (row-major 4x4).", nameof(matrix4X4));
            }

            IPointCloud.Transform(Handle, matrix4X4);
        }

        ///@brief Translates the point cloud by the given vector
        ///@param relative If true, translation is added to the current position; if false, the cloud is moved so its center equals the translation
        public void Translate(double x, double y, double z, bool relative = true) =>
            IPointCloud.Translate(Handle, new[] { x, y, z }, relative ? 1 : 0);

        ///@brief Scales the point cloud about the given center
        public void Scale(double scale, double centerX, double centerY, double centerZ) =>
            IPointCloud.Scale(Handle, scale, new[] { centerX, centerY, centerZ });

        ///@brief Rotates the point cloud using a 3x3 rotation matrix (row-major, 9 elements) about the given center
        public void Rotate(double[] rotation3X3, double centerX, double centerY, double centerZ)
        {
            if (rotation3X3 == null || rotation3X3.Length != 9)
            {
                throw new ArgumentException("Rotation matrix must have 9 elements (row-major 3x3).", nameof(rotation3X3));
            }

            IPointCloud.Rotate(Handle, rotation3X3, new[] { centerX, centerY, centerZ });
        }

        ///@brief Appends another point cloud's points/normals/colors/covariances onto this one
        public void Append(PointCloud other) => IPointCloud.Append(Handle, other.Handle);

        #endregion

        #region Downsampling, filtering, cropping

        ///@brief Returns a new point cloud containing only the given point indices
        ///@param invert If true, selects every index NOT in indices instead
        public PointCloud SelectByIndex(long[] indices, bool invert = false)
        {
            indices ??= Array.Empty<long>();
            return new PointCloud(IPointCloud.SelectByIndex(Handle, indices, indices.Length, invert ? 1 : 0));
        }

        ///@brief Downsamples with a voxel grid; normals/colors/covariances are averaged if present
        public PointCloud VoxelDownSample(double voxelSize) =>
            new PointCloud(IPointCloud.VoxelDownSample(Handle, voxelSize));

        ///@brief Downsamples with a voxel grid and also returns, for each output voxel, the
        ///indices of the input points that were merged into it (indices[i] are the source
        ///point indices for output point i), plus the raw voxel-index matrix Open3D produces.
        public (PointCloud Result, int[,] VoxelMatrix, int[][] SourceIndices) VoxelDownSampleAndTrace(
            double voxelSize, double[] minBound3, double[] maxBound3, bool approximateClass = false)
        {
            if (minBound3 == null || minBound3.Length != 3) throw new ArgumentException("minBound3 must have 3 elements.", nameof(minBound3));
            if (maxBound3 == null || maxBound3.Length != 3) throw new ArgumentException("maxBound3 must have 3 elements.", nameof(maxBound3));

            var resultHandle = IPointCloud.VoxelDownSampleAndTrace(
                Handle, voxelSize, minBound3, maxBound3, approximateClass ? 1 : 0,
                out var matrixPtr, out var rows, out var cols,
                out var valuesPtr, out var offsetsPtr, out var outerCount);

            var flatMatrix = NativeArrayMarshal.CopyAndFreeInt32s(matrixPtr, (long)rows * cols);
            var matrix = new int[rows, cols];
            for (var r = 0; r < rows; r++)
            {
                for (var c = 0; c < cols; c++)
                {
                    matrix[r, c] = flatMatrix[r * cols + c];
                }
            }

            var offsets = NativeArrayMarshal.CopyAndFreeInt32s(offsetsPtr, outerCount + 1);
            var totalValues = outerCount > 0 ? offsets[outerCount] : 0;
            var values = NativeArrayMarshal.CopyAndFreeInt32s(valuesPtr, totalValues);

            var sourceIndices = new int[outerCount][];
            for (var i = 0; i < outerCount; i++)
            {
                var start = offsets[i];
                var length = offsets[i + 1] - start;
                sourceIndices[i] = new int[length];
                Array.Copy(values, start, sourceIndices[i], 0, length);
            }

            return (new PointCloud(resultHandle), matrix, sourceIndices);
        }

        ///@brief Downsamples by taking every k-th point, starting from index 0
        public PointCloud UniformDownSample(long everyKPoints) =>
            new PointCloud(IPointCloud.UniformDownSample(Handle, everyKPoints));

        ///@brief Downsamples by randomly selecting the given ratio of points
        public PointCloud RandomDownSample(double samplingRatio) =>
            new PointCloud(IPointCloud.RandomDownSample(Handle, samplingRatio));

        ///@brief Downsamples by iteratively selecting the farthest point from those already selected
        public PointCloud FarthestPointDownSample(long numSamples, long startIndex = 0) =>
            new PointCloud(IPointCloud.FarthestPointDownSample(Handle, numSamples, startIndex));

        ///@brief Crops to the points within an axis-aligned box
        public PointCloud CropAxisAligned(double[] min3, double[] max3, bool invert = false)
        {
            if (min3 == null || min3.Length != 3) throw new ArgumentException("min3 must have 3 elements.", nameof(min3));
            if (max3 == null || max3.Length != 3) throw new ArgumentException("max3 must have 3 elements.", nameof(max3));
            return new PointCloud(IPointCloud.CropAxisAligned(Handle, min3, max3, invert ? 1 : 0));
        }

        ///@brief Crops to the points within an oriented bounding box
        public PointCloud CropOriented(OrientedBoundingBox box, bool invert = false) =>
            new PointCloud(IPointCloud.CropOriented(Handle, box.Handle, invert ? 1 : 0));

        ///@brief Removes points with a NaN and/or infinite coordinate, in place
        public void RemoveNonFinitePoints(bool removeNan = true, bool removeInfinite = true) =>
            IPointCloud.RemoveNonFinitePoints(Handle, removeNan ? 1 : 0, removeInfinite ? 1 : 0);

        ///@brief Removes points with identical coordinates, in place
        public void RemoveDuplicatedPoints() => IPointCloud.RemoveDuplicatedPoints(Handle);

        ///@brief Removes points with fewer than nbPoints neighbors within searchRadius
        ///@returns The filtered cloud, plus the indices (into the original cloud) of the points that were kept
        public (PointCloud Result, long[] KeptIndices) RemoveRadiusOutliers(
            long nbPoints, double searchRadius, bool printProgress = false)
        {
            var resultHandle = IPointCloud.RemoveRadiusOutliers(
                Handle, nbPoints, searchRadius, printProgress ? 1 : 0, out var keptPtr, out var count);
            return (new PointCloud(resultHandle), NativeArrayMarshal.CopyAndFreeInt64s(keptPtr, count));
        }

        ///@brief Removes points further than stdRatio standard deviations from their nbNeighbors average distance
        ///@returns The filtered cloud, plus the indices (into the original cloud) of the points that were kept
        public (PointCloud Result, long[] KeptIndices) RemoveStatisticalOutliers(
            long nbNeighbors, double stdRatio, bool printProgress = false)
        {
            var resultHandle = IPointCloud.RemoveStatisticalOutliers(
                Handle, nbNeighbors, stdRatio, printProgress ? 1 : 0, out var keptPtr, out var count);
            return (new PointCloud(resultHandle), NativeArrayMarshal.CopyAndFreeInt64s(keptPtr, count));
        }

        #endregion

        #region Analysis

        ///@brief Computes, for each point in this cloud, the distance to the nearest point in target
        public double[] ComputePointCloudDistance(PointCloud target)
        {
            IPointCloud.ComputePointCloudDistance(Handle, target.Handle, out var ptr, out var count);
            return NativeArrayMarshal.CopyAndFreeDoubles(ptr, count);
        }

        ///@brief Computes the per-point covariance matrix of input without modifying it; each returned
        ///matrix is 9 elements, row-major 3x3, at offset index*9
        public static double[] EstimatePerPointCovariances(
            PointCloud input, KDTreeSearchParamType searchType = KDTreeSearchParamType.Knn,
            long knn = 30, double radius = 0, long maxNn = 0)
        {
            IPointCloud.EstimatePerPointCovariances(input.Handle, (int)searchType, knn, radius, maxNn, out var ptr, out var count);
            return NativeArrayMarshal.CopyAndFreeDoubles(ptr, (long)count * 9);
        }

        ///@brief Computes the covariance matrix for each point of the point cloud, in place
        public void EstimateCovariances(
            KDTreeSearchParamType searchType = KDTreeSearchParamType.Knn,
            long knn = 30, double radius = 0, long maxNn = 0) =>
            IPointCloud.EstimateCovariances(Handle, (int)searchType, knn, radius, maxNn);

        ///@brief Computes the mean and 3x3 covariance matrix (row-major, 9 elements) of the point cloud
        public (double[] Mean, double[] Covariance) ComputeMeanAndCovariance()
        {
            var mean = new double[3];
            var cov = new double[9];
            IPointCloud.ComputeMeanAndCovariance(Handle, mean, cov);
            return (mean, cov);
        }

        ///@brief Computes the Mahalanobis distance for each point relative to the point cloud's own mean/covariance
        public double[] ComputeMahalanobisDistance()
        {
            IPointCloud.ComputeMahalanobisDistance(Handle, out var ptr, out var count);
            return NativeArrayMarshal.CopyAndFreeDoubles(ptr, count);
        }

        ///@brief Computes, for each point, the distance to its nearest neighbor in this same point cloud
        public double[] ComputeNearestNeighborDistance()
        {
            IPointCloud.ComputeNearestNeighborDistance(Handle, out var ptr, out var count);
            return NativeArrayMarshal.CopyAndFreeDoubles(ptr, count);
        }

        ///@brief Estimates point normals in place
        public void EstimateNormals(
            KDTreeSearchParamType searchType = KDTreeSearchParamType.Knn,
            long knn = 30, double radius = 0, long maxNn = 0, bool fastNormalComputation = true) =>
            IPointCloud.EstimateNormals(Handle, (int)searchType, knn, radius, maxNn, fastNormalComputation ? 1 : 0);

        ///@brief Orients normals to align with the given reference direction
        public void OrientNormalsToAlignWithDirection(double x = 0, double y = 0, double z = 1) =>
            IPointCloud.OrientNormalsToAlignWithDirection(Handle, new[] { x, y, z });

        ///@brief Orients normals to point towards the given camera location
        public void OrientNormalsTowardsCameraLocation(double x = 0, double y = 0, double z = 0) =>
            IPointCloud.OrientNormalsTowardsCameraLocation(Handle, new[] { x, y, z });

        ///@brief Orients normals using consistent tangent planes (Hoppe et al.)
        public void OrientNormalsConsistentTangentPlane(long k, double lambda = 0.0, double cosAlphaTol = 1.0) =>
            IPointCloud.OrientNormalsConsistentTangentPlane(Handle, k, lambda, cosAlphaTol);

        ///@brief Computes the convex hull of the point cloud
        ///@returns The hull as a triangle mesh, plus the indices of the input points that are part of the hull
        public (TriangleMesh Hull, long[] HullPointIndices) ComputeConvexHull(bool joggleInputs = false)
        {
            var meshHandle = IPointCloud.ComputeConvexHull(Handle, joggleInputs ? 1 : 0, out var ptr, out var count);
            return (new TriangleMesh(meshHandle), NativeArrayMarshal.CopyAndFreeInt64s(ptr, count));
        }

        ///@brief Removes points not visible from cameraLocation (Katz et al. hidden point removal)
        ///@returns A triangle mesh of the visible-region hull, plus the indices of the points that remained visible
        public (TriangleMesh VisibilityMesh, long[] VisibleIndices) HiddenPointRemoval(double cameraX, double cameraY, double cameraZ, double radius)
        {
            var meshHandle = IPointCloud.HiddenPointRemoval(Handle, new[] { cameraX, cameraY, cameraZ }, radius, out var ptr, out var count);
            return (new TriangleMesh(meshHandle), NativeArrayMarshal.CopyAndFreeInt64s(ptr, count));
        }

        ///@brief Clusters the point cloud using DBSCAN; returns one label per point, -1 indicates noise
        public int[] ClusterDBSCAN(double eps, long minPoints, bool printProgress = false)
        {
            IPointCloud.ClusterDBSCAN(Handle, eps, minPoints, printProgress ? 1 : 0, out var ptr, out var count);
            return NativeArrayMarshal.CopyAndFreeInt32s(ptr, count);
        }

        ///@brief Segments the largest plane in the point cloud using RANSAC
        ///@returns The plane model (a, b, c, d) for ax+by+cz+d=0, plus the indices of the inlier points
        public (double[] PlaneModel, long[] InlierIndices) SegmentPlane(
            double distanceThreshold = 0.01, int ransacN = 3, int numIterations = 100, double probability = 0.99999999)
        {
            var model = new double[4];
            IPointCloud.SegmentPlane(Handle, distanceThreshold, ransacN, numIterations, probability, model, out var ptr, out var count);
            return (model, NativeArrayMarshal.CopyAndFreeInt64s(ptr, count));
        }

        ///@brief Detects planar patches, returned as oriented bounding boxes (the box's Z axis is the patch normal)
        public OrientedBoundingBox[] DetectPlanarPatches(
            double normalVarianceThresholdDeg = 60, double coplanarityDeg = 75, double outlierRatio = 0.75,
            double minPlaneEdgeLength = 0.0, long minNumPoints = 0,
            KDTreeSearchParamType searchType = KDTreeSearchParamType.Knn, long knn = 30, double radius = 0, long maxNn = 0)
        {
            IPointCloud.DetectPlanarPatches(
                Handle, normalVarianceThresholdDeg, coplanarityDeg, outlierRatio, minPlaneEdgeLength, minNumPoints,
                (int)searchType, knn, radius, maxNn, out var handlesPtr, out var count);

            if (handlesPtr == IntPtr.Zero || count == 0)
            {
                if (handlesPtr != IntPtr.Zero)
                {
                    IPointCloud.FreeHandleArray(handlesPtr);
                }

                return Array.Empty<OrientedBoundingBox>();
            }

            var result = new OrientedBoundingBox[count];
            for (var i = 0; i < count; i++)
            {
                var handle = System.Runtime.InteropServices.Marshal.ReadIntPtr(handlesPtr, i * IntPtr.Size);
                result[i] = new OrientedBoundingBox(handle);
            }

            IPointCloud.FreeHandleArray(handlesPtr);
            return result;
        }

        #endregion

        #region IO

        ///@brief Writes this point cloud to a file; the format is inferred from the file extension
        ///@returns true if the write succeeded
        public bool WritePointCloud(string path, bool writeAscii = false, bool compressed = false, bool printProgress = false) =>
            IPointCloud.WritePointCloud(Handle, path, writeAscii ? 1 : 0, compressed ? 1 : 0, printProgress ? 1 : 0) != 0;

        #endregion
    }
}
