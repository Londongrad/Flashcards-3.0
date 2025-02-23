using Flashcards.Liblary.Command;
using System.Runtime.CompilerServices;

namespace Flashcards.Liblary.ViewModelBase
{
    public abstract partial class ViewModelBase
    {
        private readonly Dictionary<string, RelayCommand> commands = new();

        /// <summary>Конструктор-фабрика команд.</summary>
        /// <typeparam name="T">Тип параметра команды.</typeparam>
        /// <param name="execute">Выполняемый метод команды.</param>
        /// <param name="canExecute">Метод, возвращающий состояние команды.</param>
        /// <param name="converter">Делегат метода преобразующего <see cref="object"/> в тип <typeparamref name="T"/>.</param>
        /// <param name="commandName">Имя комманды - обычно имя свойства.</param>
        /// <returns>Возвращает экземпляр <see cref="RelayCommand"/>.
        /// Все команды содержатся в общем словаре по ключу <paramref name="commandName"/>.
        /// Если команды в словаре нет, то она создаётся и записывается в словарь.
        /// Создаваемые команды подписываются на <see cref="CommandManager.RequerySuggested"/>.</returns>
        /// <exception cref="ArgumentException">Если один из параметров <see langword="null"/>
        /// или если <see cref="string.IsNullOrWhiteSpace(string?)">string.IsNullOrWhiteSpace(<paramref name="commandName"/>)</see>.</exception>
        protected RelayCommand GetCommand<T>(ExecuteHandler<T> execute, CanExecuteHandler<T> canExecute, ConverterFromObjectHandler<T> converter, [CallerMemberName] string? commandName = null)
            => GetCommand<T>(
                execute ?? throw executeException,
                canExecute ?? throw canExecuteException,
                converter ?? throw converterException,
                string.IsNullOrWhiteSpace(commandName) ? throw commandNameException : commandName,
                false);

        ///<inheritdoc cref="GetCommand{T}(ExecuteHandler{T}, CanExecuteHandler{T}, ConverterFromObjectHandler{T}, string?)"/>
        protected RelayCommand GetCommand<T>(ExecuteHandler<T> execute, ConverterFromObjectHandler<T> converter, [CallerMemberName] string? commandName = null)
            => GetCommand<T>(
                execute ?? throw executeException,
                null,
                converter ?? throw converterException,
                string.IsNullOrWhiteSpace(commandName) ? throw commandNameException : commandName,
                false);

        ///<inheritdoc cref="GetCommand{T}(ExecuteHandler{T}, CanExecuteHandler{T}, ConverterFromObjectHandler{T}, string?)"/>
        protected RelayCommand GetCommand<T>(ExecuteHandler<T> execute, CanExecuteHandler<T> canExecute, [CallerMemberName] string? commandName = null)
            => GetCommand<T>(
                execute ?? throw executeException,
                canExecute ?? throw canExecuteException,
                null,
                string.IsNullOrWhiteSpace(commandName) ? throw commandNameException : commandName,
                false);

        ///<inheritdoc cref="GetCommand{T}(ExecuteHandler{T}, CanExecuteHandler{T}, ConverterFromObjectHandler{T}, string?)"/>
        protected RelayCommand GetCommand<T>(ExecuteHandler<T> execute, [CallerMemberName] string? commandName = null)
            => GetCommand<T>(
                execute ?? throw executeException,
                null,
                null,
                string.IsNullOrWhiteSpace(commandName) ? throw commandNameException : commandName,
                false);

        ///<inheritdoc cref="GetCommand{T}(ExecuteHandler{T}, CanExecuteHandler{T}, ConverterFromObjectHandler{T}, string?)"/>
        protected RelayCommand GetCommand(ExecuteHandler execute, CanExecuteHandler canExecute, [CallerMemberName] string? commandName = null)
            => GetCommand<int>(
                execute ?? throw executeException,
                canExecute ?? throw canExecuteException,
                null,
                string.IsNullOrWhiteSpace(commandName) ? throw commandNameException : commandName,
                true);

