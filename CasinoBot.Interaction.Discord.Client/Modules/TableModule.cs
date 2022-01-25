using CasinoBot.Domain.Interfaces;
using Discord;
using Discord.Interactions;
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
                .WithTitle("Tables");

            var tables = getTablesResponse.Value;
            if (tables is null || !tables.Any())
            {
                embed.WithDescription("There are no tables in this server yet,");
            }
            else
            {
                // TODO: add in context buttons for navigating available tables
                // Also TODO: learn how context buttons work
            }

            await FollowupAsync(embed: embed.Build());
        }
    }
}
