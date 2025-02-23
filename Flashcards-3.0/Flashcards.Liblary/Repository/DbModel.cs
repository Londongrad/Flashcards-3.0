using Microsoft.EntityFrameworkCore;

namespace Flashcards.Liblary.Repository
{
    public class DbModel : IDbModel
    {
        private readonly DbContext context;

        public IRepository<Word> Words { get; }
        public IRepository<Set> Sets { get; }

        public Task LoadAsync() => Task.Run(() =>
        {
            context.Set<Word>().Load();
            context.Set<Set>().Load();
        });

        /// <summary>Конструктор Репозитория.</summary>
        /// <param name="context">Постоянный Контекст БД используемый для синхронизации через локальный кеш.</param>
        /// <param name="contextCreator">Функция создающая новый одноразовый DbContext из которого можно получить
        /// <see cref="DbSet{TEntity}">DbSet&lt;<typeparamref name="TId"/>&gt;</see>.</param>
        public DbModel(DbContext context, Func<DbContext> contextCreator)
        {
            //context.Database.EnsureCreated();
            this.context = context;

            Words = new DbRepository<Word>(context, contextCreator);
            Sets = new DbRepository<Set>(context, contextCreator);
        }
    }
}