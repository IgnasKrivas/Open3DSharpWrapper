#pragma region Includes
#include "open3d/Open3D.h"
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

	if (open3d::io::ReadPointCloud(path, *cloud) == -1)
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
	delete  pc;
}
HEAD int CallingConvention CountPointCloud(open3d::geometry::PointCloud* pc)
{
	return  pc->points_.size();
}
HEAD int CallingConvention GetPointCloudH(open3d::geometry::PointCloud* pc)
{
	return (pc->GetMaxBound().x() - pc->GetMinBound().x());
}
HEAD int CallingConvention GetPointCloudW(open3d::geometry::PointCloud* pc)
{
	return (pc->GetMaxBound().y() - pc->GetMinBound().y());
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
#pragma endregion