# Shop

## Build and Run:

Install `posgreSQL` and `dotnet 8.0` in your machine

Change PostgreSQL `ConnectionStrings` in [appsettings.json](Api/appsettings.json)

on root directory run:

`dotnet ef database update -p Data -s Api -c AppDbContext`

on Api directory run `dotnet run`