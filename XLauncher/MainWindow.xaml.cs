using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using NAudio;
using NAudio.Wave;
using NAudioExtensions;

namespace XLauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Config Config;
        //private WindowsMediaPlayer Player;

        private LoopStream MusicStream;
        private WaveOutEvent OutputDevice;

        private string CurrentDirectory => Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {

                //get startup args
                string configPath = Path.Combine(CurrentDirectory, "startup.json");
                Config = Config.ReadConfig(configPath);

                //do stuff
                Title = Config.WindowTitle;
                Width = Config.Width;
                Height = Config.Height;
                Recenter();

                //hide unused buttons
                if(string.IsNullOrEmpty(Config.HelpPath))
                {
                    ButtonHelp.Visibility = Visibility.Hidden;
                }

                if(string.IsNullOrEmpty(Config.OptionsPath))
                {
                    ButtonOpts.Visibility = Visibility.Hidden;
                }

                //probably a gross hack...
                if (!string.IsNullOrEmpty(Config.ImagePath))
                {
                    string imagePath = Path.Combine(CurrentDirectory, Config.ImagePath);
                    if (File.Exists(imagePath))
                    {
                        StartupImage.Source = new BitmapImage(new Uri(imagePath));
                    }
                }

                //music!
                if(!string.IsNullOrEmpty(Config.MusicPath))
                {
                    try
                    {
                        string musicPath = Path.Combine(CurrentDirectory, Config.MusicPath);
                        if (File.Exists(musicPath))
                        {
                            MusicStream = new LoopStream(new AudioFileReader(musicPath));
                            MusicStream.EnableLooping = Config.LoopMusic;
                            OutputDevice = new WaveOutEvent();
                            OutputDevice.DeviceNumber = -1;
                            OutputDevice.Init(MusicStream);
                            OutputDevice.Play();
                        }
                    }
                    catch(Exception ex)
                    {
                        Debug.WriteLine($"Failed to play music!\n {(ex.GetType().Name)}: {ex.Message}");
                        Debug.WriteLine(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to initialize launcher!\n {(ex.GetType().Name)}: {ex.Message}");

                Debug.WriteLine("Failed to initialize launcher!");
                Debug.WriteLine(ex);
                Close();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            //dispose of audio
            if (OutputDevice != null)
                OutputDevice.Dispose();

            if (MusicStream != null)
                MusicStream.Dispose();
        }


        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            //Application.Current.Shutdown();
            Close();
            //Environment.Exit(0);
        }

        private void ButtonHelp_Click(object sender, RoutedEventArgs e)
        {
            //System.Diagnostics.Debug.Write(helpPath);
            try
            {
                var helpProc = new Process();
                helpProc.StartInfo.FileName = Path.Combine(CurrentDirectory, Config.HelpPath);
                helpProc.StartInfo.UseShellExecute = true;
                helpProc.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to open help! {(ex.GetType().Name)}: {ex.Message}");

                Debug.WriteLine("Failed to open help!");
                Debug.WriteLine(ex);
                //Environment.Exit(0);
            }
        }

        private void ButtonOpts_Click(object sender, RoutedEventArgs e)
        {
            //System.Diagnostics.Debug.Write(optsPath);
            try
            {
                var optionsProc = new Process();
                optionsProc.StartInfo.FileName = Path.Combine(CurrentDirectory, Config.OptionsPath);
                optionsProc.StartInfo.UseShellExecute = true;
                optionsProc.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to open options! {(ex.GetType().Name)}: {ex.Message}");

                Debug.WriteLine("Failed to open options!");
                Debug.WriteLine(ex);
                //Environment.Exit(0);
            }
        }

        private void ButtonPlay_Click(object sender, RoutedEventArgs e)
        {
            //System.Diagnostics.Debug.Write(gamePath);
            try
            {
                string fullGamePath = Path.Combine(CurrentDirectory, Config.GamePath);
                Process gameProcess = new Process();
                gameProcess.StartInfo.FileName = fullGamePath;
                gameProcess.StartInfo.WorkingDirectory = Path.GetDirectoryName(fullGamePath);
                if(!string.IsNullOrEmpty(Config.GameArgs))
                    gameProcess.StartInfo.Arguments = Config.GameArgs;

                gameProcess.Start();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to open game! {(ex.GetType().Name)}: {ex.Message}");

                Debug.WriteLine("Failed to open game!");
                Debug.WriteLine(ex);
                //Environment.Exit(0);
            }

            //probably want to close on play
            if(Config.CloseOnPlay)
                Close();
        }

        /// <summary>
        /// Recenters the window
        /// </summary>
        /// <remarks>Modified from https://stackoverflow.com/questions/4019831/how-do-you-center-your-main-window-in-wpf/4022379#4022379</remarks>
        private void Recenter()
        {
            //get the current monitor
            System.Windows.Forms.Screen currentMonitor = System.Windows.Forms.Screen.FromHandle(new System.Windows.Interop.WindowInteropHelper(Application.Current.MainWindow).Handle);

            //find out if our app is being scaled by the monitor
            PresentationSource source = PresentationSource.FromVisual(Application.Current.MainWindow);
            double dpiScaling = (source != null && source.CompositionTarget != null ? source.CompositionTarget.TransformFromDevice.M11 : 1);

            //get the available area of the monitor
            var workArea = currentMonitor.WorkingArea; //we do need to scale the workarea
            var workAreaWidth = (int)Math.Floor(workArea.Width * dpiScaling);
            var workAreaHeight = (int)Math.Floor(workArea.Height * dpiScaling);

            //move to the centre
            Application.Current.MainWindow.Left = (((workAreaWidth - (Width)) / 2) + (workArea.Left * dpiScaling)); //we do not need to scale the width
            Application.Current.MainWindow.Top = (((workAreaHeight - (Height)) / 2) + (workArea.Top * dpiScaling));
        }

    }
}
