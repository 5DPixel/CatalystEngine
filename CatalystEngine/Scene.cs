﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using CatalystEngine.Graphics;
using OpenTK.Mathematics;
using CatalystEngine.Models;

namespace CatalystEngine
{
    internal class Scene
    {
        public string filePath;
        //public List<GameObject> gameObjects = new List<GameObject>();
        // Predefined dictionary of textures
        private Dictionary<string, Texture> textures = new Dictionary<string, Texture>
        {
            { "texture", new Texture("Texture.png") },
            { "wood", new Texture("Wood.jpg") },
            { "stone", new Texture("Stone.jpg") }
        };

        // Dictionary for creating GameObject instances based on mesh names

        public Scene(string filePath)
        {
            this.filePath = filePath;
        }

        public void Load()
        {
            // Read the JSON file content
            string json = File.ReadAllText(this.filePath);

            // Parse the JSON into a dynamic object
            dynamic sceneData = JsonConvert.DeserializeObject(json);

            // Access the "objects" array and create GameObject instances
            foreach (var obj in sceneData.objects)
            {
                string mesh = obj.mesh;
                float[] positionArr = obj.position.ToObject<float[]>();
                float rotation = obj.rotation;
                float scale = obj.scale;
                string textureName = obj.texture;

                Vector3 position = new Vector3(positionArr[0], positionArr[1], positionArr[2]);
                Texture texture = textures.ContainsKey(textureName) ? textures[textureName] : null;

                if (mesh == "obj")
                {
                    string? filePath = obj.file?.ToString();
                    if (filePath != null)
                    {
                        // Create the Mesh object using the filePath
                        GameObject gameObject = new Mesh(position, texture, filePath, rotation, scale);
                        gameObjects.Add(gameObject);
                    }
                    else
                    {
                        Console.WriteLine("File property does not exist.");
                    }
                }
            }
        }


        // Assuming a List<GameObject> to store the objects to be rendered
        public List<GameObject> gameObjects = new List<GameObject>();

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