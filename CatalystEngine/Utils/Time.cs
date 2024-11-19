using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalystEngine.Utils
{
    internal static class Time
    {
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

        public static long GetEpochSeconds()
        {
            return EpochSeconds;
        }

        public static long GetEpochMilliseconds()
        {
            return EpochMilliseconds;
        }
    }
}
