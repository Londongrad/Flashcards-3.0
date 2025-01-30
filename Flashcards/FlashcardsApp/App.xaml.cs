using FlashcardsApp.Windows;
using System.Windows;

namespace FlashcardsApp
{
    public partial class App : Application
    {
        public static IServiceProvider? ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();

            base.OnStartup(e);
        }
    }
}
