using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace FlashcardsLiblary.Command
{
    /// <summary>Класс реализующий <see cref="ICommand"/>.<br/>
    /// Реализация взята из <see href="https://www.cyberforum.ru/wpf-silverlight/thread2390714-page4.html#post13535649"/>
    /// и дополнена конструктором для методов без параметра.</summary>
    public class RelayCommand : ICommand
    {
        private readonly CanExecuteHandler<object?> canExecute;
        private readonly ExecuteHandler<object?> execute;
        private readonly EventHandler requerySuggested;

        /// <summary>Событие извещающее об изменении состояния команды.</summary>
        public event EventHandler? CanExecuteChanged;

        /// <summary>Конструктор команды.</summary>
        /// <param name="execute">Исполняющий метод команды.</param>
        /// <param name="canExecute">Метод, возвращающий состояние команды.</param>
        public RelayCommand(ExecuteHandler<object?> execute, CanExecuteHandler<object?> canExecute)
        {
            ArgumentNullException.ThrowIfNull(execute);
            ArgumentNullException.ThrowIfNull(canExecute);
            this.execute = execute;
            this.canExecute = canExecute;

            requerySuggested = (o, e) => Invalidate();
            CommandManager.RequerySuggested += requerySuggested;
        }

        /// <inheritdoc cref="RelayCommand(ExecuteHandler{object?}, CanExecuteHandler{object?})"/>
        public RelayCommand(ExecuteHandler<object?> execute)
            : this(execute, _ => true)
        { }

        /// <inheritdoc cref="RelayCommand(ExecuteHandler{object?}, CanExecuteHandler{object?})"/>
        public RelayCommand(ExecuteHandler execute, CanExecuteHandler canExecute)
                : this
                (
                      p => execute(),
                      (canExecute is null ? null : p => canExecute())!
                )
        { }

        /// <inheritdoc cref="RelayCommand(ExecuteHandler{object?}, CanExecuteHandler{object?})"/>
        public RelayCommand(ExecuteHandler execute)
                : this
                (
                      p => execute()
                )
        { }


        private readonly Dispatcher dispatcher = Application.Current.Dispatcher;

        /// <summary>Метод, подымающий событие <see cref="CanExecuteChanged"/>.</summary>
        public void RaiseCanExecuteChanged()
        {
            if (dispatcher.CheckAccess())
                Invalidate();
            else
                dispatcher.BeginInvoke(Invalidate);
        }
        private void Invalidate()
            => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        /// <summary>Вызов метода, возвращающего состояние команды.</summary>
        /// <param name="parameter">Параметр команды.</param>
        /// <returns><see langword="true"/> - если выполнение команды разрешено.</returns>
        public bool CanExecute(object? parameter) => canExecute(parameter);

        /// <summary>Вызов выполняющего метода команды.</summary>
        /// <param name="parameter">Параметр команды.</param>
        public void Execute(object? parameter) => execute(parameter);
    }
}
