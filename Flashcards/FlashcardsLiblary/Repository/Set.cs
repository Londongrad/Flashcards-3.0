namespace FlashcardsLiblary.Repository
{
    /// <summary>Иммутабельный класс для Set (Набор).</summary>
    public class Set(int id, string name) : IdDto(id)
    {
        /// <summary>Название набора</summary>
        public string Name { get; set; } = name;

        /// <summary>Навигационное свойство. Слова, которые относятся к набору</summary>
        //public List<Word>? Words { get; set; }
    }
}