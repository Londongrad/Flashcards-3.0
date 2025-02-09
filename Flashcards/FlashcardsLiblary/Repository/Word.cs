namespace FlashcardsLiblary.Repository
{
    /// <summary>Иммутабельный класс для Word (Слово).</summary>
    public class Word(int id, string name, string? definition, string? imagePath, bool isFavorite, bool isLastWord, int setId) : IdDto(id)
    {
        /// <summary>Слово</summary>
        public string Name { get; set; } = name;

        /// <summary>Определение слова</summary>
        public string? Definition { get; set; } = definition;

        /// <summary>Путь до картинки</summary>
        public string? ImagePath { get; set; } = imagePath;

        /// <summary>Является ли слово избранным</summary>
        public bool IsFavorite { get; set; } = isFavorite;

        /// <summary>Является ли слово последним</summary>
        public bool IsLastWord { get; set; } = isLastWord;

        /// <summary>Внешний ключ</summary>
        public int SetId { get; set; } = setId;

        /// <summary>Навигационное свойство. Сет, к которому относится слово</summary>
        public Set? Set { get; set; }
    }
}