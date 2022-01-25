using CasinoBot.Data;
using CasinoBot.Domain.Interfaces;
using CasinoBot.Interaction.Discord.Client;
using CasinoBot.Logging.SqlLoggingService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("connectionStrings.json")
                .AddJsonFile("discordSettings.json")
                .Build();

var serviceProvider = new ServiceCollection()
    .AddTransient<IConfiguration>(_ => config)
    .AddSingleton<DiscordClient>()
    .AddDbContext<CasinoContext>(options => options.UseSqlServer(config.GetConnectionString("database")))
    .AddScoped<ILoggingService, SqlLoggingService>()
    .BuildServiceProvider();

using var scope = serviceProvider.CreateScope();
DiscordClient? discordClient = null;
try
{
    discordClient = scope.ServiceProvider.GetRequiredService<DiscordClient>();
    await discordClient.Connect();

    await discordClient.Start();

    var endTime = DateTime.UtcNow.AddMinutes(20);

    do
    {
        await Task.Delay(1000);
    } while (DateTime.UtcNow < endTime);

}
catch (Exception ex)
{
    Console.WriteLine($"Exception Encountered: {ex}");
}
finally
{
    if (discordClient != null)
    {
        await discordClient.Stop();
    }
}
