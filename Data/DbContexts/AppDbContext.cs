using Data.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Data.DbContexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var entitiesAssembly = typeof(IEntity).Assembly;

        modelBuilder.RegisterAllEntities<IEntity>(entitiesAssembly);
        modelBuilder.RegisterEntityTypeConfiguration(entitiesAssembly);
        modelBuilder.SeedDatabase();
    }
}