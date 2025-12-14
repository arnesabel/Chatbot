using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;


namespace Chatbot.Infrastructure.Data;

public class AppDbContextDesignTimeFactory
    : IDesignTimeDbContextFactory<AppDbContext>
{
    private const string ASP_NET_CORE_ENVIRONMENT = "ASPNETCORE_ENVIRONMENT";
    private const string DEFAULT_ENVIRONMENT = "ASPNETCORE_ENVIRONMENT";

    public AppDbContext CreateDbContext(string[] args)
    {
        var basePath = Directory.GetCurrentDirectory();
        return Create(basePath, Environment.GetEnvironmentVariable(ASP_NET_CORE_ENVIRONMENT) ?? DEFAULT_ENVIRONMENT);
    }

    private AppDbContext Create(string basePath, string environmentName)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
            .Build();

        var readWriteConnectionString = configuration.GetConnectionString(AppDbContext.CONNECTION_STRING_NAME);

        return Create(readWriteConnectionString);
    }

    private AppDbContext Create(string? readWriteConnectionString)
    {
        if (string.IsNullOrWhiteSpace(readWriteConnectionString))
        {
            throw new ArgumentException($"Connection string is null or empty.", nameof(readWriteConnectionString));
        }

        var assemblyName = typeof(AppDbContext).Assembly.GetName().Name;

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        optionsBuilder.UseSqlServer(readWriteConnectionString, o =>
        {
            o.MigrationsAssembly(assemblyName);
            o.CommandTimeout(1800);
        });

        return new AppDbContext(optionsBuilder.Options);
    }
}
