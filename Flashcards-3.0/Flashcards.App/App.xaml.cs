using Flashcards.App.Windows;
using Flashcards.Liblary;
using Flashcards.ViewModels.Windows;
using System.Windows;

namespace Flashcards.App
{
    public partial class App : Application
    {
        public App()
        {
            Startup += async (s, e) =>
            {
                MainViewModel vm = (MainViewModel)FindResource("mainVM");
                vm.Sets.EnableCollectionSynchronization();
                vm.Words.EnableCollectionSynchronization();
                await vm.LoadAsync();
            };
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            MainWindow = new MainWindow();
            MainWindow.Show();
        }
    }
}