namespace CatalystEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            using(Window window = new Window(1280, 720))
            {
                window.Run();
            }
        }
    }
}