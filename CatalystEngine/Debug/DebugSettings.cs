using Newtonsoft.Json;

namespace CatalystEngine.Debug
{
    static class DebugSettings
    {
        private static string debugJsonFilePath = "../../../Debug/DebugSettings.json";
        static Settings settings;

        public static Settings LoadSettings()
        {
            try
            {
                string json = File.ReadAllText(debugJsonFilePath);

                dynamic debugInfo = JsonConvert.DeserializeObject(json);
                bool showFPS = false;
                bool logIDs = false;
                bool wireframeMode = false;

                foreach(var setting in debugInfo.settings)
                {
                    logIDs = setting.logGameObjectIDs;
                    showFPS = setting.showFPS;
                    wireframeMode = setting.wireframeMode;
                }

                settings = new Settings
                {
                    LogGameObjectIDs = logIDs,
                    showFPS = showFPS,
                    wireframeMode = wireframeMode
                };
            }
            catch(Exception)
            {
                Console.WriteLine($"Error reading or parsing the file {debugJsonFilePath}.");
            }

            return settings;
        }

        public struct Settings
        {
            public Settings(bool logGameObjectIDs, bool showFPSCounter, bool _wireframeMode)
            {
                LogGameObjectIDs = logGameObjectIDs;
                showFPS = showFPSCounter;
                wireframeMode = _wireframeMode;
            }

            public bool LogGameObjectIDs { get; set; }
            public bool showFPS { get; set; }

            public bool wireframeMode { get; set; }
        }
    }
}
