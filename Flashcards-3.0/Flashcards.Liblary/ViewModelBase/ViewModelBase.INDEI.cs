using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Flashcards.Liblary.ViewModelBase
{
    public abstract partial class ViewModelBase : INotifyDataErrorInfo
    {
        private readonly Dictionary<string, HashSet<string>?> _propertyErrors = [];

        public bool HasErrors { get => Get<bool>(); private set => Set(value); }

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IEnumerable GetErrors([CallerMemberName] string? propertyName = null)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                foreach (var errors in _propertyErrors)
                {
                    foreach (var error in errors.Value ?? [])
                    {
                        yield return error;
                    }
                }
            }
            else
            {
                foreach (var error in _propertyErrors.GetValueOrDefault(propertyName) ?? [])
                    yield return error;
            }
        }

        protected void AddError(string errorMessage, [CallerMemberName] string? propertyName = null)
        {
            ArgumentException.ThrowIfNullOrEmpty(propertyName);
            if (!_propertyErrors.ContainsKey(propertyName))
            {
                _propertyErrors.Add(propertyName, []);
            }

            _propertyErrors[propertyName]!.Add(errorMessage);
            RaiseErrorsChanged(propertyName);
        }

        protected void RaiseErrorsChanged([CallerMemberName] string? propertyName = null)
        {
            HasErrors = _propertyErrors.Count != 0;
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        protected void ClearErrors([CallerMemberName] string? propertyName = null)
        {
            ArgumentException.ThrowIfNullOrEmpty(propertyName);
            if (_propertyErrors.Remove(propertyName))
                RaiseErrorsChanged(propertyName);
        }
    }
}
