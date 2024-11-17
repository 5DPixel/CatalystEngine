using CatalystEngine.Graphics;
using OpenTK.Mathematics;

namespace CatalystEngine.Models
{
    internal abstract class GameObject
    {
        public Vector3 Position { get; set; }
        public float Rotation { get; set; }
        public float Scale { get; set; }
        public Texture _Texture { get; set; }
        public string physicsType { get; set; }
        public string Name { get; set; }
        public int ID { get; set; }
        private static int nextID = 0;

        protected GameObject()
        {
            Position = Vector3.Zero;
            Rotation = 0f;
            Scale = 1f;

            ID = nextID++;
        }
        public abstract void Render(int modelLocation, int viewLocation, int projectionLocation, Matrix4 view, Matrix4 projection);
    }
}
