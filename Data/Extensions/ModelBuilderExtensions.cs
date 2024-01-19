using System.Reflection;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Extensions;

public static class ModelBuilderExtensions
{
    /// <summary>
    /// Dynamic load all IEntityTypeConfiguration with Reflection
    /// </summary>
    /// <param name="modelBuilder"></param>
    /// <param name="assemblies">Assemblies contains Entities</param>
    public static void RegisterEntityTypeConfiguration(this ModelBuilder modelBuilder, params Assembly[] assemblies)
    {
        var applyGenericMethod = typeof(ModelBuilder).GetMethods()
            .First(m => m.Name == nameof(ModelBuilder.ApplyConfiguration));

        var types = assemblies.SelectMany(a => a.GetExportedTypes())
            .Where(c => c.IsClass && c is { IsAbstract: false, IsPublic: true });

        foreach (var type in types)
        {
            foreach (var @interface in type.GetInterfaces())
            {
                if (!@interface.IsConstructedGenericType ||
                    @interface.GetGenericTypeDefinition() != typeof(IEntityTypeConfiguration<>)) continue;
                var applyConcreteMethod =
                    applyGenericMethod.MakeGenericMethod(@interface.GenericTypeArguments[0]);
                applyConcreteMethod.Invoke(modelBuilder, [Activator.CreateInstance(type)]);
            }
        }
    }

    /// <summary>
    /// Dynamic register all Entities that inherit from specific BaseType
    /// </summary>
    /// <param name="modelBuilder"></param>
    /// <param name="assemblies">Assemblies contains Entities</param>
    public static void RegisterAllEntities<TBaseType>(this ModelBuilder modelBuilder, params Assembly[] assemblies)
    {
        var types = assemblies.SelectMany(a => a.GetExportedTypes())
            .Where(c => c.IsClass && c is { IsAbstract: false, IsPublic: true } &&
                        typeof(TBaseType).IsAssignableFrom(c));

        foreach (var type in types)
            modelBuilder.Entity(type);
    }

    public static void SeedDatabase(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(_initialUserList());
    }

    private static IReadOnlyList<User> _initialUserList()
    {
        var userList = new List<User>
        {
            new(1, "Mohammad"),
            new(2, "Ali"),
            new(3, "Sara")
        };

        return userList;
    }
}