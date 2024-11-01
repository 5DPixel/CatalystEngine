using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;
using CatalystEngine.Graphics;

namespace CatalystEngine.Models
{
    internal sealed class Plane : GameObject
    {
        public Plane(Vector3 position, Texture texture, float rotationAngle = 90f, float scale = 1f)
        {
            Position = position;
            Rotation = rotationAngle;
            Scale = scale;
            _Texture = texture;
        }

        public List<Vector3> vertices = new List<Vector3>()
        {
            new Vector3(-0.5f, -0.5f, 0.5f), // topleft vert
            new Vector3(0.5f, -0.5f, 0.5f), // topright vert
            new Vector3(0.5f, -0.5f, -0.5f), // bottomright vert
            new Vector3(-0.5f, -0.5f, -0.5f), // bottomleft vert
        };

        public List<Vector2> texCoords = new List<Vector2>
        {
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f)
        };

        public List<uint> indices = new List<uint>
        {
            0, 1, 2,  // Triangle formed by vertices 0, 1, 2
            2, 3, 0   // Second triangle formed by vertices 2, 3, 0
        };

        public override void Render(int modelLocation, int viewLocation, int projectionLocation, Matrix4 view, Matrix4 projection)
        {
            // Set up the model matrix
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

            // Draw call
            GL.DrawElements(PrimitiveType.Triangles, indices.Count, DrawElementsType.UnsignedInt, 0);
        }

    }
}
