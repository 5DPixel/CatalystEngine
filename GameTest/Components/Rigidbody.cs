using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace GameTest.Components
{
    internal class Rigidbody
    {
        private float gravity { get; set; }
        public List<Vector3> rigidbodyPoints = new List<Vector3>();
        public Vector3 position = Vector3.Zero;
        public bool grounded = false;
        private float mass { get; set; }
        public Vector3 velocity = Vector3.Zero;

        public Rigidbody(float mass, float gravity = -1f)
        {
            this.gravity = gravity;
            this.mass = mass;
        }

        public Vector3 ComputeForce()
        {
            return new Vector3(0, mass * gravity, 0);
        }

        public Vector3 ApplyPhysics(float deltaTime)
        {
            Vector3 force = ComputeForce();
            Vector3 acceleration = force / mass;
            velocity += acceleration * deltaTime;
            position += velocity * deltaTime;

            if (grounded)
            {
                velocity = Vector3.Zero;
            }

            return position;
        }
    }
}
