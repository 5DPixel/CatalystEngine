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
                bool logIDs = false;

                foreach(var setting in debugInfo.settings)
                {
                    logIDs = setting.logGameObjectIDs;
                }

                settings = new Settings
                {
                    LogGameObjectIDs = logIDs
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
            public Settings(bool logGameObjectIDs)
            {
                LogGameObjectIDs = logGameObjectIDs;
            }

            public bool LogGameObjectIDs { get; set; }
        }
    }
}
