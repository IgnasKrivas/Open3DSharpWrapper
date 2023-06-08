using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Open3DWrapper
{
    public class PointCloud
    {
        //Pointcloud object pointer
        private IntPtr _pointCloudPointer;

        private int _width;
        private int _height;
        private int _size;

        ///@brief Constructor without parameters
        ///@details Initialize an empty point cloud object
        public PointCloud()
        {
            _pointCloudPointer = IPointCloud.CreatePointCloud();
        }

        ///@brief Constructor with parameters
        ///@details Pass pointcloud file path and load pointcloud object into pointer
        ///@param Pointcloud file path
        public PointCloud(string path)
        {
            _pointCloudPointer = IPointCloud.LoadPcFile(path);
        }

        ///@brief Destructor
        ///@details memory management. When the lifetime of the object ends, the point cloud object memory is released. Wild pointers are easy to appear here, so pay attention!
        ~PointCloud()
        {
            IPointCloud.DeletePointCloud(_pointCloudPointer);
        }

        ///@brief Width
        ///@details Get the width attribute
        public int Width
        {
            get
            {
                _width = IPointCloud.GetPointCloudW(_pointCloudPointer);
                return _width;
            }
        }

        ///@brief Height
        ///@details Get the height attribute
        public int Height
        {
            get
            {
                _height = IPointCloud.GetPointCloudH(_pointCloudPointer);
                return _height;
            }
        }

        ///@brief Size
        ///@details Get the size attribute
        public int Size
        {
            get
            {
                _size = IPointCloud.CountPointCloud(_pointCloudPointer);
                return _size;
            }
        }

        //There may be errors after destructuring, but it is currently running fine. If there is a problem check pointer value
        public IntPtr PointCloudXyzPointer
        {
            get
            {
                return _pointCloudPointer;
            }
        }

        ///@brief X value
        ///@param Get X index number
        public double GetX(int index)
        {
            return IPointCloud.GetX(_pointCloudPointer, index);
        }

        ///@brief Y value
        ///@param Get Y index number
        public double GetY(int index)
        {
            return IPointCloud.GetY(_pointCloudPointer, index);
        }

        ///@brief Z value
        ///@param Get Z index number
        public double GetZ(int index)
        {
            return IPointCloud.GetZ(_pointCloudPointer, index);
        }

        ///@brief X value
        ///@param Set X index number
        ///@param index - index, x - value
        public void SetX(int index, double x)
        {
            IPointCloud.SetX(_pointCloudPointer, index, x);
        }

        ///@brief Y value
        ///@param Set Y index number
        ///@param index - index, y - value
        public void SetY(int index, double y)
        {
            IPointCloud.SetY(_pointCloudPointer, index, y);
        }

        ///@brief Z value
        ///@param Set Z index number
        ///@param index - index, z - value
        public void SetZ(int index, double z)
        {
            IPointCloud.SetZ(_pointCloudPointer, index, z);
        }

        ///@brief Resize
        ///@param Resize pointcloud size
        public void Resize(int size)
        {
            IPointCloud.Resize(_pointCloudPointer, size);
        }

        ///@brief Inserts a point to the end of pointcloud object
        ///@param x value of the pushed point
        ///@param y value of the pushed point
        ///@param z value of the pushed point
        public void Push(double x, double y, double z)
        {
            IPointCloud.Push(_pointCloudPointer, x, y, z);
        }

        ///@brief Pop up a point, similar to popping the stack, the last point in the element is popped up
        public void Pop()
        {
            IPointCloud.Pop(_pointCloudPointer);
        }

        ///@brief Clear all points in the point cloud object
        public void Clear()
        {
            IPointCloud.Clear(_pointCloudPointer);
        }
    }
}