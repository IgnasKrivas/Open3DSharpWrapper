#pragma region Includes
#include "open3d/Open3D.h"
#include <cstdint>
using namespace std;
#pragma endregion

#pragma region Defines
#define EXTERNC extern "C"

#define HEAD EXTERNC __declspec(dllexport)

#define CallingConvention __stdcall
#pragma endregion

#pragma region PointCloud controller
HEAD open3d::geometry::PointCloud* CallingConvention CreatePointCloud()
{
	return new open3d::geometry::PointCloud;
}

HEAD open3d::geometry::PointCloud* CallingConvention loadPcFile(char* path)
{
	open3d::geometry::PointCloud* cloud = new open3d::geometry::PointCloud;

	if (!open3d::io::ReadPointCloud(path, *cloud))
	{
		cloud->points_.push_back(Eigen::Vector3d(0, 0, 0));
		return cloud;
	}
	else
	{
		return cloud;
	}
}

HEAD void CallingConvention DeletePointCloud(open3d::geometry::PointCloud* pc)
{
	delete pc;
}
HEAD int CallingConvention CountPointCloud(open3d::geometry::PointCloud* pc)
{
	return pc->points_.size();
}
HEAD double CallingConvention GetX(open3d::geometry::PointCloud* pc, int index)
{
	return pc->points_[index].x();
}
HEAD double CallingConvention GetY(open3d::geometry::PointCloud* pc, int index)
{
	return  pc->points_[index].y();
}
HEAD double CallingConvention GetZ(open3d::geometry::PointCloud* pc, int index)
{
	return  pc->points_[index].z();
}
HEAD void CallingConvention SetX(open3d::geometry::PointCloud* pc, int index, double x)
{
	pc->points_[index].x() = x;
}
HEAD void CallingConvention SetY(open3d::geometry::PointCloud* pc, int index, double y)
{
	pc->points_[index].y() = y;
}
HEAD void CallingConvention SetZ(open3d::geometry::PointCloud* pc, int index, double z)
{
	pc->points_[index].z() = z;
}
HEAD void CallingConvention Resize(open3d::geometry::PointCloud* pc, int size)
{
	pc->points_.resize(size);
}
HEAD void CallingConvention Push(open3d::geometry::PointCloud* pc, double x, double y, double z)
{
	pc->points_.push_back(Eigen::Vector3d(x, y, z));
}
HEAD void CallingConvention Pop(open3d::geometry::PointCloud* pc)
{
	pc->points_.pop_back();
}

HEAD void CallingConvention Clear(open3d::geometry::PointCloud* pc)
{
	pc->Clear();
}

HEAD void CallingConvention Show(open3d::geometry::PointCloud* pc)
{
	// A no-op deleter is required here: DrawGeometries only accepts
	// shared_ptr, but pc is owned by the caller (freed via DeletePointCloud).
	// A shared_ptr constructed without one would delete pc when this function
	// returns, leaving the caller holding a dangling pointer.
	std::vector<std::shared_ptr<const open3d::geometry::Geometry>> geometries;
	std::shared_ptr<const open3d::geometry::Geometry> pcPtr(pc, [](const open3d::geometry::Geometry*) {});

	geometries.push_back(pcPtr);
	open3d::visualization::DrawGeometries(geometries);
}
#pragma endregion

#pragma region Has
HEAD int CallingConvention HasPoints(open3d::geometry::PointCloud* pc)
{
	return pc->HasPoints() ? 1 : 0;
}
HEAD int CallingConvention HasNormals(open3d::geometry::PointCloud* pc)
{
	return pc->HasNormals() ? 1 : 0;
}
HEAD int CallingConvention HasColors(open3d::geometry::PointCloud* pc)
{
	return pc->HasColors() ? 1 : 0;
}
HEAD int CallingConvention HasCovariances(open3d::geometry::PointCloud* pc)
{
	return pc->HasCovariances() ? 1 : 0;
}
#pragma endregion

