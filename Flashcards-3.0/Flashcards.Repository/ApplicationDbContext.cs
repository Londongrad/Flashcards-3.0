using Flashcards.Liblary.Repository;
using Flashcards.Repository.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Flashcards.Repository
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Word>? Words { get; set; }
        public DbSet<Set>? Sets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            //var connectionString = configuration.GetConnectionString("SQLServerDBMain");
            var connectionString = configuration.GetConnectionString("SQLServerDBTest");
            optionsBuilder.UseSqlServer(connectionString);

            //var connectionString = configuration.GetConnectionString("SQLite");
            //optionsBuilder.UseSqlite(connectionString);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SetConfiguration());
            modelBuilder.ApplyConfiguration(new WordConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}