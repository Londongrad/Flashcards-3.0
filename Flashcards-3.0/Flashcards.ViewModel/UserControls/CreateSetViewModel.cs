using Flashcards.Liblary.Command;
using Flashcards.Liblary.Repository;
using Flashcards.Liblary.ViewModelBase;
using Microsoft.Win32;

namespace Flashcards.ViewModels.UserControls
{
    public class CreateSetViewModel : ViewModelBase
    {
        private readonly IRepository<Word> _wordRepository;

        public CreateSetViewModel(IRepository<Word> repository)
        {
            _wordRepository = repository;
            Id = 0;
            Set = string.Empty;
            Word = string.Empty;
            Definition = string.Empty;
        }

        #region [ Properties ]

        /// <summary>Целочисленный идентификатор.</summary>
        public int Id { get => Get<int>(); set => Set(value); }

        /// <summary>Имя сета.</summary>
        public string Set { get => Get<string>()!; set => Set(value); }

        /// <summary>Название слова для добавления в сет</summary>
        public string Word {  get => Get<string>()!; 
            set 
            {
                //ClearErrors();
                //if (_wordRepository.IsUnique(value).Result)
                //{
                //    AddError("");
                //}
                
                Set(value);
                ValidateWordAsync();
            }
        }

        /// <summary>Определение слова для добавления в сет</summary>
        public string Definition { get => Get<string>()!; set { Set(value); } }

        /// <summary>Путь до картинки к слову</summary>
        public string? ImagePath { get => Get<string>(); set { Set(value); } }

        #endregion [ Properties ]

        #region [ Commands ]

        public RelayCommand FindImageCommand => GetCommand
    (
        () =>
        {
            OpenFileDialog dlg = new()
            {
                Filter = "All Pictures (*.emf;*.wmf;*.jpg;*.jpeg;*.jfif;*.jpe;*.png;*.bmp;*.dib;*.rle;*.gif;*.emz;*.wmz;*.tif;*.tiff;*.svg;*.ico;*.webp)" +
                         "|*.emf;*.wmf;*.jpg;*.jpeg;*.jfif;*.jpe;*.png;*.bmp;*.dib;*.rle;*.gif;*.emz;*.wmz;*.tif;*.tiff;*.svg;*.ico;*.webp",
                InitialDirectory = "D:\\Download\\Images"
            };

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
                ImagePath = dlg.FileName;

            //ImagePath = Directory.GetFiles("D:\\Download\\Images", Word + ".*", SearchOption.AllDirectories).FirstOrDefault()!;
        },
        () => !string.IsNullOrEmpty(Word)

    );

        #endregion [ Commands ]

        #region [ Methods ]

        public void Clear()
        {
            Id = 0;
            Word = string.Empty;
            Definition = string.Empty;
            ImagePath = null;
        }

        private async void ValidateWordAsync()
        {
            string word = Word; // кешируется валидируемое значение
            AddError("Процесс валидации"); // Здесь обычно не стринг сообщение, а другой тип.

            try
            {
                var error = await _wordRepository.IsUnique(word);
                if (word != Word)
                    return;

                // Обработка возращённой ошибки.
            }
            catch
            {
                // Обработка исключений
            }
        }

        #endregion [ Methods ]
    }
}