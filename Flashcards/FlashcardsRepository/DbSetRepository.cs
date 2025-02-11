using FlashcardsLiblary.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;

namespace FlashcardsRepository
{
    public class DbSetRepository : IRepository<Set>
    {
        #region [ Fields ]

#pragma warning disable IDE0052 // Удалить непрочитанные закрытые члены
        private readonly DbContext _context;
#pragma warning restore IDE0052 // Удалить непрочитанные закрытые члены
        private readonly Func<DbContext> _contextCreator;
        private readonly DbSet<Set> _sets;

        // Создание метода для уведомления об обновлении всех свойств Set
        private static readonly ImmutableArray<Action<object, EventArgs>> allPropertiesChangedHandler;

        private static readonly PropertyChangedEventArgs args = new(string.Empty);

        #endregion [ Fields ]

        public DbSetRepository(DbContext context, Func<DbContext> contextCreator)
        {
            context.Database.EnsureCreated();
            _contextCreator = contextCreator;
            _context = _contextCreator();
            _sets = context.Set<Set>();
        }

        public DbSetRepository(Func<DbContext> contextCreator)
            : this(contextCreator(), contextCreator)
        { }

        public DbSetRepository()
            : this(() => new ApplicationDbContext())
        { }

        #region [ Methods ]

        public async Task<Set> AddAsync(Set set) => await Task.Run(() =>
        {
            // Добавление сущности через другой, одноразовый контекст БД.
            using DbContext context = _contextCreator(); // Создание одноразового контекста БД.
            DbSet<Set> sets = context.Set<Set>();
            sets.Add(set);
            context.SaveChanges();

            // Получение сущности в локальный кеш и её возврат.
            return _sets.Find(set.Id) ?? throw new NullReferenceException("Процесс добавления не был осуществлен");
        });

        public async Task DeleteAsync(int id) => await Task.Run(() =>
        {
            // Удаление сущности через другой, одноразовый контекст БД.
            using (DbContext context = _contextCreator())
            {
                DbSet<Set> sets = context.Set<Set>();
                Set set = sets.Find(id) ?? throw new NullReferenceException("Нет записи с таким ID");
                sets.Remove(set);
                context.SaveChanges();
            }
            {
                // Обновление локального кеша, если там есть сущность с таким Id.
                Set? set = _sets.Find(id);
                if (set != null)
                    _sets.Entry(set).Reload();
            }
        });

        public async Task UpdateAsync(Set set) => await Task.Run(() =>
        {
            // Обновление записи в БД через другой, одноразовый контекст БД.
            using (DbContext context = _contextCreator())
            {
                DbSet<Set> sets = context.Set<Set>();

                _ = sets.Update(set);
                _ = context.SaveChanges();
            }
            // Замена сущности в локальном кеше.
            //_sets.Local.ToObservableCollection().ReplaceOrAdd(s => s.Id == set.Id, set);
            //_sets.Local.FindEntry(set.Id)!.State = EntityState.Unchanged;

            // Обновление сущности с уведомлением привязок через PropertyDescriptor об изменении Bird.IsActive.
            // Вариант показан, как альтернативная реализация.
            // В данной задаче, на практике, более лучшим вариантом будет замена сущности.
            EntityEntry<Set>? be = _sets.Local.FindEntry(set.Id);
            be!.Reload();
            Set b = _sets.Find(set.Id) ?? throw new NullReferenceException();
            OnAllPropertiesChanged(b);
        });

        private ReadOnlyObservableCollection<Set>? setsReadOnlyObservableCollection;

        public ReadOnlyObservableCollection<Set> GetAllAsync()
        {
            setsReadOnlyObservableCollection ??= new(_sets.Local.ToObservableCollection());
            return setsReadOnlyObservableCollection;
        }

        static DbSetRepository()
        {
            var onValueChanged = typeof(PropertyDescriptor)
                .GetMethod($"OnValueChanged", BindingFlags.Instance | BindingFlags.NonPublic)!;
            allPropertiesChangedHandler = TypeDescriptor.GetProperties(typeof(Set))
                .Cast<PropertyDescriptor>()
                .Select(pr => onValueChanged.CreateDelegate<Action<object, EventArgs>>(pr))
                .ToImmutableArray();
        }

        // Метод обновляющий все привязки к Set.
        private static void OnAllPropertiesChanged(Set s)
        {
            foreach (var p in allPropertiesChangedHandler)
            {
                p(s, args);
            }
        }

        public async Task LoadAsync() => await Task.Run(_sets.Load);

        #endregion [ Methods ]
    }
}