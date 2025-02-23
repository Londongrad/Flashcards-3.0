using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;

namespace Flashcards.Liblary.Repository
{
    /// <summary>Дженерик Репозиторий для БД,</summary>
    public class DbRepository<TId> : IRepository<TId> where TId : IdDto
    {
        private readonly DbContext context;
        private readonly Func<DbContext> contextCreator;
        private readonly DbSet<TId> dbSet;

        /// <summary>Конструктор Репозитория.</summary>
        /// <param name="context">Постоянный Контекст БД, используемый для синхронизации через локальный кеш.</param>
        /// <param name="contextCreator">Функция, создающая новый одноразовый DbContext,
        /// из которого можно получить <see cref="DbSet{TEntity}">DbSet&lt;<typeparamref name="TId"/>&gt;</see>.</param>
        public DbRepository(DbContext context, Func<DbContext> contextCreator)
        {
            this.context = context;
            dbSet = context.Set<TId>();
            this.contextCreator = contextCreator;
        }

        public async Task<TId> AddAsync(TId idDto) => await Task.Run(() =>
        {
            // Добавление сущности через другой, одноразовый Контекст БД.
            using (DbContext context = contextCreator())
            {
                DbSet<TId> set = context.Set<TId>();
                set.Add(idDto);
                context.SaveChanges();
            }
            // Получение сущности в локальный кеш и её возврат.
            return dbSet.Find(idDto.Id) ?? throw new Exception("Не добавилась.");
        });

        public async Task DeleteAsync(int id) => await Task.Run(() =>
        {
            // Удаление сущности через другой, одноразовый Контекст БД.
            using (DbContext context = contextCreator())
            {
                DbSet<TId> set = context.Set<TId>();
                TId ent = set.Find(id) ?? throw new Exception("Записи с таким Id нет.");
                set.Remove(ent);
                context.SaveChanges();
            }
            {
                // Обновление локального кеша, если там есть сущность с таким Id.
                TId? ent = dbSet.Find(id);
                if (ent is not null)
                {
                    dbSet.Entry(ent).Reload();
                }
            }
        });

        public async Task<IEnumerable<TId>> GetAllAsync() => await Task.Run(() =>
        {
            return context.Set<TId>().ToList().Select(b => b);
        });

        private ReadOnlyObservableCollection<TId>? birdsReadOnlyObservableCollection;

        public ReadOnlyObservableCollection<TId> GetObservableCollection()
        {
            birdsReadOnlyObservableCollection ??= new(dbSet.Local.ToObservableCollection());
            return birdsReadOnlyObservableCollection;
        }

        public async Task UpdateAsync(TId idDto) => await Task.Run(() =>
        {
            // Обновление записи в БД через другой, одноразовый Контекст БД.
            using (DbContext context = contextCreator()) // Создание одноразового Контекста БД.
            {
                DbSet<TId> set = context.Set<TId>();

                _ = set.Update(idDto);
                _ = context.SaveChanges();
            }

            // Обновление сущности с уведомлением привязок через PropertyDescriptor об изменении Bird.IsActive.
            // Вариант показан, как альтернативная реализация.
            // В данной задаче, на практике, более лучшим вариантом будет замена сущности.
            EntityEntry<TId>? ent = dbSet.Local.FindEntry(idDto.Id);
            ent!.Reload();
            TId tid = dbSet.Find(idDto.Id) ?? throw new NullReferenceException();

            OnAllPropertiesChanged(tid);
        });

        private static readonly PropertyChangedEventArgs args = new(string.Empty);

        // Создание метода для уведомления об обновлении всех свойств Bird
        private static readonly ImmutableArray<Action<object, EventArgs>> allPropertiesChangedHandler;

        static DbRepository()
        {
            var onValueChanged = typeof(PropertyDescriptor)
                .GetMethod($"OnValueChanged", BindingFlags.Instance | BindingFlags.NonPublic)!;
            allPropertiesChangedHandler = TypeDescriptor.GetProperties(typeof(TId))
                .Cast<PropertyDescriptor>()
                .Select(onValueChanged.CreateDelegate<Action<object, EventArgs>>)
                .ToImmutableArray();
        }

        // Метод обновляющий все привязки к Bird.
        private static void OnAllPropertiesChanged(TId ent)
        {
            foreach (var p in allPropertiesChangedHandler)
            {
                p(ent, args);
            }
        }

        public async Task LoadAsync() => await Task.Run(dbSet.Load);
    }
}