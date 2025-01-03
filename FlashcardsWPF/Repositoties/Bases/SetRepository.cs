using Flashcards.Data;
using Flashcards.Models;
using Flashcards.Repositoties.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Flashcards.Repositoties.Bases
{
    public class SetRepository(DesignTimeDbContextFactory contextFactory) : ISetRepository
    {
        #region [ Fields ]
        private readonly DesignTimeDbContextFactory _contextFactory = contextFactory;
        #endregion

        #region [ Methods ]
        public async Task<SetEntity> AddAsync(SetEntity set)
        {
            using var _context = _contextFactory.CreateDbContext();
            var result = await _context.Sets!.AddAsync(set);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
        public async Task DeleteAsync(string name)
        {
            using var _context = _contextFactory.CreateDbContext();
            await _context.Sets!
            .Where(c => c.Name == name)
            .ExecuteDeleteAsync();
        }
        public async Task<IEnumerable<SetEntity>> GetAllAsync()
        {
            using var _context = _contextFactory.CreateDbContext();
            return await _context.Sets!.Include(s => s.Words).AsNoTracking().ToListAsync();
        }
        public async Task UpdateAsync(SetEntity set)
        {
            using var _context = _contextFactory.CreateDbContext();
            await _context.Sets!
                .Where(c => c.Id == set.Id)
                .ExecuteUpdateAsync(sp => sp
                    .SetProperty(c => c.Name, set.Name));
        }
        #endregion
    }
}