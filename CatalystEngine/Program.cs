namespace CatalystEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Choose scene: \n");
            string[] files = Directory.GetFiles("../../../Scenes");
            foreach (string file in files)
                Console.WriteLine(Path.GetFileName(file).Replace("../../../Scenes", "").Replace(".json", ""));
            Console.WriteLine("\n");
            string sceneName = Console.ReadLine();
            using (Window window = new Window(1280, 720, sceneName))
            {
                window.Run();
            }
        }
    }
}