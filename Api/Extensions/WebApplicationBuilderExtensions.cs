using System.Reflection;
using Api.Services.Order;
using Api.Services.Product;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace Api.Extensions;

internal static class WebApplicationBuilderExtensions
{
    internal static void RegisterGeneralServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddControllers();

        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddFluentValidationClientsideAdapters();
        builder.Services.AddValidatorsFromAssembly(Assembly.GetEntryAssembly());

        builder.Services.AddMemoryCache();

        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<IOrderService, OrderService>();
    }
}