        ///<inheritdoc cref="GetCommand{T}(ExecuteHandler{T}, CanExecuteHandler{T}, ConverterFromObjectHandler{T}, string?)"/>
        protected RelayCommand GetCommand(ExecuteHandler execute, [CallerMemberName] string? commandName = null)
            => GetCommand<int>(
                execute ?? throw executeException,
                null,
                null,
                string.IsNullOrWhiteSpace(commandName) ? throw commandNameException : commandName,
                true);

        /// <summary>Метод получения уже созданной команды.</summary>
        /// <param name="commandName">Имя комманды, если не заданно  то используется имя вызвавщего метода.</param>
        /// <returns>Возвращает экземпляр <see cref="RelayCommand"/>.
        /// Все команды содержатся в общем словаре по ключу <paramref name="commandName"/>.
        /// Если команды в словаре нет, то будет выкинуто исключение.</returns>
        protected RelayCommand GetCommand([CallerMemberName] string? commandName = null)
            => commands[string.IsNullOrWhiteSpace(commandName) ? throw commandNameException : commandName];

        /// <summary>Обобщенный метод возрата команды для методов с параметром любого типа и без параметра.</summary>
        /// <typeparam name="T">Тип параметра методов.</typeparam>
        /// <param name="execute"><inheritdoc cref="GetCommand{T}(ExecuteHandler{T}, CanExecuteHandler{T}, ConverterFromObjectHandler{T}, string?)"/></param>
        /// <param name="canExecute"><inheritdoc cref="GetCommand{T}(ExecuteHandler{T}, CanExecuteHandler{T}, ConverterFromObjectHandler{T}, string?)"/></param>
        /// <param name="converter"><inheritdoc cref="GetCommand{T}(ExecuteHandler{T}, CanExecuteHandler{T}, ConverterFromObjectHandler{T}, string?)"/></param>
        /// <param name="commandName"><inheritdoc cref="GetCommand{T}(ExecuteHandler{T}, CanExecuteHandler{T}, ConverterFromObjectHandler{T}, string?)"/></param>
        /// <param name="isVoid"><see langword="true"/>, если методы для команды без параметра.</param>
        /// <returns><inheritdoc cref="GetCommand{T}(ExecuteHandler{T}, CanExecuteHandler{T}, ConverterFromObjectHandler{T}, string?)"/></returns>
        private RelayCommand GetCommand<T>(Delegate execute, Delegate? canExecute, Delegate? converter, string commandName, bool isVoid)
        {
            if (!commands.TryGetValue(commandName, out var command))
            {
                if (!isVoid)
                {
                    ExecuteHandler<T> executeT = (ExecuteHandler<T>)execute;

                    command = canExecute == null
                        ? converter == null
                            ? new RelayCommand<T>(executeT)
                            : new RelayCommand<T>(executeT, (ConverterFromObjectHandler<T>)converter)
                        : converter == null
                            ? new RelayCommand<T>(executeT, (CanExecuteHandler<T>)canExecute)
                            : new RelayCommand<T>(executeT, (CanExecuteHandler<T>)canExecute, (ConverterFromObjectHandler<T>)converter);
                }
                else
                {
                    ExecuteHandler executeV = (ExecuteHandler)execute;
                    command = canExecute == null
                        ? new RelayCommand(executeV)
                        : new RelayCommand(executeV, (CanExecuteHandler)canExecute);
                }

                commands.Add(commandName, command);
            }

            return command;
        }

        /// <summary>Удаляет команду.</summary>
        /// <param name="commandName">Имя команды.</param>
        /// <returns><see langword="true"/>, если команда удалена.</returns>
        protected bool RemoveCommnad(string commandName) => commands.Remove(commandName);

        protected static readonly ArgumentException executeException = new("null не разрешён", "execute");
        protected static readonly ArgumentException canExecuteException = new("null не разрешён", "canExecute");
        protected static readonly ArgumentException converterException = new("null не разрешён", "converter");
        protected static readonly ArgumentException commandNameException = new("null, Empty и только пробелы не разрешёны", "commandName");
    }
}