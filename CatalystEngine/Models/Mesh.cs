using CatalystEngine.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using Assimp;
using CatalystEngine.Components;

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
        public VBO normalsVBO { get; private set; }
        //private Rigidbody body;

        public List<Vector2> texCoords;
        public Mesh(Vector3 position, Texture texture, string filePath, OpenTK.Mathematics.Quaternion rotation, float scale = 1f)
        {
            Position = position;
            Rotation = rotation;
            Scale = scale;
            _Texture = texture;
            FilePath = filePath;
            LoadModel(filePath);

            ibo = new IBO(indices);
            vao = new VAO();
            vbo = new VBO(vertices);
            normalsVBO = new VBO(normals);
            uvVBO = new VBO(texCoords);

            vao.LinkToVAO(0, 3, vbo); // 3 components per vertex
            vao.LinkToVAO(1, 2, uvVBO);
            vao.LinkToVAO(2, 3, normalsVBO);
        }

        public void LoadModel(string path)
        {
            // Initialize lists for vertices, texture coordinates, and indices
            vertices = new List<Vector3>();
            normals = new List<Vector3>();
            texCoords = new List<Vector2>();
            indices = new List< uint>();

            AssimpContext importer = new AssimpContext();

            Assimp.Scene scene = importer.ImportFile(path, PostProcessSteps.Triangulate | PostProcessSteps.FlipUVs);

            foreach (var mesh in scene.Meshes)
            {
                // Get vertex data
                foreach (var vertex in mesh.Vertices)
                {
                    vertices.Add(new Vector3(vertex.X, vertex.Y, vertex.Z));
                }

                // Get texture coordinates
                foreach (var uv in mesh.TextureCoordinateChannels[0])
                {
                    texCoords.Add(new Vector2(uv.X, uv.Y));
                }

                // Get face indices
                foreach (var face in mesh.Faces)
                {
                    for (int i = 0; i < face.IndexCount; i++)
                    {
                        indices.Add((uint)face.Indices[i]);
                    }
                }

                foreach (var normal in mesh.Normals)
                {
                    normals.Add(new Vector3(normal.X, normal.Y, normal.Z));
                }
            }
        }



        public override void Render(int modelLocation, int viewLocation, int projectionLocation, Matrix4 view, Matrix4 projection)
        {

            Matrix4 model = Matrix4.CreateTranslation(Position);

            model *= Matrix4.CreateScale(Scale);

            model *= Matrix4.CreateTranslation(-Position); // Move to origin
            model *= Matrix4.CreateFromQuaternion(Rotation);
            model *= Matrix4.CreateTranslation(Position);
            // Set the uniform matrices
            GL.UniformMatrix4(modelLocation, true, ref model);
            GL.UniformMatrix4(viewLocation, true, ref view);
            GL.UniformMatrix4(projectionLocation, true, ref projection);

            _Texture.Bind();

            // Draw the block
            GL.DrawElements(OpenTK.Graphics.OpenGL4.PrimitiveType.Triangles, indices.Count, DrawElementsType.UnsignedInt, 0);
        }

    }
}
