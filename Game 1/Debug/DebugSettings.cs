using Newtonsoft.Json;

namespace Game_1.Debug
{
    internal static class DebugSettings
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

                foreach (var setting in debugInfo.settings)
                {
                    logIDs = setting.logGameObjectIDs;
                    showFPS = setting.showFPS;
                }

                settings = new Settings
                {
                    LogGameObjectIDs = logIDs,
                    showFPS = showFPS
                };
            }
            catch (Exception)
            {
                Console.WriteLine($"Error reading or parsing the file {debugJsonFilePath}.");
            }

            return settings;
        }

        public struct Settings
        {
            public Settings(bool logGameObjectIDs, bool showFPSCounter)
            {
                LogGameObjectIDs = logGameObjectIDs;
                showFPS = showFPSCounter;
            }

            public bool LogGameObjectIDs { get; set; }
            public bool showFPS { get; set; }
        }
    }
}
