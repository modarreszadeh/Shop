using Microsoft.EntityFrameworkCore;

namespace Api.Extensions;

public static class HostBuilderExtensions
{
    public static WebApplication MigrateDatabase<TContext>(this IHost host, Action<TContext, IServiceProvider> seeder)
        where TContext : DbContext
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;

        var hostEnvironment = services.GetService<IWebHostEnvironment>();
        if (hostEnvironment!.IsDevelopment())
            return (host as WebApplication)!;

        var logger = services.GetRequiredService<ILogger>();
        var context = services.GetService<TContext>();


        try
        {
            logger.LogInformation("Migrating database associated with context {DbContextName}", typeof(TContext).Name);

            InvokeSeeder(seeder!, context, services);

            logger.LogInformation("Migrated database associated with context {DbContextName}", typeof(TContext).Name);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while migrating the database used on context {DbContextName}",
                typeof(TContext).Name);
        }

        return (host as WebApplication)!;
    }

    private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder, TContext context,
        IServiceProvider services) where TContext : DbContext?
    {
        context?.Database.Migrate();
        seeder(context, services);
    }
}