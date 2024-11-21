using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using CatalystEngine.Utils;

namespace CatalystEngine.Components
{
    internal class Rigidbody : Component
    {
        private float gravity { get; set; }
        public List<Vector3> rigidbodyPoints = new List<Vector3>();
        public Vector3 position = Vector3.Zero;
        public Vector3 torque;
        public Vector3 AngularVelocity;
        private float mass { get; set; }
        public Vector3 velocity = Vector3.Zero;

        public Rigidbody() : this(1f, -9.81f) { } //Note to self that you have to give the component a parameterless constructor to work with AddComponent<T>

        public Rigidbody(float mass, float gravity = -9.8f)
        {
            this.gravity = gravity;
            this.mass = mass;
        }

        public Vector3 ComputeForce()
        {
            return new Vector3(0, mass * gravity, 0);
        }

        public Vector3 ApplyPhysics()
        {
            Vector3 force = ComputeForce();
            Vector3 acceleration = force / mass;
            velocity += acceleration * Time.DeltaTime;
            position += velocity * Time.DeltaTime;

            return position;
        }

        public Vector3 UpdateAngularMotion()
        {
            Quaternion.ToEulerAngles(gameObject.Rotation, out Vector3 currentEulerAngles);
            Vector3 deltaEulerAngles = AngularVelocity * Time.DeltaTime;

            Vector3 newEulerAngles = currentEulerAngles + deltaEulerAngles * MathF.PI / 180;

            return newEulerAngles;
        }

        public override void Update()
        {
            gameObject.Position = ApplyPhysics();
            gameObject.Rotation = Quaternion.FromEulerAngles(UpdateAngularMotion());
        }
    }
}