#pragma region Normals
HEAD double CallingConvention GetNormalX(open3d::geometry::PointCloud* pc, int64_t index)
{
	return pc->normals_[index].x();
}
HEAD double CallingConvention GetNormalY(open3d::geometry::PointCloud* pc, int64_t index)
{
	return pc->normals_[index].y();
}
HEAD double CallingConvention GetNormalZ(open3d::geometry::PointCloud* pc, int64_t index)
{
	return pc->normals_[index].z();
}
HEAD void CallingConvention SetNormalX(open3d::geometry::PointCloud* pc, int64_t index, double x)
{
	pc->normals_[index].x() = x;
}
HEAD void CallingConvention SetNormalY(open3d::geometry::PointCloud* pc, int64_t index, double y)
{
	pc->normals_[index].y() = y;
}
HEAD void CallingConvention SetNormalZ(open3d::geometry::PointCloud* pc, int64_t index, double z)
{
	pc->normals_[index].z() = z;
}
HEAD void CallingConvention ResizeNormals(open3d::geometry::PointCloud* pc, int64_t size)
{
	pc->normals_.resize(size);
}
HEAD void CallingConvention NormalizeNormals(open3d::geometry::PointCloud* pc)
{
	pc->NormalizeNormals();
}
#pragma endregion

#pragma region Colors
HEAD double CallingConvention GetColorR(open3d::geometry::PointCloud* pc, int64_t index)
{
	return pc->colors_[index].x();
}
HEAD double CallingConvention GetColorG(open3d::geometry::PointCloud* pc, int64_t index)
{
	return pc->colors_[index].y();
}
HEAD double CallingConvention GetColorB(open3d::geometry::PointCloud* pc, int64_t index)
{
	return pc->colors_[index].z();
}
HEAD void CallingConvention SetColorR(open3d::geometry::PointCloud* pc, int64_t index, double r)
{
	pc->colors_[index].x() = r;
}
HEAD void CallingConvention SetColorG(open3d::geometry::PointCloud* pc, int64_t index, double g)
{
	pc->colors_[index].y() = g;
}
HEAD void CallingConvention SetColorB(open3d::geometry::PointCloud* pc, int64_t index, double b)
{
	pc->colors_[index].z() = b;
}
HEAD void CallingConvention ResizeColors(open3d::geometry::PointCloud* pc, int64_t size)
{
	pc->colors_.resize(size);
}
HEAD void CallingConvention PaintUniformColor(open3d::geometry::PointCloud* pc, double* color)
{
	pc->PaintUniformColor(Eigen::Vector3d(color[0], color[1], color[2]));
}
#pragma endregion

#pragma region Covariances
HEAD void CallingConvention GetCovariance(open3d::geometry::PointCloud* pc, int64_t index, double* out9)
{
	const Eigen::Matrix3d& m = pc->covariances_[index];
	for (int r = 0; r < 3; r++)
		for (int c = 0; c < 3; c++)
			out9[r * 3 + c] = m(r, c);
}
HEAD void CallingConvention SetCovariance(open3d::geometry::PointCloud* pc, int64_t index, double* in9)
{
	Eigen::Matrix3d m;
	for (int r = 0; r < 3; r++)
		for (int c = 0; c < 3; c++)
			m(r, c) = in9[r * 3 + c];
	pc->covariances_[index] = m;
}
HEAD void CallingConvention ResizeCovariances(open3d::geometry::PointCloud* pc, int64_t size)
{
	pc->covariances_.resize(size);
}
#pragma endregion

