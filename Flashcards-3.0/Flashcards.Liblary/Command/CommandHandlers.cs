// Delegates for WPF Command Methods
namespace Flashcards.Liblary.Command
{
    /// <summary>Делегат исполняющего метода команды без параметра.</summary>
    public delegate void ExecuteHandler();

    /// <summary>Делегат метода возвращающего состояние команды без параметра.</summary>
    public delegate bool CanExecuteHandler();

    /// <summary>Делегат исполняющего метода команды с параметром.</summary>
    public delegate void ExecuteHandler<T>(T parameter);

    /// <summary>Делегат метода возвращающего состояние команды с параметром.</summary>
    public delegate bool CanExecuteHandler<T>(T parameter);

    /// <summary>Делегат метода, конвертирующий параметр типа <see cref="object"/> в нужный тип.</summary>
    public delegate bool ConverterFromObjectHandler<T>(in object value, out T result);
}