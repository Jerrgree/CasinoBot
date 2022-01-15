﻿using Discord;
using Discord.Interactions;
using Discord.WebSocket;
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

        public DiscordClient(string token, IServiceProvider serviceProvider, ulong? debugGuildId = null)
        {
            if (string.IsNullOrWhiteSpace(token)) throw new ArgumentNullException(nameof(token));
            _token = token;
            //_serviceProvider = serviceProvider;
            _debugGuildId = debugGuildId;

            _client = new DiscordSocketClient();
            _serviceProvider = new ServiceCollection()
            .AddSingleton(x => new InteractionService(_client.Rest))
            .BuildServiceProvider();

            _interactionService = new InteractionService(_client.Rest);

            _client.Ready += OnClientReady;
            _client.InteractionCreated += HandleInteraction;
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

        private async Task HandleInteraction(SocketInteraction arg)
        {
            try
            {
                // Create an execution context that matches the generic type parameter of your InteractionModuleBase<T> modules
                var ctx = new SocketInteractionContext(_client, arg);
                await _interactionService.ExecuteCommandAsync(ctx, _serviceProvider);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                // If a Slash Command execution fails it is most likely that the original interaction acknowledgement will persist. It is a good idea to delete the original
                // response, or at least let the user know that something went wrong during the command execution.
                if (arg.Type == InteractionType.ApplicationCommand)
                    await arg.GetOriginalResponseAsync().ContinueWith(async (msg) => await msg.Result.DeleteAsync());
            }
        }


        private async Task OnClientReady()
        {
            try
            {
#if DEBUG
                var result = await new InteractionService(_client.Rest).RegisterCommandsToGuildAsync(_debugGuildId.Value, true);
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