#pragma region Bounds
HEAD void CallingConvention GetMinBound(open3d::geometry::PointCloud* pc, double* out3)
{
	Eigen::Vector3d v = pc->GetMinBound();
	out3[0] = v.x(); out3[1] = v.y(); out3[2] = v.z();
}
HEAD void CallingConvention GetMaxBound(open3d::geometry::PointCloud* pc, double* out3)
{
	Eigen::Vector3d v = pc->GetMaxBound();
	out3[0] = v.x(); out3[1] = v.y(); out3[2] = v.z();
}
HEAD void CallingConvention GetCenter(open3d::geometry::PointCloud* pc, double* out3)
{
	Eigen::Vector3d v = pc->GetCenter();
	out3[0] = v.x(); out3[1] = v.y(); out3[2] = v.z();
}
HEAD void CallingConvention GetAxisAlignedBoundingBox(open3d::geometry::PointCloud* pc, double* outMin3, double* outMax3)
{
	open3d::geometry::AxisAlignedBoundingBox box = pc->GetAxisAlignedBoundingBox();
	outMin3[0] = box.min_bound_.x(); outMin3[1] = box.min_bound_.y(); outMin3[2] = box.min_bound_.z();
	outMax3[0] = box.max_bound_.x(); outMax3[1] = box.max_bound_.y(); outMax3[2] = box.max_bound_.z();
}
HEAD open3d::geometry::OrientedBoundingBox* CallingConvention GetOrientedBoundingBox(open3d::geometry::PointCloud* pc, int robust)
{
	return new open3d::geometry::OrientedBoundingBox(pc->GetOrientedBoundingBox(robust != 0));
}
HEAD open3d::geometry::OrientedBoundingBox* CallingConvention GetMinimalOrientedBoundingBox(open3d::geometry::PointCloud* pc, int robust)
{
	return new open3d::geometry::OrientedBoundingBox(pc->GetMinimalOrientedBoundingBox(robust != 0));
}
#pragma endregion

#pragma region OrientedBoundingBox handle
HEAD void CallingConvention DeleteOrientedBoundingBox(open3d::geometry::OrientedBoundingBox* obb)
{
	delete obb;
}
HEAD void CallingConvention ShowOrientedBoundingBox(open3d::geometry::OrientedBoundingBox* obb)
{
	std::vector<std::shared_ptr<const open3d::geometry::Geometry>> geometries;
	std::shared_ptr<const open3d::geometry::Geometry> ptr(obb, [](const open3d::geometry::Geometry*) {});

	geometries.push_back(ptr);
	open3d::visualization::DrawGeometries(geometries);
}
#pragma endregion

#pragma region Transform
HEAD void CallingConvention Transform(open3d::geometry::PointCloud* pc, double* matrix16)
{
	Eigen::Matrix4d m;
	for (int r = 0; r < 4; r++)
		for (int c = 0; c < 4; c++)
			m(r, c) = matrix16[r * 4 + c];
	pc->Transform(m);
}
HEAD void CallingConvention Translate(open3d::geometry::PointCloud* pc, double* translation3, int relative)
{
	pc->Translate(Eigen::Vector3d(translation3[0], translation3[1], translation3[2]), relative != 0);
}
HEAD void CallingConvention Scale(open3d::geometry::PointCloud* pc, double scale, double* center3)
{
	pc->Scale(scale, Eigen::Vector3d(center3[0], center3[1], center3[2]));
}
HEAD void CallingConvention Rotate(open3d::geometry::PointCloud* pc, double* rotation9, double* center3)
{
	Eigen::Matrix3d r;
	for (int i = 0; i < 3; i++)
		for (int j = 0; j < 3; j++)
			r(i, j) = rotation9[i * 3 + j];
	pc->Rotate(r, Eigen::Vector3d(center3[0], center3[1], center3[2]));
}
HEAD void CallingConvention Append(open3d::geometry::PointCloud* pc, open3d::geometry::PointCloud* other)
{
	*pc += *other;
}
#pragma endregion

#pragma region Array memory management
// Every native export that returns a variable-length array allocates it with
// new[]; callers must free it with the matching function below once copied
// into managed memory.
HEAD void CallingConvention FreeDoubleArray(double* arr)
{
	delete[] arr;
}
HEAD void CallingConvention FreeInt64Array(int64_t* arr)
{
	delete[] arr;
}
HEAD void CallingConvention FreeInt32Array(int32_t* arr)
{
	delete[] arr;
}
#pragma endregion

