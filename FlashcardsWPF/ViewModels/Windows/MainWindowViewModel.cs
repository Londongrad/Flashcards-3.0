using Flashcards.Core.Commands.Base;
using Flashcards.Services.Abstracts;
using Flashcards.ViewModels.Base;
using Flashcards.ViewModels.UserControls;
using Microsoft.Extensions.DependencyInjection;

namespace Flashcards.ViewModels.Windows
{
    public delegate Task SetCommand(int num);
    public class MainWindowViewModel : ViewModel
    {
        public MainWindowViewModel(INavigationService navigationService)
        {
            Navigation = navigationService;
        }

        #region [ Fields ]
        private INavigationService? _navigation;
        #endregion

        #region [ Properties ]
        public INavigationService Navigation
        {
            get => _navigation!;
            private set
            {
                _navigation = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region [ Commands ]
        public RelayCommand NavigateToCSViewCommand => new RelayCommand
            (
                execute => Navigation.NavigateTo(App.ServiceProvider!.GetRequiredService<CSViewModel>())
            );
        public RelayCommand NavigateToSetsViewCommand => new RelayCommand
            (
                execute => Navigation.NavigateTo(App.ServiceProvider!.GetRequiredService<SetsViewModel>())
            );
        public RelayCommand SetVisibility => new RelayCommand
            (
            execute => SelectedSetViewModel.setCommand!(0),
            canExecute => Navigation!.CurrentView is SelectedSetViewModel
            );
        public RelayCommand GoForward => new RelayCommand
            (
            execute => SelectedSetViewModel.setCommand!(1),
            canExecute => Navigation!.CurrentView is SelectedSetViewModel
            );
        public RelayCommand GoBack => new RelayCommand
            (
            execute => SelectedSetViewModel.setCommand!(2),
            canExecute => Navigation!.CurrentView is SelectedSetViewModel
            );
        public RelayCommand ToFavorite => new RelayCommand
            (
            execute => SelectedSetViewModel.setCommand!(3),
            canExecute => Navigation!.CurrentView is SelectedSetViewModel
            );
        #endregion

        #region [ Methods ]

        #endregion
    }
}
