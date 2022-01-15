using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using System.Reflection;

namespace CasinoBot.Interaction.Discord.Client
{
    public class DiscordClient : IDisposable
    {
        private readonly string _token;
        private readonly DiscordSocketClient _client;
        private readonly InteractionService _interactionService;
        private readonly IServiceProvider _serviceProvider;
        private readonly ulong? _debugGuildId;

        public DiscordClient(string token, IServiceProvider serviceProvider, ulong? debugGuildId = null)
        {
            if (string.IsNullOrWhiteSpace(token)) throw new ArgumentNullException(nameof(token));
            _token = token;
            _serviceProvider = serviceProvider;
            _debugGuildId = debugGuildId;

            _client = new DiscordSocketClient();
            _interactionService = new InteractionService(_client.Rest);

            _client.Ready += async () => await OnClientReady();
        }

        public async Task Connect()
        {
            await _client.LoginAsync(TokenType.Bot, _token);
            await _interactionService.AddModulesAsync(Assembly.GetExecutingAssembly(), _serviceProvider);
        }

        public async Task Start()
        {
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

        private async Task OnClientReady()
        {
            try
            {
#if DEBUG
                await _interactionService.RegisterCommandsToGuildAsync(_debugGuildId.Value, true);
                // Global interactions can take up to an hour to update, use register to guild for a specific guild
#else
            await _interactionService.RegisterCommandsGloballyAsync(); // If ever splitting debug/vs prod, use RegisterCommandsToGuildAsync to move debug features;
#endif
            }   
            catch (Exception ex)
            {

            }
        }

    }
}