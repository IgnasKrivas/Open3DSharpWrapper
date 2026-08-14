using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Open3DWrapper
{
    public class IPointCloud
    {
        //Return an empty pointcloud object pointer
        [DllImport("Open3DLibrary.dll", EntryPoint = "CreatePointCloud", CharSet = CharSet.Auto)]
        public static extern IntPtr CreatePointCloud();

        //Load pointcloud from the file and return the pointcloud object pointer
        [DllImport("Open3DLibrary.dll", EntryPoint = "loadPcFile", CharSet = CharSet.Auto)]
        public static extern IntPtr LoadPcFile([MarshalAs(UnmanagedType.LPStr)] string path);

        //Delete pointcloud pointer
        [DllImport("Open3DLibrary.dll", EntryPoint = "DeletePointCloud", CharSet = CharSet.Auto)]
        public static extern void DeletePointCloud(IntPtr PointCloudPointer);

        //Return pointcloud size
        [DllImport("Open3DLibrary.dll", EntryPoint = "CountPointCloud", CharSet = CharSet.Auto)]
        public static extern int CountPointCloud(IntPtr PointCloudPointer);

        //Return x coordinate of pointcloud index
        [DllImport("Open3DLibrary.dll", EntryPoint = "GetX", CharSet = CharSet.Auto)]
        public static extern double GetX(IntPtr PointCloudPointer, int index);

        //Return y coordinate of pointcloud index
        [DllImport("Open3DLibrary.dll", EntryPoint = "GetY", CharSet = CharSet.Auto)]
        public static extern double GetY(IntPtr PointCloudPointer, int index);

        //Return z coordinate of pointcloud index
        [DllImport("Open3DLibrary.dll", EntryPoint = "GetZ", CharSet = CharSet.Auto)]
        public static extern double GetZ(IntPtr PointCloudPointer, int index);

        //Set x coordinate of pointcloud index
        [DllImport("Open3DLibrary.dll", EntryPoint = "SetX", CharSet = CharSet.Auto)]
        public static extern void SetX(IntPtr PointCloudPointer, int index, double x);

        //Set y coordinate of pointcloud index
        [DllImport("Open3DLibrary.dll", EntryPoint = "SetY", CharSet = CharSet.Auto)]
        public static extern void SetY(IntPtr PointCloudPointer, int index, double y);

        //Return z coordinate of pointcloud index
        [DllImport("Open3DLibrary.dll", EntryPoint = "SetZ", CharSet = CharSet.Auto)]
        public static extern void SetZ(IntPtr PointCloudPointer, int index, double z);

        //Change pointcloud size
        [DllImport("Open3DLibrary.dll", EntryPoint = "Resize", CharSet = CharSet.Auto)]
        public static extern void Resize(IntPtr PointCloudPointer, int size);

        //Add value to pointcloud
        [DllImport("Open3DLibrary.dll", EntryPoint = "Push", CharSet = CharSet.Auto)]
        public static extern void Push(IntPtr PointCloudPointer, double x, double y, double z);

        //Removes last element from pointcloud
        [DllImport("Open3DLibrary.dll", EntryPoint = "Pop", CharSet = CharSet.Auto)]
        public static extern void Pop(IntPtr PointCloudPointer);

        //Clears all values in the pointcloud
        [DllImport("Open3DLibrary.dll", EntryPoint = "Clear", CharSet = CharSet.Auto)]
        public static extern void Clear(IntPtr PointCloudPointer);

        //Displays pointcloud in a window
        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(Show), CharSet = CharSet.Auto)]
        public static extern void Show(IntPtr PointCloudPointer);

        #region Has

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(HasPoints), CharSet = CharSet.Auto)]
        public static extern int HasPoints(IntPtr pc);

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(HasNormals), CharSet = CharSet.Auto)]
        public static extern int HasNormals(IntPtr pc);

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(HasColors), CharSet = CharSet.Auto)]
        public static extern int HasColors(IntPtr pc);

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(HasCovariances), CharSet = CharSet.Auto)]
        public static extern int HasCovariances(IntPtr pc);

        #endregion

        #region Normals

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(GetNormalX), CharSet = CharSet.Auto)]
        public static extern double GetNormalX(IntPtr pc, long index);

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(GetNormalY), CharSet = CharSet.Auto)]
        public static extern double GetNormalY(IntPtr pc, long index);

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(GetNormalZ), CharSet = CharSet.Auto)]
        public static extern double GetNormalZ(IntPtr pc, long index);

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(SetNormalX), CharSet = CharSet.Auto)]
        public static extern void SetNormalX(IntPtr pc, long index, double x);

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(SetNormalY), CharSet = CharSet.Auto)]
        public static extern void SetNormalY(IntPtr pc, long index, double y);

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(SetNormalZ), CharSet = CharSet.Auto)]
        public static extern void SetNormalZ(IntPtr pc, long index, double z);

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(ResizeNormals), CharSet = CharSet.Auto)]
        public static extern void ResizeNormals(IntPtr pc, long size);

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(NormalizeNormals), CharSet = CharSet.Auto)]
        public static extern void NormalizeNormals(IntPtr pc);

        #endregion

        #region Colors

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(GetColorR), CharSet = CharSet.Auto)]
        public static extern double GetColorR(IntPtr pc, long index);

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(GetColorG), CharSet = CharSet.Auto)]
        public static extern double GetColorG(IntPtr pc, long index);

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(GetColorB), CharSet = CharSet.Auto)]
        public static extern double GetColorB(IntPtr pc, long index);

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(SetColorR), CharSet = CharSet.Auto)]
        public static extern void SetColorR(IntPtr pc, long index, double r);

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(SetColorG), CharSet = CharSet.Auto)]
        public static extern void SetColorG(IntPtr pc, long index, double g);

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(SetColorB), CharSet = CharSet.Auto)]
        public static extern void SetColorB(IntPtr pc, long index, double b);

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(ResizeColors), CharSet = CharSet.Auto)]
        public static extern void ResizeColors(IntPtr pc, long size);

        // color is a caller-allocated double[3] (r, g, b)
        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(PaintUniformColor), CharSet = CharSet.Auto)]
        public static extern void PaintUniformColor(IntPtr pc, [In] double[] color);

        #endregion

        #region Covariances

        // out9/in9 are caller-allocated double[9], row-major 3x3
        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(GetCovariance), CharSet = CharSet.Auto)]
        public static extern void GetCovariance(IntPtr pc, long index, [Out] double[] out9);

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(SetCovariance), CharSet = CharSet.Auto)]
        public static extern void SetCovariance(IntPtr pc, long index, [In] double[] in9);

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(ResizeCovariances), CharSet = CharSet.Auto)]
        public static extern void ResizeCovariances(IntPtr pc, long size);

        #endregion

        #region Bounds

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(GetMinBound), CharSet = CharSet.Auto)]
        public static extern void GetMinBound(IntPtr pc, [Out] double[] out3);

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(GetMaxBound), CharSet = CharSet.Auto)]
        public static extern void GetMaxBound(IntPtr pc, [Out] double[] out3);

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(GetCenter), CharSet = CharSet.Auto)]
        public static extern void GetCenter(IntPtr pc, [Out] double[] out3);

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(GetAxisAlignedBoundingBox), CharSet = CharSet.Auto)]
        public static extern void GetAxisAlignedBoundingBox(IntPtr pc, [Out] double[] outMin3, [Out] double[] outMax3);

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(GetOrientedBoundingBox), CharSet = CharSet.Auto)]
        public static extern IntPtr GetOrientedBoundingBox(IntPtr pc, int robust);

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(GetMinimalOrientedBoundingBox), CharSet = CharSet.Auto)]
        public static extern IntPtr GetMinimalOrientedBoundingBox(IntPtr pc, int robust);

        #endregion

        #region Transform

        // matrix16 is a caller-allocated double[16], row-major 4x4
        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(Transform), CharSet = CharSet.Auto)]
        public static extern void Transform(IntPtr pc, [In] double[] matrix16);

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(Translate), CharSet = CharSet.Auto)]
        public static extern void Translate(IntPtr pc, [In] double[] translation3, int relative);

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(Scale), CharSet = CharSet.Auto)]
        public static extern void Scale(IntPtr pc, double scale, [In] double[] center3);

        // rotation9 is a caller-allocated double[9], row-major 3x3
        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(Rotate), CharSet = CharSet.Auto)]
        public static extern void Rotate(IntPtr pc, [In] double[] rotation9, [In] double[] center3);

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(Append), CharSet = CharSet.Auto)]
        public static extern void Append(IntPtr pc, IntPtr other);

        #endregion

        #region Array memory management

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(FreeDoubleArray), CharSet = CharSet.Auto)]
        public static extern void FreeDoubleArray(IntPtr arr);

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(FreeInt64Array), CharSet = CharSet.Auto)]
        public static extern void FreeInt64Array(IntPtr arr);

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(FreeInt32Array), CharSet = CharSet.Auto)]
        public static extern void FreeInt32Array(IntPtr arr);

        #endregion

        #region Downsampling, filtering, cropping

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(SelectByIndex), CharSet = CharSet.Auto)]
        public static extern IntPtr SelectByIndex(IntPtr pc, [In] long[] indices, long count, int invert);

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(VoxelDownSample), CharSet = CharSet.Auto)]
        public static extern IntPtr VoxelDownSample(IntPtr pc, double voxelSize);

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(VoxelDownSampleAndTrace), CharSet = CharSet.Auto)]
        public static extern IntPtr VoxelDownSampleAndTrace(
            IntPtr pc, double voxelSize, [In] double[] minBound3, [In] double[] maxBound3, int approximateClass,
            out IntPtr outMatrix, out int outMatrixRows, out int outMatrixCols,
            out IntPtr outIndicesValues, out IntPtr outIndicesOffsets, out int outIndicesOuterCount);

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(UniformDownSample), CharSet = CharSet.Auto)]
        public static extern IntPtr UniformDownSample(IntPtr pc, long everyKPoints);

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(RandomDownSample), CharSet = CharSet.Auto)]
        public static extern IntPtr RandomDownSample(IntPtr pc, double samplingRatio);

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(FarthestPointDownSample), CharSet = CharSet.Auto)]
        public static extern IntPtr FarthestPointDownSample(IntPtr pc, long numSamples, long startIndex);

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(CropAxisAligned), CharSet = CharSet.Auto)]
        public static extern IntPtr CropAxisAligned(IntPtr pc, [In] double[] min3, [In] double[] max3, int invert);

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(CropOriented), CharSet = CharSet.Auto)]
        public static extern IntPtr CropOriented(IntPtr pc, IntPtr obb, int invert);

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(RemoveNonFinitePoints), CharSet = CharSet.Auto)]
        public static extern void RemoveNonFinitePoints(IntPtr pc, int removeNan, int removeInfinite);

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(RemoveDuplicatedPoints), CharSet = CharSet.Auto)]
        public static extern void RemoveDuplicatedPoints(IntPtr pc);

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(RemoveRadiusOutliers), CharSet = CharSet.Auto)]
        public static extern IntPtr RemoveRadiusOutliers(IntPtr pc, long nbPoints, double searchRadius, int printProgress, out IntPtr outKept, out int outCount);

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(RemoveStatisticalOutliers), CharSet = CharSet.Auto)]
        public static extern IntPtr RemoveStatisticalOutliers(IntPtr pc, long nbNeighbors, double stdRatio, int printProgress, out IntPtr outKept, out int outCount);

        #endregion

        #region Handle array memory management

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(FreeHandleArray), CharSet = CharSet.Auto)]
        public static extern void FreeHandleArray(IntPtr arr);

        #endregion

        #region Analysis

        // searchType: 0 = KNN (uses knn), 1 = Radius (uses radius), 2 = Hybrid (uses radius + maxNn)

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(ComputePointCloudDistance), CharSet = CharSet.Auto)]
        public static extern void ComputePointCloudDistance(IntPtr pc, IntPtr target, out IntPtr outDistances, out int outCount);

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(EstimatePerPointCovariances), CharSet = CharSet.Auto)]
        public static extern void EstimatePerPointCovariances(IntPtr pc, int searchType, long knn, double radius, long maxNn, out IntPtr outCovariances, out int outCount);

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(EstimateCovariances), CharSet = CharSet.Auto)]
        public static extern void EstimateCovariances(IntPtr pc, int searchType, long knn, double radius, long maxNn);

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(ComputeMeanAndCovariance), CharSet = CharSet.Auto)]
        public static extern void ComputeMeanAndCovariance(IntPtr pc, [Out] double[] outMean3, [Out] double[] outCov9);

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(ComputeMahalanobisDistance), CharSet = CharSet.Auto)]
        public static extern void ComputeMahalanobisDistance(IntPtr pc, out IntPtr outDistances, out int outCount);

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(ComputeNearestNeighborDistance), CharSet = CharSet.Auto)]
        public static extern void ComputeNearestNeighborDistance(IntPtr pc, out IntPtr outDistances, out int outCount);

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(EstimateNormals), CharSet = CharSet.Auto)]
        public static extern void EstimateNormals(IntPtr pc, int searchType, long knn, double radius, long maxNn, int fastNormalComputation);

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(OrientNormalsToAlignWithDirection), CharSet = CharSet.Auto)]
        public static extern void OrientNormalsToAlignWithDirection(IntPtr pc, [In] double[] direction3);

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(OrientNormalsTowardsCameraLocation), CharSet = CharSet.Auto)]
        public static extern void OrientNormalsTowardsCameraLocation(IntPtr pc, [In] double[] cameraLocation3);

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(OrientNormalsConsistentTangentPlane), CharSet = CharSet.Auto)]
        public static extern void OrientNormalsConsistentTangentPlane(IntPtr pc, long k, double lambda, double cosAlphaTol);

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(ComputeConvexHull), CharSet = CharSet.Auto)]
        public static extern IntPtr ComputeConvexHull(IntPtr pc, int joggleInputs, out IntPtr outIndices, out int outCount);

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(HiddenPointRemoval), CharSet = CharSet.Auto)]
        public static extern IntPtr HiddenPointRemoval(IntPtr pc, [In] double[] cameraLocation3, double radius, out IntPtr outIndices, out int outCount);

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(ClusterDBSCAN), CharSet = CharSet.Auto)]
        public static extern void ClusterDBSCAN(IntPtr pc, double eps, long minPoints, int printProgress, out IntPtr outLabels, out int outCount);

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(SegmentPlane), CharSet = CharSet.Auto)]
        public static extern void SegmentPlane(IntPtr pc, double distanceThreshold, int ransacN, int numIterations, double probability, [Out] double[] outPlaneModel4, out IntPtr outInliers, out int outCount);

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(DetectPlanarPatches), CharSet = CharSet.Auto)]
        public static extern void DetectPlanarPatches(
            IntPtr pc, double normalVarianceThresholdDeg, double coplanarityDeg, double outlierRatio, double minPlaneEdgeLength, long minNumPoints,
            int searchType, long knn, double radius, long maxNn,
            out IntPtr outHandles, out int outCount);

        #endregion

        #region IO

        [DllImport("Open3DLibrary.dll", EntryPoint = nameof(WritePointCloud), CharSet = CharSet.Auto)]
        public static extern int WritePointCloud(IntPtr pc, [MarshalAs(UnmanagedType.LPStr)] string path, int writeAscii, int compressed, int printProgress);

        #endregion
    }
}