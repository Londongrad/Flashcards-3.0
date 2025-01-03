using Flashcards.ViewModels.Base;

namespace Flashcards.Services.Abstracts
{
    public interface INavigationService
    {
        ViewModel CurrentView { get; }
        void NavigateTo(ViewModel viewModel);
    }
}
