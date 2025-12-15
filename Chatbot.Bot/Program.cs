// See https://aka.ms/new-console-template for more information
using Chatbot.Bot.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Chatbot.Infrastructure.Data;
using Chatbot.Core.Interface;
using Chatbot.Core.Services;
using Chatbot.Bot.Options;
using Microsoft.Extensions.Configuration;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHttpClient();
        services.AddAppDbContext(hostContext.Configuration);
        services.AddSingleton<IRabbitMqService, RabbitMqService>();

        services.Configure<StockApiOptions>(hostContext.Configuration.GetRequiredSection(StockApiOptions.STOCK_API_OPTIONS));

        services.AddHostedService<BotRequestBackgroundService>();
    })
    .Build();

await host.StartAsync();
await host.WaitForShutdownAsync();