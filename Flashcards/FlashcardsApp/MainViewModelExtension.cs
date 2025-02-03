using FlashcardsRepository;
using FlashcardsViewModels.Windows;
using System.Windows.Markup;

namespace FlashcardsApp
{
    [MarkupExtensionReturnType(typeof(MainViewModel))]
    public class MainViewModelExtension : MarkupExtension
    {
        public MainViewModelExtension() { }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new MainViewModel(new DbSetRepository(), new DbWordRepository());
        }
    }
}
