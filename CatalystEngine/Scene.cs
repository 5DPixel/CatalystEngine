using CatalystEngine.Graphics;
using CatalystEngine.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenTK.Mathematics;
using CatalystEngine.Utils;

namespace CatalystEngine
{
    internal class Scene
    {
        public string filePath;
        public string name;

        public float ambientStrength;
        public bool isFreeCamera;
        public float cameraPitch, cameraYaw;
        public Vector3 cameraPosition;

        GameObject gameObject;
        public float gravity;
        public float mass;
        public Vector3 lightPos { get; set; }
        public Vector3 lightColor { get; set; }

        public List<GameObject> gameObjects = new List<GameObject>();
        public Dictionary<GameObject, string> rigidbodies = new Dictionary<GameObject, string> { };

        private Dictionary<string, GameObject> objectNames = new Dictionary<string, GameObject> { };


        private Dictionary<string, Texture> textures = new Dictionary<string, Texture>
        {
            { "texture", new Texture("Texture.png") },
            { "wood", new Texture("Wood.jpg") },
            { "stone", new Texture("Stone.jpg") },
            { "tree", new Texture("Tree.png") },
            { "brick", new Texture("brick.jpg") },
            { "grass", new Texture("grass.png") },
        };

        public Scene(string filePath)
        {
            this.filePath = filePath;
        }

        public void Destroy(int ID)
        {
            if(ID >= 0 && ID < gameObjects.Count)
            {
                GameObject item = gameObjects[ID];

                if(item != null)
                {
                    gameObjects.Remove(item);
                } else
                {
                    Console.WriteLine($"Item at index {ID} is null.");
                }
            }

            else
            {
                Console.WriteLine($"Item at index {ID} is out of range.");
            }
        }

        public void Update()
        {
            foreach(GameObject gameObject in gameObjects)
            {
                gameObject.Update();
                gameObject.UpdateComponents();
            }
        }

        public void Start()
        {
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Start();
                gameObject.StartComponents();
            }
        }

        public GameObject FindGameObjectByName(string name)
        {
            return objectNames[name];
        }

        public void Load()
        {
            // Read the JSON file content
            string json = File.ReadAllText(this.filePath);

            // Parse the JSON into a dynamic object
            dynamic sceneData = JsonConvert.DeserializeObject(json);

            foreach (var setting in sceneData.settings)
            {
                gravity = setting.gravity.ToObject<float>();
                ambientStrength = setting.ambient.ToObject<float>();

                isFreeCamera = setting.isFreeCamera.ToObject<bool>();
                cameraPitch = setting.cameraPitch.ToObject<float>();
                cameraYaw = setting.cameraYaw.ToObject<float>();
                float[] cameraPositionArr = setting.cameraPosition.ToObject<float[]>();

                cameraPosition = new Vector3(cameraPositionArr[0], cameraPositionArr[1], cameraPositionArr[2]);
            }

            // Access the "objects" array and create GameObject instances
            foreach (var obj in sceneData.objects)
            {
                string mesh = obj.mesh;
                string light = obj.lightType;
                float[] positionArr = obj.position.ToObject<float[]>();

                float[] rotationArr = obj.rotation.ToObject<float[]>();
                Quaternion rotation = QuaternionHelper.CreateFromEulerAnglesDegrees(rotationArr[0], rotationArr[1], rotationArr[2]);
                float scale = obj.scale;
                string textureName = obj.texture;

                Vector3 position = new Vector3(positionArr[0], positionArr[1], positionArr[2]);
                Texture texture = textures.ContainsKey(textureName) ? textures[textureName] : null;

                if (mesh == "obj")
                {
                    string filePath = obj.file.ToString();
                    string physics = obj.physics.ToString();
                    mass = obj.mass.ToObject<float>();
                    name = obj.name.ToObject<string>();

                    if (filePath != null)
                    {
                        // Create the Mesh object using the filePath
                        gameObject = new Mesh(position, texture, filePath, rotation, scale);
                        gameObjects.Add(gameObject);

                        objectNames.Add(name, gameObject);
                    }
                    else
                    {
                        Console.WriteLine("File property does not exist.");
                    }
                    rigidbodies.Add(gameObject, physics);
                    gameObject.physicsType = physics;

                    gameObject.Name = name;
                }
            }

            foreach (var light in sceneData.lights)
            {
                string lightType = light.type;

                if (lightType == "default")
                {
                    float[] lightPosArray = light.position.ToObject<float[]>();
                    float[] lightColorArray = light.color.ToObject<float[]>();

                    lightPos = new Vector3(lightPosArray[0], lightPosArray[1], lightPosArray[2]);
                    lightColor = new Vector3(lightColorArray[0], lightColorArray[1], lightColorArray[2]);
                }
            }
        }

        // Example RenderAll method
        public void RenderAll(int modelLocation, int viewLocation, int projectionLocation, Matrix4 view, Matrix4 projection)
        {
            foreach (var gameObject in gameObjects)
            {
                gameObject.Render(modelLocation, viewLocation, projectionLocation, view, projection);
            }
        }
    }
}
