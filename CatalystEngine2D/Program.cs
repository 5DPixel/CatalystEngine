using OpenTK.Windowing.GraphicsLibraryFramework;

namespace CatalystEngine2D
{
    class Program
    {
        static void Main(string[] args)
        {
            using(Window2D window = new Window2D(1280, 720))
            {
                window.Run();
            }
        }
    }
}