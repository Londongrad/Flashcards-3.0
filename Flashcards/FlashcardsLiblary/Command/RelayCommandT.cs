namespace FlashcardsLiblary.Command
{
    /// <summary>Реализация RelayCommand для методов с обобщённым параметром.</summary>
    /// <typeparam name="T">Тип параметра методов.</typeparam>
    public class RelayCommand<T> : RelayCommand
    {
        /// <summary>Конструктор команды с дженерик параметром.</summary>
        /// <param name="execute">Исполняющий метод команды.</param>
        /// <param name="canExecute">Метод, возвращающий состояние команды.</param>
        /// <param name="converter">Метод конвертирующий из <see cref="object"/> в <typeparamref name="T"/>. <br/>
        /// Этот метод вызывается когда
        /// <see href="https://docs.microsoft.com/ru-ru/dotnet/csharp/language-reference/operators/is">
        /// оператор is возвращает false на проверку совместимости </see> с типом <typeparamref name="T"/>.
        /// </param>
        public RelayCommand(ExecuteHandler<T> execute, CanExecuteHandler<T> canExecute, ConverterFromObjectHandler<T> converter)
            : base
            (

                  Convert(execute, converter)!,
                  Convert(canExecute, converter)!
            )
        { }

        /// <inheritdoc cref="RelayCommand{T}.RelayCommand(ExecuteHandler{T}, CanExecuteHandler{T}, ConverterFromObjectHandler{T})"/>
        public RelayCommand(ExecuteHandler<T> execute, CanExecuteHandler<T> canExecute)
            : base
            (

                  Convert(execute)!,
                  Convert(canExecute)!
            )
        { }

        /// <inheritdoc cref="RelayCommand{T}.RelayCommand(ExecuteHandler{T}, CanExecuteHandler{T}, ConverterFromObjectHandler{T})"/>
        public RelayCommand(ExecuteHandler<T> execute, ConverterFromObjectHandler<T> converter)
            : base
            (

                  Convert(execute, converter)!
            )
        { }

        /// <inheritdoc cref="RelayCommand{T}.RelayCommand(ExecuteHandler{T}, CanExecuteHandler{T}, ConverterFromObjectHandler{T})"/>
        public RelayCommand(ExecuteHandler<T> execute)
            : base
            (

                  Convert(execute)!
            )
        { }

        private static ExecuteHandler<object>? Convert(ExecuteHandler<T> execute, ConverterFromObjectHandler<T> converter)
        {
            if (execute is null) return null;
            ArgumentNullException.ThrowIfNull(converter);

            return p =>
            {
                if (p is T t || converter(p, out t))
                {
                    execute(t);
                }
            };
        }

        private static ExecuteHandler<object>? Convert(ExecuteHandler<T> execute)
        {
            if (execute is null) return null;

            return p =>
            {
                if (p is T t)
                {
                    execute(t);
                }
            };
        }

        private static CanExecuteHandler<object>? Convert(CanExecuteHandler<T> canExecute, ConverterFromObjectHandler<T> converter)
        {
            if (canExecute is null) return null;
            ArgumentNullException.ThrowIfNull(converter);

            return p => (p is T t || converter(p, out t)) &&
                        canExecute(t);
        }

        private static CanExecuteHandler<object>? Convert(CanExecuteHandler<T> canExecute)
        {
            if (canExecute is null) return null;

            return p => p is T t &&
                        canExecute(t);
        }
    }
}