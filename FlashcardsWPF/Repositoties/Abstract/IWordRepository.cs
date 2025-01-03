using Flashcards.Models;

namespace Flashcards.Repositoties.Abstract
{
    public interface IWordRepository
    {
        Task AddAsync(WordEntity word);
        Task UpdateAsync(WordEntity word);
        Task DeleteAsync(string word);
        Task<bool> CheckWordIfExistsAsync(string name);
    }
}
