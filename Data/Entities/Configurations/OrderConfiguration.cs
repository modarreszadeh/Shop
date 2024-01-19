using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Entities.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(x => x.CreationDate)
            .HasDefaultValueSql("NOW()")
            .IsRequired();

        builder.HasOne(x => x.Product)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.ProductId);

        builder.HasOne(x => x.Buyer)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.UserId);
    }
}