#pragma region Downsampling, filtering, cropping
// shared_ptr<PointCloud> results are heap-copied into a new raw-owned
// instance via the copy constructor rather than releasing the shared_ptr -
// make_shared's combined control-block/object allocation can't be delete'd
// as a raw pointer. Callers free the result with the existing DeletePointCloud.

HEAD open3d::geometry::PointCloud* CallingConvention SelectByIndex(open3d::geometry::PointCloud* pc, int64_t* indices, int64_t count, int invert)
{
	std::vector<size_t> idx(indices, indices + count);
	auto result = pc->SelectByIndex(idx, invert != 0);
	return new open3d::geometry::PointCloud(*result);
}

HEAD open3d::geometry::PointCloud* CallingConvention VoxelDownSample(open3d::geometry::PointCloud* pc, double voxelSize)
{
	auto result = pc->VoxelDownSample(voxelSize);
	return new open3d::geometry::PointCloud(*result);
}

HEAD open3d::geometry::PointCloud* CallingConvention VoxelDownSampleAndTrace(
	open3d::geometry::PointCloud* pc,
	double voxelSize,
	double* minBound3,
	double* maxBound3,
	int approximateClass,
	int32_t** outMatrix,
	int32_t* outMatrixRows,
	int32_t* outMatrixCols,
	int32_t** outIndicesValues,
	int32_t** outIndicesOffsets,
	int32_t* outIndicesOuterCount)
{
	Eigen::Vector3d minB(minBound3[0], minBound3[1], minBound3[2]);
	Eigen::Vector3d maxB(maxBound3[0], maxBound3[1], maxBound3[2]);

	auto tupleResult = pc->VoxelDownSampleAndTrace(voxelSize, minB, maxB, approximateClass != 0);
	auto result = std::get<0>(tupleResult);
	auto& matrix = std::get<1>(tupleResult);
	auto& indices = std::get<2>(tupleResult);

	*outMatrixRows = (int32_t)matrix.rows();
	*outMatrixCols = (int32_t)matrix.cols();
	int32_t* matBuffer = new int32_t[(size_t)matrix.rows() * (size_t)matrix.cols()];
	for (int r = 0; r < matrix.rows(); r++)
		for (int c = 0; c < matrix.cols(); c++)
			matBuffer[r * matrix.cols() + c] = matrix(r, c);
	*outMatrix = matBuffer;

	// indices flattened CSR-style: offsets[i]..offsets[i+1] is the i-th
	// inner vector's slice of values.
	*outIndicesOuterCount = (int32_t)indices.size();
	int32_t* offsets = new int32_t[indices.size() + 1];
	int32_t total = 0;
	offsets[0] = 0;
	for (size_t i = 0; i < indices.size(); i++)
	{
		total += (int32_t)indices[i].size();
		offsets[i + 1] = total;
	}
	int32_t* values = new int32_t[total > 0 ? total : 1];
	int32_t pos = 0;
	for (size_t i = 0; i < indices.size(); i++)
		for (size_t j = 0; j < indices[i].size(); j++)
			values[pos++] = indices[i][j];

	*outIndicesValues = values;
	*outIndicesOffsets = offsets;

	return new open3d::geometry::PointCloud(*result);
}

HEAD open3d::geometry::PointCloud* CallingConvention UniformDownSample(open3d::geometry::PointCloud* pc, int64_t everyKPoints)
{
	auto result = pc->UniformDownSample((size_t)everyKPoints);
	return new open3d::geometry::PointCloud(*result);
}

HEAD open3d::geometry::PointCloud* CallingConvention RandomDownSample(open3d::geometry::PointCloud* pc, double samplingRatio)
{
	auto result = pc->RandomDownSample(samplingRatio);
	return new open3d::geometry::PointCloud(*result);
}

HEAD open3d::geometry::PointCloud* CallingConvention FarthestPointDownSample(open3d::geometry::PointCloud* pc, int64_t numSamples, int64_t startIndex)
{
	auto result = pc->FarthestPointDownSample((size_t)numSamples, (size_t)startIndex);
	return new open3d::geometry::PointCloud(*result);
}

