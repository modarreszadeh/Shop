using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Entities.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(x => x.Title)
            .HasMaxLength(40)
            .IsRequired();

        builder.HasIndex(x => x.Title)
            .IsUnique();
    }
}