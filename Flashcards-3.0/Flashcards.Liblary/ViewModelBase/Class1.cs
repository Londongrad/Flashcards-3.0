using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Flashcards.Liblary.ViewModelBase
{
    public abstract partial class ViewModelBase : INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<string>?> _propertyErrors = [];

        public bool HasErrors => _propertyErrors.Count != 0;

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IEnumerable GetErrors([CallerMemberName] string? propertyName = null)
        {
            ArgumentException.ThrowIfNullOrEmpty(propertyName);
            return (IEnumerable)_propertyErrors.GetValue(propertyName);
        }

        public void AddError(string errorMessage, [CallerMemberName] string? propertyName = null)
        {
            ArgumentException.ThrowIfNullOrEmpty(propertyName);
            if (!_propertyErrors.ContainsKey(propertyName))
            {
                _propertyErrors.Add(propertyName, []);
            }

            _propertyErrors[propertyName]!.Add(errorMessage);
            RaiseErrorsChanged(propertyName);
        }

        private void RaiseErrorsChanged([CallerMemberName] string? propertyName = null)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            RaisePropertyChanged(nameof(HasErrors));
        }
    }
}
