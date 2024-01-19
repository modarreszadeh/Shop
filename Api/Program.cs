using Api.Extensions;
using Data.DbContexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.RegisterGeneralServices();

builder.Services.AddDbContextPool<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("Postgres");
    ArgumentNullException.ThrowIfNull(connectionString);

    options.UseNpgsql(connectionString);
});

var app = builder.Build();

app.UseGeneralServices();

app.Run();