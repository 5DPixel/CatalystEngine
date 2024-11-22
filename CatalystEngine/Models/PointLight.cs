using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;
using CatalystEngine.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace CatalystEngine.Models
{
    internal struct PointLight
    {
        public Vector3 Position;
        public Vector3 Color;
        public float Constant;
        public float Linear;
        public float Quadratic;

        public PointLight(Vector3 position, Vector3 color, float constant, float linear, float quadratic)
        {
            Position = position;
            Color = color;
            Constant = constant;
            Linear = linear;
            Quadratic = quadratic;
        }

        public static void RenderAll(ShaderProgram program, PointLight[] lights)
        {
            for (int i = 0; i < lights.Length; i++)
            {
                string baseName = $"pointLights[{i}]";

                int positionLocation = GL.GetUniformLocation(program.ID, $"{baseName}.position");
                GL.Uniform3(positionLocation, lights[i].Position);

                int colorLocation = GL.GetUniformLocation(program.ID, $"{baseName}.color");
                GL.Uniform3(colorLocation, lights[i].Color);

                int constantLocation = GL.GetUniformLocation(program.ID, $"{baseName}.constant");
                GL.Uniform1(constantLocation, lights[i].Constant);

                int linearLocation = GL.GetUniformLocation(program.ID, $"{baseName}.linear");
                GL.Uniform1(linearLocation, lights[i].Linear);

                int quadraticLocation = GL.GetUniformLocation(program.ID, $"{baseName}.quadratic");
                GL.Uniform1(quadraticLocation, lights[i].Quadratic);
            }
        }

    }
}
