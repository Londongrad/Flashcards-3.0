using FlashcardsLiblary;
using FlashcardsLiblary.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;

namespace FlashcardsRepository
{
    public class DbWordRepository : IRepository<Word>
    {
        #region [ Fields ]

#pragma warning disable IDE0052 // Удалить непрочитанные закрытые члены
        private readonly DbContext _context;
#pragma warning restore IDE0052 // Удалить непрочитанные закрытые члены
        private readonly Func<DbContext> _contextCreator;
        private readonly DbSet<Word> _words;
        private static readonly ImmutableArray<Action<object, EventArgs>> allPropertiesChangedHandler;
        private static readonly PropertyChangedEventArgs args = new(string.Empty);

        #endregion [ Fields ]

        public DbWordRepository(DbContext context, Func<DbContext> contextCreator)
        {
            _context = context;
            _contextCreator = contextCreator;
            _words = context.Set<Word>();
        }

        public DbWordRepository(Func<DbContext> contextCreator)
            : this(contextCreator(), contextCreator)
        { }

        public DbWordRepository()
            : this(() => new ApplicationDbContext())
        { }

        #region [ Methods ]

        public async Task<Word> AddAsync(Word word) => await Task.Run(() =>
        {
            // Добавление сущности через другой, одноразовый контекст БД.
            using DbContext context = _contextCreator(); // Создание одноразового контекста БД.
            DbSet<Word> words = context.Set<Word>();
            words.Add(word);
            context.SaveChanges();

            // Получение сущности в локальный кеш и её возврат.
            return _words.Find(word.Id) ?? throw new NullReferenceException("Процесс добавления не был осуществлен");
        });

        public async Task DeleteAsync(int id) => await Task.Run(() =>
        {
            // Удаление сущности через другой, одноразовый контекст БД.
            using (DbContext context = _contextCreator())
            {
                DbSet<Word> words = context.Set<Word>();
                Word word = words.Find(id) ?? throw new NullReferenceException("Нет записи с таким ID");
                words.Remove(word);
                context.SaveChanges();
            }
            {
                // Обновление локального кеша, если там есть сущность с таким Id.
                Word? word = _words.Find(id);
                if (word != null)
                    _words.Entry(word).Reload();
            }
        });

        public async Task UpdateAsync(Word word) => await Task.Run(() =>
        {
            // Обновление записи в БД через другой, одноразовый контекст БД.
            using (DbContext context = _contextCreator())
            {
                DbSet<Word> words = context.Set<Word>();

                words.Update(word);
                context.SaveChanges();
            }
            // Замена сущности в локальном кеше.
            _words.Local.ToObservableCollection().ReplaceOrAdd(s => s.Id == word.Id, word);
            _words.Local.FindEntry(word.Id)!.State = EntityState.Unchanged;

            // Обновление сущности с уведомлением привязок через PropertyDescriptor об изменении Bird.IsActive.
            // Вариант показан, как альтернативная реализация.
            // В данной задаче, на практике, более лучшим вариантом будет замена сущности.
            //EntityEntry<Word>? be = _words.Local.FindEntry(word.Id);
            //be!.Reload();
            //Word w = _words.Find(word.Id) ?? throw new NullReferenceException();
            //OnAllPropertiesChanged(w);
        });

        private ReadOnlyObservableCollection<Word>? wordsReadOnlyObservableCollection;

        public ReadOnlyObservableCollection<Word> GetAllAsync()
        {
            wordsReadOnlyObservableCollection ??= new(_words.Local.ToObservableCollection());
            return wordsReadOnlyObservableCollection;
        }

        static DbWordRepository()
        {
            var onValueChanged = typeof(PropertyDescriptor)
                .GetMethod($"OnValueChanged", BindingFlags.Instance | BindingFlags.NonPublic)!;
            allPropertiesChangedHandler = TypeDescriptor.GetProperties(typeof(Word))
                .Cast<PropertyDescriptor>()
                .Select(pr => onValueChanged.CreateDelegate<Action<object, EventArgs>>(pr))
                .ToImmutableArray();
        }

        // Метод обновляющий все привязки к Set.
        private static void OnAllPropertiesChanged(Word s)
        {
            foreach (var p in allPropertiesChangedHandler)
            {
                p(s, args);
            }
        }

        public async Task LoadAsync() => await Task.Run(_words.Load);

        #endregion [ Methods ]
    }
}