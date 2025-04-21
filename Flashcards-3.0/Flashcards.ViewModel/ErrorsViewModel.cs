using System.Collections;
using System.ComponentModel;

namespace Flashcards.ViewModels
{
    public class ErrorsViewModel : INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<string>> _propertyErrors;
        public ErrorsViewModel()
        {
            errors = new Dictionary<string, List<string>>();
        }
        public bool HasErrors => throw new NotImplementedException();

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IEnumerable GetErrors(string? propertyName)
        {
            throw new NotImplementedException();
        }
    }
}
