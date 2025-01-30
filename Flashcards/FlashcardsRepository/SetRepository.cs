using FlashcardsLiblary.Repository;
using Microsoft.EntityFrameworkCore;

namespace FlashcardsRepository
{
    public class SetRepository(ApplicationDbContext context)
    {
        #region [ Methods ]
        public async Task<Set> AddAsync(Set set)
        {
            var result = await context.Sets!.AddAsync(set);
            await context.SaveChangesAsync();
            return result.Entity;
        }
        public async Task DeleteAsync(string name)
        {
            await context.Sets!
            .Where(c => c.Name == name)
            .ExecuteDeleteAsync();
        }
        public async Task<IEnumerable<Set>> GetAllAsync()
        {
            return await context.Sets!.Include(s => s.Words).AsNoTracking().ToListAsync();
        }
        public async Task UpdateAsync(Set set)
        {
            await context.Sets!
                .Where(c => c.Id == set.Id)
                .ExecuteUpdateAsync(sp => sp
                    .SetProperty(c => c.Name, set.Name));
        }
        #endregion
    }
}