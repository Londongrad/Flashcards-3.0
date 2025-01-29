using Flashcards.Data;
using Flashcards.Models;
using Flashcards.Repositoties.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Flashcards.Repositoties.Bases
{
    public class WordRepository(DesignTimeDbContextFactory contextFactory) : IWordRepository
    {
        #region [ Fields ]
        private readonly DesignTimeDbContextFactory _contextFactory = contextFactory;
        #endregion

        #region [ Methods ]
        public async Task AddAsync(WordEntity word)
        {
            using var _context = _contextFactory.CreateDbContext();
            await _context.Words!.AddAsync(word);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(string name)
        {
            using var _context = _contextFactory.CreateDbContext();
            await _context.Words!
                .Where(o => o.Name == name)
                .ExecuteDeleteAsync();
        }
        public async Task UpdateAsync(WordEntity word)
        {
            using var _context = _contextFactory.CreateDbContext();
            await _context.Words!
               .Where(o => o.Id == word.Id)
               .ExecuteUpdateAsync(sp => sp
                   .SetProperty(c => c.Name, word.Name)
                   .SetProperty(c => c.Definition, word.Definition)
                   .SetProperty(c => c.ImagePath, word.ImagePath)
                   .SetProperty(c => c.IsLastWord, word.IsLastWord)
                   .SetProperty(c => c.IsFavorite, word.IsFavorite));
        }
        public async Task<bool> CheckWordIfExistsAsync(string name)
        {
            using var _context = _contextFactory.CreateDbContext();
            return await _context.Words!.FirstOrDefaultAsync(o => o.Name == name) is not null;
        }
        #endregion
    }
}
