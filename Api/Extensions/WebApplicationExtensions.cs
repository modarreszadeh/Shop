namespace Api.Extensions;

public static class WebApplicationExtensions
{
    public static void UseGeneralServices(this WebApplication app)
    {
        app.UseHttpsRedirection();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.MapControllers();
    }
}