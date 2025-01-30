using FlashcardsLiblary.ViewModelBase;

namespace FlashcardsViewModels.UserControls
{
    public class SetsViewModel : ViewModelBase
    {
        //public SetsViewModel(ISetRepository setRepository, Func<SetEntity, SetViewModel> setView)
        //{
        //    Sets = [];
        //    _setRepository = setRepository;
        //    _setView = setView;
        //    LoadSets();
        //}

        //#region [ Fields ]
        //private ObservableCollection<SetViewModel>? _sets;
        //private readonly ISetRepository _setRepository;
        //private readonly Func<SetEntity, SetViewModel> _setView;
        //#endregion

        //#region [ Properties ]
        //public ObservableCollection<SetViewModel> Sets
        //{
        //    get { return _sets!; }
        //    set
        //    {
        //        _sets = value;
        //    }
        //}
        //#endregion

        //#region [ Methods ]
        //private async Task LoadSets()
        //{
        //    var sets = await _setRepository.GetAllAsync();
        //    foreach (var set in sets)
        //        Sets.Add(_setView(set));
        //}
        //#endregion
    }
}
