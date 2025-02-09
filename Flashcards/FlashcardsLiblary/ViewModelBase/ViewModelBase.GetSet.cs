using System.Runtime.CompilerServices;

namespace FlashcardsLiblary.ViewModelBase
{
    public abstract partial class ViewModelBase : BaseInpc
    {
        private readonly Dictionary<string, object?> _properties = new();

        protected T? Get<T>([CallerMemberName] string propertyName = "")
        {
            T? value;
            if (_properties.TryGetValue(propertyName, out object? _prop))
            {
                value = (T?)_prop;
            }
            else
            {
                value = default;
            }
            return value;
        }

        protected void Set<T>(T newValue, [CallerMemberName] string? propertyName = "")
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);
            T? oldValue = Get<T>(propertyName);
            _properties[propertyName] = newValue;
            Set(ref oldValue, newValue, propertyName);
        }
    }
}