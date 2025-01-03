using Flashcards.Core;
using Flashcards.Services.Abstracts;
using Flashcards.ViewModels.Base;
using Flashcards.ViewModels.UserControls;
using Microsoft.Extensions.DependencyInjection;

namespace Flashcards.Services.Bases
{
    public class NavigationService() : ObservableObject, INavigationService
    {
        #region [ Fields ]
        private ViewModel _currentView = App.ServiceProvider!.GetRequiredService<CSViewModel>();
        #endregion

        #region [ Properties ]
        public ViewModel CurrentView
        {
            get => _currentView;
            private set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region [ Methods ]
        public void NavigateTo(ViewModel viewModel)
        {
            CurrentView = viewModel;
        }
        #endregion
    }
}
