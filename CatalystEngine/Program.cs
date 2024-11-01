namespace CatalystEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            Scene scene = new Scene();
            scene.LoadScene("../../../Scenes/scene1.json");
            using(Window window = new Window(1280, 720))
            {
                window.Run();
            }
        }
    }
}