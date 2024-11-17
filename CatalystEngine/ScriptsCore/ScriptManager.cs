using CatalystEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalystEngine.ScriptsCore
{
    internal static class ScriptManager
    {
        private static List<IScript> scripts = new List<IScript>();

        public static void QueueScript(IScript script)
        {
            scripts.Add(script);
        }

        public static void DequeueScript(IScript script)
        {
            scripts.Remove(script);
        }

        public static void StartAllScripts()
        {
            foreach(IScript script in scripts)
            {
                script.Start();
            }
        }

        public static void UpdateAllScripts(Mesh currentInstance)
        {
            foreach(IScript script in scripts)
            {
                script.Update(currentInstance);
            }
        }
    }
}
