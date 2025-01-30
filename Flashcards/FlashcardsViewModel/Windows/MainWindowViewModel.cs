using FlashcardsLiblary.Repository;
using FlashcardsLiblary.ViewModelBase;

namespace FlashcardsViewModels.Windows
{
    public delegate Task SetCommand(int num);
    public class MainWindowViewModel : ViewModelBase
    {

        //#region [ Fields ]
        //private INavigationService? _navigation;
        //private IWordRepository? _wordRepository;
        //#endregion

        //public MainWindowViewModel(IRepository<Word> wordRepository)
        //{
        //    Navigation = navigationService;
        //    _wordRepository = wordRepository;
        //}

        //#region [ Properties ]
        //public INavigationService Navigation
        //{
        //    get => _navigation!;
        //    private set
        //    {
        //        _navigation = value;
        //        OnPropertyChanged();
        //    }
        //}
        //#endregion

        //#region [ Commands ]
        //public RelayCommand NavigateToCSViewCommand => new RelayCommand
        //    (
        //        execute => Navigation.NavigateTo(App.ServiceProvider!.GetRequiredService<CSViewModel>())
        //    );
        //public RelayCommand NavigateToSetsViewCommand => new RelayCommand
        //    (
        //        execute => Navigation.NavigateTo(App.ServiceProvider!.GetRequiredService<SetsViewModel>())
        //    );
        //public RelayCommand SetVisibility => new RelayCommand
        //    (
        //    execute => SelectedSetViewModel.setCommand!(0),
        //    canExecute => Navigation!.CurrentView is SelectedSetViewModel
        //    );
        //public RelayCommand GoForward => new RelayCommand
        //    (
        //    execute => SelectedSetViewModel.setCommand!(1),
        //    canExecute => Navigation!.CurrentView is SelectedSetViewModel
        //    );
        //public RelayCommand GoBack => new RelayCommand
        //    (
        //    execute => SelectedSetViewModel.setCommand!(2),
        //    canExecute => Navigation!.CurrentView is SelectedSetViewModel
        //    );
        //public RelayCommand ToFavorite => new RelayCommand
        //    (
        //    execute => SelectedSetViewModel.setCommand!(3),
        //    canExecute => Navigation!.CurrentView is SelectedSetViewModel
        //    );
        //public RelayCommand Import => new RelayCommand(async execute =>
        //    {
        //        //var words = new List<WordEntity>();
        //        //var path = "C:\\Users\\h-b-1\\Desktop\\words.json";

        //        //var res = JsonConvert.DeserializeObject<List<WordEntity>>(File.ReadAllText(path))!;
        //        //foreach (var word in res)
        //        //{
        //        //    await _wordRepository!.AddAsync(word);
        //        //}
        //    }
        //    );
        //#endregion

        //#region [ Methods ]

        //#endregion
    }
}
