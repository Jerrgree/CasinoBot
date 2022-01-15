using CasinoBot.Interaction.Discord.Client;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("discordSettings.json").Build();

var discordSettings = config.GetSection("DiscordConfig");

var discordClient = new DiscordClient(discordSettings["token"]);

try
{
    await discordClient.Connect();

    await discordClient.Start();

    var endTime = DateTime.UtcNow.AddMinutes(2);

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
