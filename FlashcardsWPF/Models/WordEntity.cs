namespace Flashcards.Models
{
    public class WordEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Definition { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public bool IsFavorite { get; set; } = false;
        public bool IsLastWord { get; set; } = false;
        public int SetId { get; set; }
        public SetEntity Set { get; set; }
    }
}
