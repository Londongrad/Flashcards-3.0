using Flashcards.Liblary.ViewModelBase;

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
    }
}