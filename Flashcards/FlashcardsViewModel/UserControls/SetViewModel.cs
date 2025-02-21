using FlashcardsLiblary.Command;
using FlashcardsLiblary.Repository;
using FlashcardsLiblary.ViewModelBase;
using System.Speech.Synthesis;

namespace FlashcardsViewModels.UserControls
{
    public class SetViewModel : ViewModelBase
    {
        public Set? Set { get => Get<Set>(); set => Set(value); }
        public List<Word>? Words { get => Get<List<Word>>(); set => Set(value); }
        public Word? Word { get => Get<Word>(); set => Set(value); }
        public RelayCommand GoForwardCommand => GetCommand
           (
               () =>
               {
                   
               }
           );
        public RelayCommand ShowFirstWord => GetCommand
           (
               () =>
               {
                   Word = Words?[0];
                   _ = SayWordCommand;
               }
           );
        public RelayCommand SayWordCommand => GetCommand<Word>
           (
               word =>
               {
                   var speechSynthesizer = new SpeechSynthesizer();
                   speechSynthesizer.SelectVoice("Microsoft Hazel Desktop");
                   speechSynthesizer.SpeakAsync(word.Name);
               }
           );
    }
}