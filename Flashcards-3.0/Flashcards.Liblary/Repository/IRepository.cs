using System.Collections.ObjectModel;

namespace Flashcards.Liblary.Repository
{
    /// <summary>Репозиторий <see cref="IdDto"/>.</summary>
    public interface IRepository<TId> where TId : IdDto
    {
        /// <summary>Добавление записи в Репозиторий.</summary>
        /// <param name="idDto">Экземпляр с данными для добавления. Идентификатор <paramref name="id"/> игнорируется.</param>
        /// <remarks>Если не удалось создать или добавить экземпляр, то выкдывается исключение.
        /// Игнорирование <see cref="TId.Id"/> зависит от диалекта БД.
        /// Возможно для этого добавление логики в Репозиторий.</remarks>
        /// <returns>Созданный согласно параметров экземляр <see cref="IdDto"/> и добавленный в репозиторий.
        /// Идентификатор, возвращаемего экземпляра, присваивается репозиторием.</returns>
        Task<TId> AddAsync(TId idDto);

        /// <summary>Удаление записи с указанным идентификатором.</summary>
        /// <param name="id">Идентификатор.</param>
        /// <remarks>Если не удалось удалить, то выкидывается исключение.</remarks>
        Task DeleteAsync(int id);

        /// <summary>Обновление записи Репозитория.</summary>
        /// <param name="idDto">Экземпляр с данными для обновления.
        /// Идентификатор остаётся неизменным, его поменять невозможно.</param>
        /// <remarks>Если не удалось обновить или создать экземпляр, то выкдывается исключение.</remarks>
        Task UpdateAsync(TId idDto);

        /// <summary>Возвращает отражение локального кеша БД.</summary>
        /// <returns>Метод возвращает всегда одby и тот же экземпляр <see cref="ReadOnlyObservableCollection{T}"/>.</returns>
        ReadOnlyObservableCollection<TId> GetObservableCollection();

        /// <summary>Асинхронная загрузка локального кеша (Eager loading).</summary>
        /// <returns>Задача в которой выполняется Асинхронная загрузка локального кеша.</returns>
        Task LoadAsync();

        /// <summary>Получение всех записей.</summary>
        /// <returns>Последовательность со всеми записями.</returns>
        /// <remarks>В методе производится новый запрос к БД, в отличии от метода <see cref="GetObservableCollection"/>,
        /// в котором возвращается отражение локального кеша без нового запроса в БД.</remarks>
        Task<IEnumerable<TId>> GetAllAsync();

        Task<bool> IsUnique(string name);
    }
}