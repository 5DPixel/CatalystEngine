using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using CatalystEngine.Utils;
using CatalystEngine.Models;

namespace CatalystEngine.Components
{
    internal class Rigidbody : Component
    {
        public float gravity { get; set; }
        public List<Vector3> rigidbodyPoints = new List<Vector3>();
        public Vector3 position = Vector3.Zero;
        public Vector3 AngularVelocity;
        private float dampingFactor = 0.3f;
        private float mass { get; set; }
        public Vector3 velocity = Vector3.Zero;
        private Vector3 _force;

        private Vector3 initialPosition;

        public Rigidbody() : this(1f, -9.81f) { } //Note to self that you have to give the component a parameterless constructor to work with generic method AddComponent<T>

        public Rigidbody(float mass, float gravity = -9.8f)
        {
            this.gravity = gravity;
            this.mass = mass;
        }

        public Vector3 ComputeForce()
        {
            return new Vector3(0, mass * gravity, 0);
        }

        public void AddForce(Vector3 force)
        {
            _force = force * 1000;
        }

        public Vector3 ApplyPhysics()
        {
            Vector3 force = ComputeForce() + _force;

            _force = Vector3.Zero;

            Vector3 acceleration = force / mass;
            velocity += acceleration * Time.DeltaTime;

            velocity.X *= 1 - 0.8f * Time.DeltaTime;
            velocity.Z *= 1 - 0.8f * Time.DeltaTime;

            position += velocity * Time.DeltaTime;

            return position;
        }

        public Quaternion UpdateAngularMotion()
        {
            Vector3 angularVelocityRadians = AngularVelocity * MathF.PI / 180;

            float angle = angularVelocityRadians.Length * Time.DeltaTime;
            Vector3 axis = angularVelocityRadians.Normalized();

            Quaternion deltaRotation = Quaternion.FromAxisAngle(axis, angle);

            return Quaternion.Multiply(deltaRotation, gameObject.Rotation).Normalized();
        }

        public void ApplyAngularDamping()
        {
            AngularVelocity *= (1 - dampingFactor * Time.DeltaTime);
        }

        public override void Start()
        {
            initialPosition = gameObject.Position;
        }

        public override void Update()
        {
            gameObject.Position = ApplyPhysics() + initialPosition;

            if(AngularVelocity != Vector3.Zero)
            {
                ApplyAngularDamping();
                gameObject.Rotation = UpdateAngularMotion();
            }
        }
    }
}
