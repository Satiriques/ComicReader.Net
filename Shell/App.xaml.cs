using ComicReader.Net.Shell.Startup;
using System.Windows;

namespace ComicReader.Net.Shell
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var bootstrapper = new Bootstrapper();
            bootstrapper.Bootstrap();

            var mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
