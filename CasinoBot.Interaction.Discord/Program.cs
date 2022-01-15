using CasinoBot.Interaction.Discord.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("discordSettings.json").Build();

var discordSettings = config.GetSection("DiscordConfig");

_ = ulong.TryParse(discordSettings["debugGuildId"], out ulong debugGuildId);

var discordClient = new DiscordClient(discordSettings["token"], new ServiceCollection().BuildServiceProvider(), debugGuildId);

try
{
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
    await discordClient.Stop();
}
