using Flashcards.Liblary.Repository;
using Flashcards.Repository;
using Flashcards.ViewModels.Windows;
using System.Windows.Markup;

namespace Flashcards.App
{
    [MarkupExtensionReturnType(typeof(MainViewModel))]
    public class MainViewModelExtension : MarkupExtension
    {
        public MainViewModelExtension()
        { }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new MainViewModel(new DbModel(new ApplicationDbContext(), () => new ApplicationDbContext()));
        }
    }
}