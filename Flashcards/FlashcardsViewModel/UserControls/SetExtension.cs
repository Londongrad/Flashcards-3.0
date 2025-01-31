using FlashcardsLiblary.Repository;
using System.Windows.Markup;

namespace FlashcardsViewModels.UserControls
{
    public class SetExtension : MarkupExtension
    {
        /// <summary>Целочисленный идентификатор.</summary>
        public int Id { get; set; }

        /// <summary>Имя.</summary>
        public string Name { get; set; }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new Set(Id, Name);
        }
    }
}
