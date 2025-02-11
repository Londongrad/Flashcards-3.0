using FlashcardsLiblary.Repository;
using FlashcardsRepository.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace FlashcardsRepository
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Word>? Words { get; set; }
        public DbSet<Set>? Sets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var configuration = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json")
            //    .Build();

            //var connectionString = configuration.GetConnectionString("DBTest");
            
            optionsBuilder
                .UseSqlite($"Data Source=flashcards.db");

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