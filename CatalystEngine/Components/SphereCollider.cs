using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;

namespace CatalystEngine.Components
{
    internal class SphereCollider : Component
    {
        public Vector3 Center;
        public float radius = 1f;

        public SphereCollider() : this(Vector3.Zero, 1f) { }
        public SphereCollider(Vector3 center, float radius)
        {
            this.Center = center;
            this.radius = radius;
        }

        public static bool CheckIntersection(SphereCollider thisCollider, SphereCollider other)
        {
            float distanceSquared = MathF.Pow(other.Center.X - thisCollider.Center.X, 2) + MathF.Pow(other.Center.Y - thisCollider.Center.Y, 2) + MathF.Pow(other.Center.Z - thisCollider.Center.Z, 2);

            float radiiSum = thisCollider.radius + other.radius;
            float radiiSumSquared = MathF.Pow(radiiSum, 2);

            return distanceSquared <= radiiSumSquared;
        }

        public override void Update()
        {
            this.Center = gameObject.Position;
        }
    }
}
