using Flashcards.Liblary.Command;
using Flashcards.Liblary.ViewModelBase;
using System.IO;

namespace Flashcards.ViewModels.UserControls
{
    public class CreateSetViewModel : ViewModelBase
    {
        public CreateSetViewModel()
        {
            Id = 0;
            Set = string.Empty;
            Word = string.Empty;
            Definition = string.Empty;
            ImagePath = string.Empty;
        }

        #region [ Properties ]

        /// <summary>Целочисленный идентификатор.</summary>
        public int Id { get => Get<int>(); set => Set(value); }

        /// <summary>Имя сета.</summary>
        public string Set { get => Get<string>()!; set => Set(value); }

        /// <summary>Название слова для добавления в сет</summary>
        public string Word
        { get => Get<string>()!; set { Set(value); } }

        /// <summary>Определение слова для добавления в сет</summary>
        public string Definition
        { get => Get<string>()!; set { Set(value); } }

        /// <summary>Путь до картинки к слову</summary>
        public string ImagePath
        { get => Get<string>()!; set { Set(value); } }

        #endregion [ Properties ]

        #region [ Commands ]
        public RelayCommand FindImageCommand => GetCommand
    (
        () =>
        {
            //OpenFileDialog dlg = new()
            //{
            //    Filter = "All Pictures (*.emf;*.wmf;*.jpg;*.jpeg;*.jfif;*.jpe;*.png;*.bmp;*.dib;*.rle;*.gif;*.emz;*.wmz;*.tif;*.tiff;*.svg;*.ico;*.webp)" +
            //             "|*.emf;*.wmf;*.jpg;*.jpeg;*.jfif;*.jpe;*.png;*.bmp;*.dib;*.rle;*.gif;*.emz;*.wmz;*.tif;*.tiff;*.svg;*.ico;*.webp"
            //};

            //Nullable<bool> result = dlg.ShowDialog();

            //if (result == true)
            //    ImagePath = dlg.FileName;

            // You can use this code, if you're lazy to select and image. Just predefine the path and name you image just like a word. And it will choose it automatically by pressing a button.

            ImagePath = Directory.GetFiles("D:\\Download\\Images", Word + ".*", SearchOption.AllDirectories).FirstOrDefault()!;
        },
        () => !string.IsNullOrEmpty(Word)
    );
        #endregion [ Commands ]
    }
}