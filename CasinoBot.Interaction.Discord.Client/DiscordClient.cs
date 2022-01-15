using Discord;
using Discord.WebSocket;

namespace CasinoBot.Interaction.Discord.Client
{
    public class DiscordClient : IDisposable
    {
        private readonly string _token;
        private DiscordSocketClient _client;

        public DiscordClient(string token)
        {
            if (string.IsNullOrWhiteSpace(token)) throw new ArgumentNullException(nameof(token));
            _token = token;

            _client = new DiscordSocketClient();
        }

        public async Task Connect()
        {
            await _client.LoginAsync(TokenType.Bot, _token);
        }

        public async Task Start()
        {
            if (_client.Status == UserStatus.Offline) throw new InvalidOperationException("Bot is not online");
            await _client.StartAsync();
        }

        public async Task Stop()
        {
            await _client.StopAsync();
        }

        public void Dispose()
        {
            _client?.Dispose();
        }

    }
}