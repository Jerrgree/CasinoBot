using CasinoBot.Domain.Interfaces;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

        public DiscordClient(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            var discordSettings = configuration.GetSection("DiscordConfig");
            if (!discordSettings.Exists()) throw new ArgumentException("There is no DiscordConfig section of the provided configuration", nameof(configuration));

            _ = ulong.TryParse(discordSettings["debugGuildId"], out ulong debugGuildId);
            _token = discordSettings["token"];

            if (string.IsNullOrWhiteSpace(_token)) throw new InvalidOperationException("No token could be located in the provided configuration");
            _serviceProvider = serviceProvider;
            _debugGuildId = debugGuildId;

            _client = new DiscordSocketClient();
            _interactionService = new InteractionService(_client.Rest);

            _client.Ready += OnClientReady;
            _client.InteractionCreated += OnInteractionCreated;

            // Process the command execution results 
            _interactionService.SlashCommandExecuted += OnSlashCommandCompleted;
            _interactionService.ContextCommandExecuted += OnContextCommandCompleted;
            _interactionService.ComponentCommandExecuted += OnComponentCommandExecuted;
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

        #region Interaction bases
        private async Task OnComponentCommandExecuted(ComponentCommandInfo arg1, IInteractionContext arg2, IResult arg3)
        {
            if (!arg3.IsSuccess)
            {
                using var scope = _serviceProvider.CreateScope();
                var loggingService = scope.ServiceProvider.GetRequiredService<ILoggingService>();
                await loggingService.LogErrorMessage($"Component Command Resulted in error {arg3.Error}: {arg3.ErrorReason}");

                if (!arg2.Interaction.HasResponded)
                {
                    await arg2.Interaction.RespondAsync("Your command has resulted in an error");
                }
            }
        }

        private async Task OnContextCommandCompleted(ContextCommandInfo arg1, IInteractionContext arg2, IResult arg3)
        {
            if (!arg3.IsSuccess)
            {
                using var scope = _serviceProvider.CreateScope();
                var loggingService = scope.ServiceProvider.GetRequiredService<ILoggingService>();
                await loggingService.LogErrorMessage($"Context Command Resulted in error {arg3.Error}: {arg3.ErrorReason}");

                if (!arg2.Interaction.HasResponded)
                {
                    await arg2.Interaction.RespondAsync("Your command has resulted in an error");
                }
            }
        }

        private async Task OnSlashCommandCompleted(SlashCommandInfo arg1, IInteractionContext arg2, IResult arg3)
        {
            if (!arg3.IsSuccess)
            {
                using var scope = _serviceProvider.CreateScope();
                var loggingService = scope.ServiceProvider.GetRequiredService<ILoggingService>();
                await loggingService.LogErrorMessage($"Slash Command Resulted in error {arg3.Error}: {arg3.ErrorReason}");

                if (!arg2.Interaction.HasResponded)
                {
                    await arg2.Interaction.RespondAsync("Your command has resulted in an error");
                }
            }
        }

        private async Task OnInteractionCreated(SocketInteraction arg)
        {
            try
            {
                // Create an execution context that matches the generic type parameter of your InteractionModuleBase<T> modules
                var ctx = new SocketInteractionContext(_client, arg);
                await _interactionService.ExecuteCommandAsync(ctx, _serviceProvider);
            }
            catch (Exception ex)
            {
                using var scope = _serviceProvider.CreateScope();
                var loggingService = scope.ServiceProvider.GetRequiredService<ILoggingService>();
                await loggingService.LogErrorMessage($"Exception ocurred while executing interaction: {ex}");

                // If a Slash Command execution fails it is most likely that the original interaction acknowledgement will persist. It is a good idea to delete the original
                // response, or at least let the user know that something went wrong during the command execution.
                if (arg.Type == InteractionType.ApplicationCommand)
                    await arg.GetOriginalResponseAsync().ContinueWith(async (msg) => await msg.Result.DeleteAsync());
            }
        }

        #endregion

        private async Task OnClientReady()
        {
            using var scope = _serviceProvider.CreateScope();
            var loggingService = scope.ServiceProvider.GetRequiredService<ILoggingService>();
            try
            {
#if DEBUG
                var result = await _interactionService.RegisterCommandsToGuildAsync(_debugGuildId.Value, true);
                // Global interactions can take up to an hour to update, use register to guild for a specific guild
#else
            await _interactionService.RegisterCommandsGloballyAsync(true); // If ever splitting debug/vs prod, use RegisterCommandsToGuildAsync to move debug features;
#endif
            }
            catch (Exception ex)
            {
                await loggingService.LogFatalMessage($"Failed to registed commands: {ex}");
            }
            finally
            {
                await loggingService.LogInformationalMessage($"Bot is ready");
                Console.WriteLine("Bot is ready"); // Go ahead and keep this going to the console despite the logging implementation for dev purposes
            }
        }

    }
}