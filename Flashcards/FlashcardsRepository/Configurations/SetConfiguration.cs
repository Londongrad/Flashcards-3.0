using FlashcardsLiblary.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlashcardsRepository.Configurations
{
    internal class SetConfiguration : IEntityTypeConfiguration<Set>
    {
        public void Configure(EntityTypeBuilder<Set> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasMany(c => c.Words)
                .WithOne(o => o.Set);

            builder.HasIndex(c => c.Name).IsUnique();

            builder.Property(c => c.Name)
                .IsRequired().HasMaxLength(20);
        }
    }
}