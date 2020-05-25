using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using Newtonsoft.Json;

namespace XLauncher
{

    /// <summary>
    /// Config object; loaded from startup file
    /// </summary>
    class Config
    {

        [JsonProperty]
        public string WindowTitle { get; private set; } = "Game Launcher";

        [JsonProperty]
        public string GamePath { get; private set; } = "game.exe"; //dumb default

        [JsonProperty]
        public string GameArgs { get; private set; } = null;

        [JsonProperty]
        public string OptionsPath { get; private set; } = null;

        [JsonProperty]
        public string HelpPath { get; private set; } = null;

        [JsonProperty]
        public string ImagePath { get; private set; } = null;

        [JsonProperty]
        public string MusicPath { get; private set; } = null;

        [JsonProperty]
        public bool LoopMusic { get; private set; } = false;

        [JsonProperty]
        public int Width { get; private set; } = 640;

        [JsonProperty]
        public int Height { get; private set; } = 480;

        [JsonProperty]
        public bool CloseOnPlay { get; private set; } = true;
        
        private Config()
        {

        }

        public static Config ReadConfig(string path)
        {
            return JsonConvert.DeserializeObject<Config>(File.ReadAllText(path));
        }
        
    }
}
