using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace XLauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Config Config;

        private string CurrentDirectory => Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {

                //get startup args
                string configPath = Path.Combine(CurrentDirectory, "startup.ini");
                Config = new Config(configPath);

                //do stuff
                Title = Config.WindowTitle;

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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to initialize launcher! {(ex.GetType().Name)}: {ex.Message}");

                Debug.WriteLine("Failed to initialize launcher!");
                Debug.WriteLine(ex);
                Close();
            }
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
                Process.Start(Path.Combine(CurrentDirectory, Config.HelpPath));
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
                Process.Start(Path.Combine(CurrentDirectory, Config.OptionsPath));
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
    }
}
