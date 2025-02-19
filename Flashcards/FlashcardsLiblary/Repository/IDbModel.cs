namespace FlashcardsLiblary.Repository
{
    public interface IDbModel
    {
        public IRepository<Word> Words { get; }
        public IRepository<Set> Sets { get; }

        Task LoadAsync();
    }
}