namespace Flashcards.Models
{
    public class SetEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<WordEntity> Words { get; set; } = [];
    }
}
