using FlashcardsApp.Windows;
using FlashcardsLiblary;
using FlashcardsViewModels.Windows;
using System.Windows;

namespace FlashcardsApp
{
    public partial class App : Application
    {
        public App()
        {
            Startup += async (s, e) =>
            {
                MainViewModel vm = (MainViewModel)FindResource("mainVM");
                vm.Sets.EnableCollectionSynchronization();
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