HEAD open3d::geometry::PointCloud* CallingConvention CropAxisAligned(open3d::geometry::PointCloud* pc, double* min3, double* max3, int invert)
{
	open3d::geometry::AxisAlignedBoundingBox box(
		Eigen::Vector3d(min3[0], min3[1], min3[2]),
		Eigen::Vector3d(max3[0], max3[1], max3[2]));
	auto result = pc->Crop(box, invert != 0);
	return new open3d::geometry::PointCloud(*result);
}

HEAD open3d::geometry::PointCloud* CallingConvention CropOriented(open3d::geometry::PointCloud* pc, open3d::geometry::OrientedBoundingBox* obb, int invert)
{
	auto result = pc->Crop(*obb, invert != 0);
	return new open3d::geometry::PointCloud(*result);
}

HEAD void CallingConvention RemoveNonFinitePoints(open3d::geometry::PointCloud* pc, int removeNan, int removeInfinite)
{
	pc->RemoveNonFinitePoints(removeNan != 0, removeInfinite != 0);
}

HEAD void CallingConvention RemoveDuplicatedPoints(open3d::geometry::PointCloud* pc)
{
	pc->RemoveDuplicatedPoints();
}

HEAD open3d::geometry::PointCloud* CallingConvention RemoveRadiusOutliers(
	open3d::geometry::PointCloud* pc,
	int64_t nbPoints,
	double searchRadius,
	int printProgress,
	int64_t** outKept,
	int32_t* outCount)
{
	// Open3D returns (filtered_cloud, indices_kept) - the second element is
	// the surviving/inlier indices into the ORIGINAL cloud, not the removed
	// ones (verified empirically: its length always equals the filtered
	// cloud's point count).
	auto tupleResult = pc->RemoveRadiusOutliers((size_t)nbPoints, searchRadius, printProgress != 0);
	auto result = std::get<0>(tupleResult);
	auto& kept = std::get<1>(tupleResult);

	*outCount = (int32_t)kept.size();
	int64_t* buffer = new int64_t[kept.size() > 0 ? kept.size() : 1];
	for (size_t i = 0; i < kept.size(); i++)
		buffer[i] = (int64_t)kept[i];
	*outKept = buffer;

	return new open3d::geometry::PointCloud(*result);
}

HEAD open3d::geometry::PointCloud* CallingConvention RemoveStatisticalOutliers(
	open3d::geometry::PointCloud* pc,
	int64_t nbNeighbors,
	double stdRatio,
	int printProgress,
	int64_t** outKept,
	int32_t* outCount)
{
	// See the note in RemoveRadiusOutliers above - second element is the
	// kept/inlier indices, not the removed ones.
	auto tupleResult = pc->RemoveStatisticalOutliers((size_t)nbNeighbors, stdRatio, printProgress != 0);
	auto result = std::get<0>(tupleResult);
	auto& kept = std::get<1>(tupleResult);

	*outCount = (int32_t)kept.size();
	int64_t* buffer = new int64_t[kept.size() > 0 ? kept.size() : 1];
	for (size_t i = 0; i < kept.size(); i++)
		buffer[i] = (int64_t)kept[i];
	*outKept = buffer;

	return new open3d::geometry::PointCloud(*result);
}
#pragma endregion

#pragma region TriangleMesh handle
// Minimal opaque-handle surface only - no vertex/triangle accessors this
// phase. Uses the same no-op-deleter Show() pattern as PointCloud/OBB.
HEAD void CallingConvention DeleteTriangleMesh(open3d::geometry::TriangleMesh* mesh)
{
	delete mesh;
}
HEAD void CallingConvention ShowTriangleMesh(open3d::geometry::TriangleMesh* mesh)
{
	std::vector<std::shared_ptr<const open3d::geometry::Geometry>> geometries;
	std::shared_ptr<const open3d::geometry::Geometry> ptr(mesh, [](const open3d::geometry::Geometry*) {});

	geometries.push_back(ptr);
	open3d::visualization::DrawGeometries(geometries);
}
#pragma endregion

