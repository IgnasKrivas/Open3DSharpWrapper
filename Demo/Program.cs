using Open3DWrapper;

namespace Demo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            PointCloud pc = new PointCloud(Path.Combine(AppContext.BaseDirectory, "high_def0.ply"));
            Console.WriteLine(pc.Size);
            pc.Show();
            Console.ReadLine();
        }
    }
}