﻿using GameTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTest.ScriptsCore
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
            foreach (IScript script in scripts)
            {
                script.Start();
            }
        }

        public static void UpdateAllScripts(GameObject currentInstance)
        {
            foreach (IScript script in scripts)
            {
                script.ExecuteUpdate(currentInstance);
            }
        }
    }
}