using CasinoBot.Domain.Interfaces;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using System.Linq;

namespace CasinoBot.Interaction.Discord.Client.Modules
{
    public class TableModule : CasinoBotInteractionModuleBase
    {
        private readonly IGameDataStore _gameDataStore;
        public TableModule(ILoggingService loggingService,
            IGameDataStore gameDataStore) : base(loggingService) 
        {
            _gameDataStore = gameDataStore;
        }

        [SlashCommand("tables", "View tables in this server")]
        public async Task Tables()
        {
            await DeferAsync();

            if (Context.Guild is null)
            {
                await FollowupAsync("This action can only be performed within a server");
                return;
            }

            var getTablesResponse = await _gameDataStore.GetTablesByGuild(Context.Guild.Id);

            if (!getTablesResponse.IsSuccessful)
            {
                await FollowupAsync("Your command has resulted in an error");
                return;
            }

            var embed = new EmbedBuilder()
                .WithTitle("Tables 1");

            var tables = getTablesResponse.Value;
            if (tables is null || !tables.Any())
            {
                embed.WithDescription("There are no tables in this server yet");
            }
            else
            {
                // TODO: add in context buttons for navigating available tables
                // Also TODO: learn how context buttons work
            }

            var components = new ComponentBuilder()
                .WithButton("test", "testId:test");

            await FollowupAsync(components: components.Build(), embed: embed.Build());
        }

        [ComponentInteraction("testId:*")]
        public async Task NextTable()
        {
            if (Context.Interaction is SocketMessageComponent socketMessage)
            {
                var message = socketMessage.Message;
                var x = int.Parse(message.Embeds.First().Title.Split(' ').Last()) + 1;

                var embed = new EmbedBuilder()
                    .WithTitle($"Tables {x}");

                await message.ModifyAsync(m => m.Embed = embed.Build());
                await RespondAsync();
            }
            else
            {
                await _loggingService.LogErrorMessage($"Unkown interaction type {Context.Interaction.GetType().Name} encountered in {nameof(NextTable)}");
            }
        }

    }
}
