namespace FlashcardsLiblary.Repository
{
    /// <summary>Immutable class for a set.</summary>
    public class Set(int id, string name) : IdDto(id)
    {
        /// <summary>Set's name</summary>
        public string Name { get; set; } = name;

        /// <summary>Words in this set</summary>
        public List<Word>? Words { get; set; }
    }
}