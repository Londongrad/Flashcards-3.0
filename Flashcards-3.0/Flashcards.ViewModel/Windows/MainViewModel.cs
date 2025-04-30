using Flashcards.Liblary;
using Flashcards.Liblary.Command;
using Flashcards.Liblary.Repository;
using Flashcards.Liblary.ViewModelBase;
using Flashcards.ViewModels.UserControls;
using System.Collections.ObjectModel;

namespace Flashcards.ViewModels.Windows
{
    public class MainViewModel : ViewModelBase, INavigationService
    {
        #region [ Fields ]

        private readonly IDbModel model;

        private readonly INavigationService navigator;

        private readonly CreateSetViewModel createSetVM;
        private readonly SetsViewModel setsVM;
        private readonly SetViewModel setVM;

        #endregion [ Fields ]

        public MainViewModel(IDbModel model)
        {
            this.model = model;

            createSetVM = new(model.Words);
            setsVM = new();
            setVM = new();
            SetVMProp = setVM;

            Sets = model.Sets.GetObservableCollection();
            Words = model.Words.GetObservableCollection();

            navigator = this;
            navigator.AddCreator(typeof(CreateSetViewModel), () => createSetVM);
            navigator.AddCreator(typeof(SetsViewModel), () => setsVM);
            navigator.AddCreator(typeof(SetViewModel), () => setVM);
            navigator.AddCreator(typeof(SetViewModel), () => setVM);
            navigator.NavigateTo(createSetVM);
        }

        #region [ Properties ]

        public ReadOnlyObservableCollection<Set> Sets { get; }
        public ReadOnlyObservableCollection<Word> Words { get; }
        public SetViewModel SetVMProp { get; private set; }

        #endregion [ Properties ]

        #region [ Commands ]

        public RelayCommand CreateSetCommand => GetCommand<CreateSetViewModel>
            (
                async setVM =>
                {
                    await model.Sets.AddAsync(new Set(setVM.Id, setVM.Set!));
                },
                setVM => !string.IsNullOrEmpty(setVM.Set)
            );

        public RelayCommand SaveWordCommand => GetCommand<CreateSetViewModel>
            (
                async setVM =>
                {
                    //Retrieve needed set from local cache or create a new one and then retrieve it.
                    var set = Sets.FirstOrDefault(s => s.Name == setVM.Set) ?? await model.Sets.AddAsync(new Set(setVM.Id, setVM.Set));

                    //Add a new word to the database
                    await model.Words.AddAsync(new Word(
                        setVM.Id,
                        setVM.Word!,
                        setVM.Definition,
                        setVM.ImagePath,
                        false,
                        false,
                        set.Id
                        ));

                    setVM.Clear();
                },
                setVM => !(setVM.HasErrors && string.IsNullOrEmpty(setVM.Word) && string.IsNullOrEmpty(setVM.Set))
            );

        public RelayCommand DeleteSetCommand => GetCommand<Set>
            (
                async set =>
                {
                    //TODO Нужно предупреждение о действии.
                    await model.Sets.DeleteAsync(set.Id);
                }
            );

        public RelayCommand SelectSetCommand => GetCommand<Set>
            (
                set =>
                {
                    setVM.Words = Words.Where(w => w.SetId == set.Id).ToList();
                    navigator.NavigateTo(setVM);
                },
                set => Words.Where(w => w.SetId == set.Id).ToList().Count > 0
            );

        #endregion [ Commands ]

        #region [ Methods ]

        public void RaiseCurrentChanged() => RaisePropertyChanged(nameof(INavigationService.Current));

        public async Task LoadAsync()
        {
            await model.LoadAsync();
        }

        #endregion [ Methods ]
    }
}