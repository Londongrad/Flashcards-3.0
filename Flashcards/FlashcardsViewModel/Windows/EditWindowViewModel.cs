using FlashcardsLiblary.Repository;
using FlashcardsLiblary.ViewModelBase;
using Microsoft.Win32;
using System.Windows;

namespace FlashcardsViewModels.Windows
{
    public class EditWindowViewModel() : ViewModelBase
    {
        //#region [ Commands ]
        //public RelayCommand<Window> SaveChangesCommand => new RelayCommand<Window>(SaveChanges);
        //public RelayCommand ChangeImageCommand => new RelayCommand(execute => ChangeImage());
        //#endregion

        //#region [ Properties ]
        //public string Name
        //{
        //    get { return word.Name; }
        //    set
        //    {
        //        word.Name = value;
        //        OnPropertyChanged();
        //    }
        //}
        //public string Definition
        //{
        //    get { return word.Definition; }
        //    set
        //    {
        //        word.Definition = value;
        //        OnPropertyChanged();
        //    }
        //}
        //public string ImagePath
        //{
        //    get { return word.ImagePath; }
        //    set
        //    {
        //        word.ImagePath = value;
        //        OnPropertyChanged();
        //    }
        //}
        //#endregion

        //#region [ Methods ]
        //private async void SaveChanges(Window window)
        //{
        //    if (Name == "" || Definition == "") { return; }
        //    try
        //    {
        //        await wordRepository.UpdateAsync(word);
        //        if (window != null)
        //            window.Close();
        //    }
        //    catch (Exception)
        //    {
        //        MessageBox.Show("You already have a word with this name!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}

        //private void ChangeImage()
        //{
        //    var dlg = new OpenFileDialog();
        //    dlg.InitialDirectory = "D:\\Download\\Images";
        //    dlg.ShowDialog();
        //    ImagePath = dlg.FileName;
        //}
        //#endregion
    }
}
