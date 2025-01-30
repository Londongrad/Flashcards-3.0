namespace FlashcardsLiblary.Repository
{
    /// <summary>Иммутабельный класс для Set (Набор).</summary>
    public class Set(int id, string name) : IdDto(id)
    {
        /// <summary>Название сета</summary>
        public string Name { get; set; } = name;

        /// <summary>Слова, относящиеся к сету</summary>
        public List<Word>? Words { get; set; }
    }
}
