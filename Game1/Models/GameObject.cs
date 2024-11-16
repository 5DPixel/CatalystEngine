using Game1.Graphics;
using OpenTK.Mathematics;

namespace Game1.Models
{
    internal abstract class GameObject
    {
        public Vector3 Position { get; set; }
        public float Rotation { get; set; }
        public float Scale { get; set; }
        public Texture _Texture { get; set; }
        public string physicsType { get; set; }
        public string Name { get; set; }

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
