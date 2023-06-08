using Open3DWrapper;

namespace Demo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            PointCloud pc = new PointCloud("\\high_def0.ply");
            Console.WriteLine(pc.Size);
            Console.ReadLine();
        }
    }
}