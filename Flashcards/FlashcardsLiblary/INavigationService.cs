using FlashcardsLiblary.Command;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FlashcardsLiblary
{
    public interface INavigationService : INotifyPropertyChanged
    {
        /// <summary>Текущий выбранный объект. Не важно какой именно.
        /// Реализует INPC или нет, выполянет ли какие-то другие требования - ничто не важно.</summary>
        /// <remarks>При смене значения должно происходить уведомление через интерфейс <see cref="INotifyPropertyChanged"/>.</remarks>

        object? Current { get => GetCurrent(); protected set => SetCurrent(value); }

        void SetCurrent(object? current)
        {
            if (current is null)
            {
                currents.Remove(this);
            }
            else
            {
                currents.AddOrUpdate(this, current);
            }
            RaiseCurrentChanged();
        }

        void RaiseCurrentChanged();

        private static readonly ConditionalWeakTable<INavigationService, object> currents = [];

        object? GetCurrent()
        {
            currents.TryGetValue(this, out object? current);
            return current;
        }

        /// <summary>Метод переключения текущего объекта.</summary>
        /// <param name="viewModel"></param>
        void NavigateTo(object? viewModel) => Current = viewModel;

        /// <summary>Команда вызывающая метод <see cref="NavigateTo"/> с реализацией по умолчанию.</summary>
        RelayCommand NavigateToCommand => GetCommand();

        private static readonly ConditionalWeakTable<INavigationService, RelayCommand> commands = [];

        private RelayCommand GetCommand()
        {
            if (!commands.TryGetValue(this, out RelayCommand? command))
            {
                command = new RelayCommand(NavigateTo);
                commands.Add(this, command);
            }
            return command;
        }

        RelayCommand<Type> NavigateToTypeCommand => GetTypeCommand();

        private RelayCommand<Type> GetTypeCommand()
        {
            if (!typeCommands.TryGetValue(this, out RelayCommand<Type>? command))
            {
                command = new RelayCommand<Type>(NavigateToType);
                typeCommands.Add(this, command);
            }
            return command;
        }

        private void NavigateToType(Type type)
        {
            if (typeCreators.TryGetValue(this, out Dictionary<Type, Func<object>>? creators) && creators.TryGetValue(type, out Func<object>? creator))
            {
                NavigateTo(creator());
            }
            else
            {
                NavigateTo(null);
            }
        }

        void AddCreator(Type type, Func<object> creator)
        {
            ArgumentNullException.ThrowIfNull(type, nameof(type));

            if (!typeCreators.TryGetValue(this, out Dictionary<Type, Func<object>>? creators))
            {
                creators = new Dictionary<Type, Func<object>>();
                typeCreators.Add(this, creators);
            }

            creators[type] = creator;
        }

        private static readonly ConditionalWeakTable<INavigationService, RelayCommand<Type>> typeCommands = [];
        private static readonly ConditionalWeakTable<INavigationService, Dictionary<Type, Func<object>>> typeCreators = [];
    }
}
