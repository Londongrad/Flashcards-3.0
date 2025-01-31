using FlashcardsLiblary.Command;
using FlashcardsLiblary.Repository;
using FlashcardsLiblary.ViewModelBase;
using System.Dynamic;
using System.IO;
using System.Windows;

namespace FlashcardsViewModels.UserControls
{
    public class CreateSetViewModel : ViewModelBase
    {
        #region [ Fields ]
        //private string _set = string.Empty;
        //private string _wordName = string.Empty;
        //private string _definition = string.Empty;
        //private string _imagePath = string.Empty;
        //private string _nameDeleteSet = string.Empty;
        //private string _nameDeleteWord = string.Empty;
        //private string _thumbUpVis = "Hidden";
        //private string _exVis = "Hidden";
        #endregion

        public CreateSetViewModel()
        {
            Id = 0;
            Name = string.Empty;
        }

        #region [ Commands ]
        //public RelayCommand CreateSetCommand => new (async execute => await CreateSet(), canExecute => Set != "");
        //public RelayCommand DeleteSetCommand => new (async execute => await DeleteSet(), canExecute => NameDeleteSet != "");
        //public RelayCommand FindImageCommand => new (execute => FindImage(), canExecute => WordName != "");
        //public RelayCommand DeleteWordCommand => new (async execute => await DeleteWord(), canExecute => NameDeleteWord != "");
        //public RelayCommand AddWordCommand => new (async execute => await AddWord(), canExecute => Set != "" && WordName != "" && Definition != "" && ExVis != "Visible");

        #endregion

        #region [ Properties ]
        //public string Set
        //{
        //    get { return _set; }
        //    set
        //    {
        //        _set = value;
        //        RaisePropertyChanged();
        //    }
        //}
        //public string WordName
        //{
        //    get { return _wordName; }
        //    set
        //    {
        //        _wordName = value;
        //        RaisePropertyChanged();
        //    }
        //}
        //public string Definition
        //{
        //    get { return _definition; }
        //    set
        //    {
        //        _definition = value;
        //        RaisePropertyChanged();
        //    }
        //}
        //public string NameDeleteSet
        //{
        //    get { return _nameDeleteSet; }
        //    set
        //    {
        //        _nameDeleteSet = value;
        //        RaisePropertyChanged();
        //    }
        //}
        //public string NameDeleteWord
        //{
        //    get { return _nameDeleteWord; }
        //    set
        //    {
        //        _nameDeleteWord = value;
        //        RaisePropertyChanged();
        //    }
        //}
        //public string ThumbUpVis
        //{
        //    get { return _thumbUpVis; }
        //    set
        //    {
        //        _thumbUpVis = value;
        //        RaisePropertyChanged();
        //    }
        //}
        //public string ExVis
        //{
        //    get { return _exVis; }
        //    set
        //    {
        //        _exVis = value;
        //        RaisePropertyChanged();
        //    }
        //}
        //public string ImagePath
        //{
        //    get { return _imagePath; }
        //    set
        //    {
        //        if (value != null)
        //            _imagePath = value;
        //        RaisePropertyChanged();
        //    }
        //}

        /// <summary>Целочисленный идентификатор.</summary>
        public int Id { get => Get<int>(); set => Set(value); }

        /// <summary>Имя.</summary>
        public string? Name { get => Get<string?>(); set => Set(value); }
        #endregion

        #region [ Methods ]
        //private async Task<Set> CreateSet()
        //{
        //    var sets = await setRepository.GetAllAsync();
        //    var set = sets.FirstOrDefault(set => set.Name == Set)
        //              ??
        //              await setRepository.AddAsync(new SetEntity() { Name = Set });
        //    return set;
        //}
        //private async Task DeleteSet()
        //{
        //    MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete the set {NameDeleteSet} ?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
        //    if (result == MessageBoxResult.Yes)
        //        await setRepository.DeleteAsync(NameDeleteSet);
        //    NameDeleteSet = "";
        //}
        //private async Task DeleteWord()
        //{
        //    MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete the word {NameDeleteWord} ?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
        //    if (result == MessageBoxResult.Yes)
        //        await wordRepository.DeleteAsync(NameDeleteWord);
        //    NameDeleteWord = "";
        //}
        //private void FindImage()
        //{
        //    ThumbUpVis = "Hidden";
        //    if (WordName != "")
        //        ImagePath = Directory.GetFiles("D:\\Download\\Images", WordName + ".*", SearchOption.AllDirectories).FirstOrDefault()!;
        //    if (ImagePath != "" && ImagePath != null)
        //        ThumbUpVis = "Visible";

        //}
        //private async Task AddWord()
        //{
        //    ExVis = "Hidden";
        //    ThumbUpVis = "Hidden";
        //    var set = await CreateSet();
        //    var word = new WordEntity()
        //    {
        //        Name = WordName,
        //        Definition = Definition,
        //        ImagePath = ImagePath,
        //        SetId = set.Id
        //    };
        //    await wordRepository.AddAsync(word);
        //    WordName = "";
        //    Definition = "";
        //}
        #endregion
    }
}