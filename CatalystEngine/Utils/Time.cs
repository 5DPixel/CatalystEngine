using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CatalystEngine.Utils
{
    internal static class Time
    {
        private static float _deltaTime;

        public static float DeltaTime
        {
            get => _deltaTime;
            private set => _deltaTime = value;
        }

        public static float FixedDeltaTime = 0.01f; //Interval between physics steps
        private static long EpochSeconds
        {
            get
            {
                DateTimeOffset now = DateTimeOffset.UtcNow;
                return now.ToUnixTimeSeconds();
            }
        }
        private static long EpochMilliseconds
        {
            get
            {
                DateTimeOffset now = DateTimeOffset.UtcNow;
                return now.ToUnixTimeMilliseconds();
            }
        }

        public static void SetDeltaTime(float value, object caller)
        {
            // Validate that the caller is the allowed class
            if (caller is Window)
            {
                DeltaTime = value;
            }
            else
            {
                throw new UnauthorizedAccessException("Only SpecificClass can set DeltaTime.");
            }
        }

        public static long GetEpochSeconds()
        {
            return EpochSeconds;
        }

        public static async Task Wait(float seconds)
        {
            await Task.Delay((int)seconds * 1000);
        }

        public static long GetEpochMilliseconds()
        {
            return EpochMilliseconds;
        }
    }
}
