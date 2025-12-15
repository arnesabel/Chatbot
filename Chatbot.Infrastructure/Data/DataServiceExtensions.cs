using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Chatbot.Infrastructure.Data;

public static class DataServiceExtensions
{
    public static IServiceCollection AddAppDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetDatabaseConnectionString()
                ?? throw new InvalidOperationException($"Connection string '{AppDbContext.CONNECTION_STRING_NAME}' not found.");

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        return services;
    }

    private static string? GetDatabaseConnectionString(this IConfiguration configuration)
    {
        var database = configuration["DBDATABASE"];
        var host = configuration["DBHOST"];
        var password = configuration["DBPASSWORD"];
        var port = configuration["DBPORT"];
        var user = configuration["DBUSER"];

        var connectionString = string.IsNullOrEmpty(database)
               || string.IsNullOrEmpty(host)
               || string.IsNullOrEmpty(password)
               || string.IsNullOrEmpty(port)
               || string.IsNullOrEmpty(user)
            ? configuration.GetConnectionString(AppDbContext.CONNECTION_STRING_NAME)
            :    $"Server={host},{port};" +
                $"Database={database};" +
                $"User Id={user};" +
                $"Password={password};" +
                $"Encrypt=True;" +
                $"TrustServerCertificate=True;";

        return connectionString;

    }
}
