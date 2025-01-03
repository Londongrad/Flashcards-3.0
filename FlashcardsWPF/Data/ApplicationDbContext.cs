using Flashcards.Configurations;
using Flashcards.Models;
using Microsoft.EntityFrameworkCore;

namespace Flashcards.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<WordEntity>? Words { get; set; }
        public DbSet<SetEntity>? Sets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SetConfiguration());
            modelBuilder.ApplyConfiguration(new WordConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