#pragma region Handle array memory management
HEAD void CallingConvention FreeHandleArray(void** arr)
{
	delete[] arr;
}
#pragma endregion

#pragma region Analysis
// searchType: 0 = KNN (uses knn), 1 = Radius (uses radius), 2 = Hybrid (uses
// radius + maxNn). No KDTreeSearchParam wrapper type on the C# side - callers
// just pass the 4 primitive params and the right subtype is built here.

HEAD void CallingConvention ComputePointCloudDistance(open3d::geometry::PointCloud* pc, open3d::geometry::PointCloud* target, double** outDistances, int32_t* outCount)
{
	std::vector<double> distances = pc->ComputePointCloudDistance(*target);
	*outCount = (int32_t)distances.size();
	double* buffer = new double[distances.size() > 0 ? distances.size() : 1];
	for (size_t i = 0; i < distances.size(); i++)
		buffer[i] = distances[i];
	*outDistances = buffer;
}

HEAD void CallingConvention EstimatePerPointCovariances(open3d::geometry::PointCloud* pc, int searchType, int64_t knn, double radius, int64_t maxNn, double** outCovariances, int32_t* outCount)
{
	std::vector<Eigen::Matrix3d> covariances;
	switch (searchType)
	{
	case 1:
		covariances = open3d::geometry::PointCloud::EstimatePerPointCovariances(*pc, open3d::geometry::KDTreeSearchParamRadius(radius));
		break;
	case 2:
		covariances = open3d::geometry::PointCloud::EstimatePerPointCovariances(*pc, open3d::geometry::KDTreeSearchParamHybrid(radius, (int)maxNn));
		break;
	default:
		covariances = open3d::geometry::PointCloud::EstimatePerPointCovariances(*pc, open3d::geometry::KDTreeSearchParamKNN((int)knn));
		break;
	}

	*outCount = (int32_t)covariances.size();
	size_t total = covariances.size() * 9;
	double* buffer = new double[total > 0 ? total : 1];
	for (size_t i = 0; i < covariances.size(); i++)
		for (int r = 0; r < 3; r++)
			for (int c = 0; c < 3; c++)
				buffer[i * 9 + r * 3 + c] = covariances[i](r, c);
	*outCovariances = buffer;
}

HEAD void CallingConvention EstimateCovariances(open3d::geometry::PointCloud* pc, int searchType, int64_t knn, double radius, int64_t maxNn)
{
	switch (searchType)
	{
	case 1:
		pc->EstimateCovariances(open3d::geometry::KDTreeSearchParamRadius(radius));
		break;
	case 2:
		pc->EstimateCovariances(open3d::geometry::KDTreeSearchParamHybrid(radius, (int)maxNn));
		break;
	default:
		pc->EstimateCovariances(open3d::geometry::KDTreeSearchParamKNN((int)knn));
		break;
	}
}

HEAD void CallingConvention ComputeMeanAndCovariance(open3d::geometry::PointCloud* pc, double* outMean3, double* outCov9)
{
	auto result = pc->ComputeMeanAndCovariance();
	Eigen::Vector3d& mean = std::get<0>(result);
	Eigen::Matrix3d& cov = std::get<1>(result);

	outMean3[0] = mean.x(); outMean3[1] = mean.y(); outMean3[2] = mean.z();
	for (int r = 0; r < 3; r++)
		for (int c = 0; c < 3; c++)
			outCov9[r * 3 + c] = cov(r, c);
}

HEAD void CallingConvention ComputeMahalanobisDistance(open3d::geometry::PointCloud* pc, double** outDistances, int32_t* outCount)
{
	std::vector<double> distances = pc->ComputeMahalanobisDistance();
	*outCount = (int32_t)distances.size();
	double* buffer = new double[distances.size() > 0 ? distances.size() : 1];
	for (size_t i = 0; i < distances.size(); i++)
		buffer[i] = distances[i];
	*outDistances = buffer;
}

