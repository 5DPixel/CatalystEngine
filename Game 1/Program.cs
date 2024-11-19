namespace Game_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string sceneName = "scene1";
            using (Window window = new Window(1280, 720, sceneName))
            {
                window.Run();
            }
        }
    }
}