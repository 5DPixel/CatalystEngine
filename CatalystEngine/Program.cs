namespace CatalystEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            string sceneName = "objs";
            using (Window window = new Window(1280, 720, sceneName))
            {
                window.Run();
            }
        }
    }
}