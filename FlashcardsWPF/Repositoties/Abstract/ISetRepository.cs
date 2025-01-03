using Flashcards.Models;

namespace Flashcards.Repositoties.Abstract
{
    public interface ISetRepository
    {
        Task<SetEntity> AddAsync(SetEntity set);
        Task DeleteAsync(string name);
        Task<IEnumerable<SetEntity>> GetAllAsync();
        Task UpdateAsync(SetEntity set);
    }
}
