using CatalystEngine.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System.IO.IsolatedStorage;

namespace CatalystEngine.Models
{
    internal class Mesh : GameObject
    {
        public List<uint> indices;
        public List<Vector3> vertices;
        public string FilePath;
        public List<Vector3> normals;
        public IBO ibo { get; private set; }
        public VAO vao { get; private set; }
        public VBO vbo { get; private set; }
        public VBO uvVBO { get; private set; }

        public List<Vector2> texCoords = new List<Vector2>
        {
        };
        public Mesh(Vector3 position, Texture texture, string filePath, float rotationAngle = 90f, float scale = 1f)
        {
            Position = position;
            Rotation = rotationAngle;
            Scale = scale;
            _Texture = texture;
            FilePath = filePath;
            string objContent = System.IO.File.ReadAllText(filePath);
            LoadFromString(objContent);
            ibo = new IBO(indices);
            vao = new VAO();
            vbo = new VBO(vertices);
            uvVBO = new VBO(texCoords);
            vao.LinkToVAO(0, 3, vbo); // Assuming mesh has 3 components per vertex (x, y, z)
            vao.LinkToVAO(1, 2, uvVBO);
        }

        public void LoadFromString(string objContent)
        {
            // Initialize lists for vertices, texture coordinates, and indices
            vertices = new List<Vector3>();
            texCoords = new List<Vector2>();
            indices = new List<uint>();
            normals = new List<Vector3>();

            // Separate lines from the file content
            var lines = objContent.Split('\n');

            // Loop through each line
            foreach (var line in lines)
            {
                // Trim whitespace from the line
                string trimmedLine = line.Trim();

                // Parse vertex data
                if (trimmedLine.StartsWith("v "))
                {
                    // Parse vertex coordinates
                    string[] parts = trimmedLine.Substring(2).Split(' ');

                    if (parts.Length >= 3 &&
                        float.TryParse(parts[0], out float x) &&
                        float.TryParse(parts[1], out float y) &&
                        float.TryParse(parts[2], out float z))
                    {
                        vertices.Add(new Vector3(x, y, z));
                    }
                    else
                    {
                        Console.WriteLine($"Failed to parse vertex: {line}");
                    }
                }
                // Parse texture coordinates
                else if (trimmedLine.StartsWith("vt "))
                {
                    // Parse texture coordinates
                    string[] parts = trimmedLine.Substring(3).Split(' ');

                    if (parts.Length >= 2 &&
                        float.TryParse(parts[0], out float u) &&
                        float.TryParse(parts[1], out float v))
                    {
                        texCoords.Add(new Vector2(u, v));
                    }
                    else
                    {
                        Console.WriteLine($"Failed to parse texture coordinate: {line}");
                    }
                }
                else if (trimmedLine.StartsWith("vn "))
                {
                    // Parse texture coordinates
                    string[] parts = trimmedLine.Substring(3).Split(' ');

                    if (parts.Length >= 3 &&
                        float.TryParse(parts[0], out float u) &&
                        float.TryParse(parts[1], out float v) &&
                        float.TryParse(parts[1], out float y))
                    {
                        normals.Add(new Vector3(u, v, y));
                        //Console.WriteLine(string.Join(",", normals));
                    }
                    else
                    {
                        Console.WriteLine($"Failed to parse normals: {line}");
                    }
                }
                // Parse face data
                else if (trimmedLine.StartsWith("f "))
                {
                    // Parse face indices
                    string[] faceParts = trimmedLine.Substring(2).Split(' ');

                    if (faceParts.Length >= 3)
                    {
                        // For each vertex in the face, parse the vertex/texture coordinates
                        for (int i = 0; i < faceParts.Length; i++)
                        {
                            string[] vertexData = faceParts[i].Split('/');
                            if (vertexData.Length >= 1 && uint.TryParse(vertexData[0], out uint vertexIndex))
                            {
                                // Convert 1-based OBJ indices to 0-based indices
                                indices.Add(vertexIndex - 1);

                                // If texture coordinates exist, retrieve them
                                if (vertexData.Length >= 2 && uint.TryParse(vertexData[1], out uint texCoordIndex))
                                {
                                    // Convert 1-based indices to 0-based indices for texture coordinates
                                    // You can store or use these texCoordIndex as needed
                                    // For example, you could store them in another list for later use
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Failed to parse face index: {faceParts[i]}");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Failed to parse face: {line}");
                    }
                }
            }
        }



        public override void Render(int modelLocation, int viewLocation, int projectionLocation, Matrix4 view, Matrix4 projection)
        {
            //Console.WriteLine("Indices: " + string.Join(", ", indices));
            //Console.WriteLine("Vertices: " + string.Join(", ", vertices.Select(v => $"({v.X}, {v.Y}, {v.Z})")));

            Matrix4 model = Matrix4.CreateTranslation(Position); // Position

            model *= Matrix4.CreateScale(Scale); // Scale around local origin

            model *= Matrix4.CreateTranslation(0f, 0f, 0f); // Move to origin (optional, since it's already at the origin of the vertices)
            model *= Matrix4.CreateRotationX(MathHelper.DegreesToRadians(Rotation)); // Rotate around local origin
            model *= Matrix4.CreateTranslation(Position); // Move back to the original positio
            // Set the uniform matrices
            GL.UniformMatrix4(modelLocation, true, ref model);
            GL.UniformMatrix4(viewLocation, true, ref view);
            GL.UniformMatrix4(projectionLocation, true, ref projection);

            _Texture.Bind();

            // Draw the block
            GL.DrawElements(PrimitiveType.Triangles, indices.Count, DrawElementsType.UnsignedInt, 0);
        }

    }
}
