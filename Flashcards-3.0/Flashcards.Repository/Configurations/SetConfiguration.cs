using Flashcards.Liblary.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Flashcards.Repository.Configurations
{
    internal class SetConfiguration : IEntityTypeConfiguration<Set>
    {
        public void Configure(EntityTypeBuilder<Set> builder)
        {
            builder.HasKey(c => c.Id);

            builder
                .HasMany<Word>()
                .WithOne()
                .HasForeignKey(w => w.SetId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(c => c.Name).IsUnique();

            builder.Property(c => c.Name)
                .IsRequired().HasMaxLength(20);
        }
    }
}