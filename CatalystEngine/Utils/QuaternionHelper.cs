using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalystEngine.Utils
{
    internal static class QuaternionHelper
    {
        public static Quaternion CreateFromEulerAnglesDegrees(float x, float y, float z)
        {
            return Quaternion.FromEulerAngles(new Vector3(x * (MathF.PI / 180), y * (MathF.PI / 180), z * (MathF.PI / 180)));
        }
    }
}
