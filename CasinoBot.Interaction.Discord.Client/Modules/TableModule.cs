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
        private static Emoji _nextButton = new Emoji("\U000025B6");
        private static Emoji _prevButton = new Emoji("\U000025C0");
        public TableModule(ILoggingService loggingService,
            IGameDataStore gameDataStore) : base(loggingService) 
        {
            _gameDataStore = gameDataStore;
        }
        

        #region Slash Commands

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

            var components = new ComponentBuilder();

            var tables = getTablesResponse.Value;
            if (tables is null || !tables.Any())
            {
                embed.WithDescription("There are no tables in this server yet");
            }
            else
            {
                var table = tables.First();
                var count = tables.Count();
                embed.WithDescription($"{table.TableType}");

                if (count > 1)
                {
                    // TODO: emotes instead of text
                    components.WithButton(emote: _nextButton, customId: $"nextTable:{table.TableId}");
                    components.WithButton(emote: _prevButton, customId: $"prevTable:{table.TableId}");

                }
            }
            components.WithButton(emote: _prevButton, customId: $"prevTable:{1}");
            components.WithButton(emote: _nextButton, customId: $"nextTable:{1}");

            await FollowupAsync(components: components.Build(), embed: embed.Build());
        }

        #endregion

        #region Components

        [ComponentInteraction("nextTable:*")]
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

        [ComponentInteraction("prevTable:*")]
        public async Task PrevTable()
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

        #endregion

    }
}
