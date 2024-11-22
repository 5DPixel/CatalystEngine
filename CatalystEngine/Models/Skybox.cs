using CatalystEngine.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;

namespace CatalystEngine.Models
{
    internal class Skybox
    {
        private Texture _tex;
        public Mesh mesh;
        public Skybox(Texture skyboxTexture)
        {
            _tex = skyboxTexture;
            mesh = new Mesh(new Vector3(0, 0, 0), skyboxTexture, "../../../OBJs/cube.obj", Quaternion.Identity, 1f);
        }

        public void Render(int modelLocation, int viewLocation, int projectionLocation, Matrix4 view, Matrix4 projection)
        {
            view.M41 = 0.0f;
            view.M42 = 0.0f;
            view.M43 = 0.0f;

            mesh.Render(modelLocation, viewLocation, projectionLocation, view, projection);
        }
    }
}
