using Flashcards.Liblary.Repository;
using Flashcards.Liblary.ViewModelBase;

namespace Flashcards.ViewModels.UserControls
{
    public class SetViewModel : ViewModelBase
    {
        public List<Word>? Words { get => Get<List<Word>>(); set => Set(value); }
        public bool HideDef { get; set; }
    }
}