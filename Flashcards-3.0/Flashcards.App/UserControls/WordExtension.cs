using Flashcards.Liblary.Repository;
using System.Windows.Markup;

namespace Flashcards.App.UserControls
{
    public class WordExtension : MarkupExtension
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Definition { get; set; } = null!;
        public string ImagePath { get; set; } = null!;
        public bool IsFavorite { get; set; }
        public bool IsLastWord { get; set; }
        public int SetId { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new Word(Id, Name, Definition, ImagePath, IsFavorite, IsLastWord, SetId);
        }
    }
}