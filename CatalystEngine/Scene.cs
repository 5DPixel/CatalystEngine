using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using CatalystEngine.Graphics;

namespace CatalystEngine
{
    internal class Scene
    {
        public void LoadScene(string filePath)
        {
            // Read the JSON file content
            string json = File.ReadAllText(filePath);

            // Parse the JSON into a dynamic object
            dynamic sceneData = JsonConvert.DeserializeObject(json);

            // Access the "objects" array
            foreach (var obj in sceneData.objects)
            {
                // Read properties from the dynamic object
                string mesh = obj.mesh;
                float[] position = obj.position.ToObject<float[]>();
                float rotation = obj.rotation;
                float scale = obj.scale;
                string texture = obj.texture;

                // Output for demonstration (you can replace this with your logic)
                Console.WriteLine($"Mesh: {mesh}, Position: {string.Join(", ", position)}, Rotation: {rotation}, Scale: {scale}, Texture: {texture}");
            }
        }
    }
}