HEAD void CallingConvention ComputeNearestNeighborDistance(open3d::geometry::PointCloud* pc, double** outDistances, int32_t* outCount)
{
	std::vector<double> distances = pc->ComputeNearestNeighborDistance();
	*outCount = (int32_t)distances.size();
	double* buffer = new double[distances.size() > 0 ? distances.size() : 1];
	for (size_t i = 0; i < distances.size(); i++)
		buffer[i] = distances[i];
	*outDistances = buffer;
}

HEAD void CallingConvention EstimateNormals(open3d::geometry::PointCloud* pc, int searchType, int64_t knn, double radius, int64_t maxNn, int fastNormalComputation)
{
	switch (searchType)
	{
	case 1:
		pc->EstimateNormals(open3d::geometry::KDTreeSearchParamRadius(radius), fastNormalComputation != 0);
		break;
	case 2:
		pc->EstimateNormals(open3d::geometry::KDTreeSearchParamHybrid(radius, (int)maxNn), fastNormalComputation != 0);
		break;
	default:
		pc->EstimateNormals(open3d::geometry::KDTreeSearchParamKNN((int)knn), fastNormalComputation != 0);
		break;
	}
}

HEAD void CallingConvention OrientNormalsToAlignWithDirection(open3d::geometry::PointCloud* pc, double* direction3)
{
	pc->OrientNormalsToAlignWithDirection(Eigen::Vector3d(direction3[0], direction3[1], direction3[2]));
}

HEAD void CallingConvention OrientNormalsTowardsCameraLocation(open3d::geometry::PointCloud* pc, double* cameraLocation3)
{
	pc->OrientNormalsTowardsCameraLocation(Eigen::Vector3d(cameraLocation3[0], cameraLocation3[1], cameraLocation3[2]));
}

HEAD void CallingConvention OrientNormalsConsistentTangentPlane(open3d::geometry::PointCloud* pc, int64_t k, double lambda, double cosAlphaTol)
{
	pc->OrientNormalsConsistentTangentPlane((size_t)k, lambda, cosAlphaTol);
}

HEAD open3d::geometry::TriangleMesh* CallingConvention ComputeConvexHull(open3d::geometry::PointCloud* pc, int joggleInputs, int64_t** outIndices, int32_t* outCount)
{
	auto tupleResult = pc->ComputeConvexHull(joggleInputs != 0);
	auto mesh = std::get<0>(tupleResult);
	auto& indices = std::get<1>(tupleResult);

	*outCount = (int32_t)indices.size();
	int64_t* buffer = new int64_t[indices.size() > 0 ? indices.size() : 1];
	for (size_t i = 0; i < indices.size(); i++)
		buffer[i] = (int64_t)indices[i];
	*outIndices = buffer;

	return new open3d::geometry::TriangleMesh(*mesh);
}

HEAD open3d::geometry::TriangleMesh* CallingConvention HiddenPointRemoval(open3d::geometry::PointCloud* pc, double* cameraLocation3, double radius, int64_t** outIndices, int32_t* outCount)
{
	auto tupleResult = pc->HiddenPointRemoval(Eigen::Vector3d(cameraLocation3[0], cameraLocation3[1], cameraLocation3[2]), radius);
	auto mesh = std::get<0>(tupleResult);
	auto& indices = std::get<1>(tupleResult);

	*outCount = (int32_t)indices.size();
	int64_t* buffer = new int64_t[indices.size() > 0 ? indices.size() : 1];
	for (size_t i = 0; i < indices.size(); i++)
		buffer[i] = (int64_t)indices[i];
	*outIndices = buffer;

	return new open3d::geometry::TriangleMesh(*mesh);
}

