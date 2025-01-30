using FlashcardsLiblary.Repository;
using Microsoft.EntityFrameworkCore;

namespace FlashcardsRepository
{
    public class WordRepository(ApplicationDbContext context)
    {
        #region [ Methods ]
        public async Task AddAsync(Word word)
        {
            await context.Words!.AddAsync(word);
            await context.SaveChangesAsync();
        }
        public async Task DeleteAsync(string name)
        {
            await context.Words!
                .Where(o => o.Name == name)
                .ExecuteDeleteAsync();
        }
        public async Task UpdateAsync(Word word)
        {
            await context.Words!
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
            return await context.Words!.FirstOrDefaultAsync(o => o.Name == name) is not null;
        }
        #endregion
    }
}
