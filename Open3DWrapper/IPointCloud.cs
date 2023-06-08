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

        //Return width of the pointcloud
        [DllImport("Open3DLibrary.dll", EntryPoint = "GetPointCloudW", CharSet = CharSet.Auto)]
        public static extern int GetPointCloudW(IntPtr PointCloudPointer);

        //Return height of the pointcloud
        [DllImport("Open3DLibrary.dll", EntryPoint = "GetPointCloudH", CharSet = CharSet.Auto)]
        public static extern int GetPointCloudH(IntPtr PointCloudPointer);

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
    }
}