HEAD void CallingConvention ClusterDBSCAN(open3d::geometry::PointCloud* pc, double eps, int64_t minPoints, int printProgress, int32_t** outLabels, int32_t* outCount)
{
	std::vector<int> labels = pc->ClusterDBSCAN(eps, (size_t)minPoints, printProgress != 0);
	*outCount = (int32_t)labels.size();
	int32_t* buffer = new int32_t[labels.size() > 0 ? labels.size() : 1];
	for (size_t i = 0; i < labels.size(); i++)
		buffer[i] = labels[i];
	*outLabels = buffer;
}

HEAD void CallingConvention SegmentPlane(open3d::geometry::PointCloud* pc, double distanceThreshold, int ransacN, int numIterations, double probability, double* outPlaneModel4, int64_t** outInliers, int32_t* outCount)
{
	auto tupleResult = pc->SegmentPlane(distanceThreshold, ransacN, numIterations, probability);
	Eigen::Vector4d& model = std::get<0>(tupleResult);
	auto& inliers = std::get<1>(tupleResult);

	outPlaneModel4[0] = model(0);
	outPlaneModel4[1] = model(1);
	outPlaneModel4[2] = model(2);
	outPlaneModel4[3] = model(3);

	*outCount = (int32_t)inliers.size();
	int64_t* buffer = new int64_t[inliers.size() > 0 ? inliers.size() : 1];
	for (size_t i = 0; i < inliers.size(); i++)
		buffer[i] = (int64_t)inliers[i];
	*outInliers = buffer;
}

HEAD void CallingConvention DetectPlanarPatches(
	open3d::geometry::PointCloud* pc,
	double normalVarianceThresholdDeg,
	double coplanarityDeg,
	double outlierRatio,
	double minPlaneEdgeLength,
	int64_t minNumPoints,
	int searchType, int64_t knn, double radius, int64_t maxNn,
	void*** outHandles,
	int32_t* outCount)
{
	std::vector<std::shared_ptr<open3d::geometry::OrientedBoundingBox>> patches;
	switch (searchType)
	{
	case 1:
		patches = pc->DetectPlanarPatches(normalVarianceThresholdDeg, coplanarityDeg, outlierRatio, minPlaneEdgeLength, (size_t)minNumPoints, open3d::geometry::KDTreeSearchParamRadius(radius));
		break;
	case 2:
		patches = pc->DetectPlanarPatches(normalVarianceThresholdDeg, coplanarityDeg, outlierRatio, minPlaneEdgeLength, (size_t)minNumPoints, open3d::geometry::KDTreeSearchParamHybrid(radius, (int)maxNn));
		break;
	default:
		patches = pc->DetectPlanarPatches(normalVarianceThresholdDeg, coplanarityDeg, outlierRatio, minPlaneEdgeLength, (size_t)minNumPoints, open3d::geometry::KDTreeSearchParamKNN((int)knn));
		break;
	}

	*outCount = (int32_t)patches.size();
	void** buffer = new void*[patches.size() > 0 ? patches.size() : 1];
	for (size_t i = 0; i < patches.size(); i++)
		buffer[i] = new open3d::geometry::OrientedBoundingBox(*patches[i]);
	*outHandles = buffer;
}
#pragma endregion

#pragma region IO
HEAD int CallingConvention WritePointCloud(open3d::geometry::PointCloud* pc, char* path, int writeAscii, int compressed, int printProgress)
{
	// WritePointCloudOption is built entirely from primitive flags on this
	// side, never marshaled from C# - same std::function-member footgun
	// class as ReadPointCloudOption (see the note on loadPcFile).
	open3d::io::WritePointCloudOption option;
	option.write_ascii = writeAscii != 0
		? open3d::io::WritePointCloudOption::IsAscii::Ascii
		: open3d::io::WritePointCloudOption::IsAscii::Binary;
	option.compressed = compressed != 0
		? open3d::io::WritePointCloudOption::Compressed::Compressed
		: open3d::io::WritePointCloudOption::Compressed::Uncompressed;
	option.print_progress = printProgress != 0;

	return open3d::io::WritePointCloud(path, *pc, option) ? 1 : 0;
}
#pragma endregion