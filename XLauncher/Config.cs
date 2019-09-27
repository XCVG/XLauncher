using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;

namespace XLauncher
{

    /// <summary>
    /// Config object; loaded from startup file
    /// </summary>
    class Config
    {
        //title=
        public string WindowTitle { get; private set; } = "Game Launcher";
        //start=
        public string GamePath { get; private set; } = "game.exe"; //dumb default
        //args=
        public string GameArgs { get; private set; } = null;
        //opts=
        public string OptionsPath { get; private set; } = null;
        //help=
        public string HelpPath { get; private set; } = null;
        //image=
        public string ImagePath { get; private set; } = null;
        //music=
        public string MusicPath { get; private set; } = null;
        //close=
        public bool CloseOnPlay { get; private set; } = true;

        public Config(string path)
        {
            if (File.Exists(path))
                ParseFile(File.ReadAllLines(path));            
        }

        private void ParseFile(string[] lines)
        {
            foreach(string line in lines)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(line)) //skip empty lines
                        continue;

                    string trimmedLine = line.Trim();
                    if (trimmedLine.StartsWith(";", StringComparison.OrdinalIgnoreCase) ||
                        trimmedLine.StartsWith("[", StringComparison.OrdinalIgnoreCase)) //skip comments and headers
                        continue;

                    int indexOfEquals = trimmedLine.IndexOf('=');
                    if (indexOfEquals == -1)
                        continue;

                    string[] splitLine = new string[] { trimmedLine.Substring(0, indexOfEquals), trimmedLine.Substring(indexOfEquals + 1, trimmedLine.Length - indexOfEquals - 1) };
                    ParseLine(splitLine);
                }
                catch(Exception ex)
                {
                    Debug.WriteLine($"Parse error on \"{line}\": {ex.GetType().Name}");
                }
            }
        }

        private void ParseLine(string[] line)
        {
            string key = line[0].ToLower(CultureInfo.InvariantCulture);
            switch (key)
            {
                case "title":
                    WindowTitle = line[1];
                    break;
                case "start":
                    GamePath = line[1];
                    break;
                case "args":
                    GameArgs = line[1];
                    break;
                case "options":
                    OptionsPath = line[1];
                    break;
                case "help":
                    HelpPath = line[1];
                    break;
                case "image":
                    ImagePath = line[1];
                    break;
                case "music":
                    MusicPath = line[1];
                    break;
                case "close":
                    CloseOnPlay = bool.Parse(line[1]);
                    break;
            }
        }
    }
}
