﻿using FlashcardsLiblary;
using FlashcardsLiblary.Command;
using FlashcardsLiblary.Repository;
using FlashcardsLiblary.ViewModelBase;
using FlashcardsViewModels.UserControls;
using System.Collections.ObjectModel;

namespace FlashcardsViewModels.Windows
{
    public class MainViewModel : ViewModelBase, INavigationService
    {
        #region [ Fields ]
        private readonly IRepository<Set> _setRepository;
        private readonly IRepository<Word> _wordRepository;
        private readonly INavigationService navigator;
        private readonly CreateSetViewModel createSetVM;
        private readonly SetsViewModel setsVM;
        private readonly SetViewModel setVM;
        #endregion
        public MainViewModel(IRepository<Set> setRepository, IRepository<Word> wordRepository)
        {
            _setRepository = setRepository;
            _wordRepository = wordRepository;
            navigator = this;
            navigator.NavigateTo(createSetVM = new());
            setsVM = new();
            setVM = new();
            Sets = setRepository.GetAllAsync();
            Words = wordRepository.GetAllAsync();
            navigator.AddCreator(typeof(CreateSetViewModel), () => createSetVM);
            navigator.AddCreator(typeof(SetsViewModel), () => setsVM);
            navigator.AddCreator(typeof(SetViewModel), () => setVM);
        }

        #region [ Properties ]
        public ReadOnlyObservableCollection<Set> Sets { get; }
        public ReadOnlyObservableCollection<Word> Words { get; }
        #endregion

        #region [ Commands ]
        public RelayCommand CreateSetCommand => GetCommand<CreateSetViewModel>
            (
                async setVM =>
                {
                    await _setRepository.AddAsync(new Set(setVM.Id, setVM.Name!));
                },
                setVM => !string.IsNullOrEmpty(setVM.Name)
            );
        public RelayCommand DeleteSetCommand => GetCommand<Set>
            (
                async set =>
                {
                    await _setRepository.DeleteAsync(set.Id);
                }
            );
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
        #endregion

        #region [ Methods ]
        public void RaiseCurrentChanged() => RaisePropertyChanged(nameof(INavigationService.Current));

        public async Task LoadAsync()
        {
            await _setRepository.LoadAsync();
            await _wordRepository.LoadAsync();
        }
        #endregion
    }
}
