using CatalystEngine.Components;
using CatalystEngine.Graphics;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalystEngine.Models
{
    internal abstract class GameObject
    {
        public Vector3 Position { get; set; }
        public float Rotation { get; set; }
        public float Scale { get; set; }
        public Texture _Texture { get; set; }
        public String physicsType { get; set; }

        protected GameObject()
        {
            Position = Vector3.Zero;
            Rotation = 0f;
            Scale = 1f;
        }

        public virtual void Update()
        {
            //Update logic here
        }
        public abstract void Render(int modelLocation, int viewLocation, int projectionLocation, Matrix4 view, Matrix4 projection);
    }
}
