using CasinoBot.Domain.Enums;
using CasinoBot.Domain.Interfaces;
using CasinoBot.Domain.Models.Tables;
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

            var tables = getTablesResponse.Value!;

            (var embed, var components) = GetTablesContent(0, tables);

            await FollowupAsync(components: components.Build(), embed: embed.Build());
        }

        [SlashCommand("create", "Add a table to this server")]
        public async Task AddTable([Summary(description: "The type of game hosted at this table")]TableType tableType)
        {
            await DeferAsync();

            if (Context.Guild is null)
            {
                await FollowupAsync("This action can only be performed within a server");
                return;
            }

            await _gameDataStore.CreateTable(Context.Guild.Id, tableType);

            await FollowupAsync($"Succesfully added a {tableType} table!");
        }

        #endregion

        #region Components

        [ComponentInteraction("nextTable:*")]
        public async Task NextTable(string tableId)
        {
            if (Context.Interaction is SocketMessageComponent socketMessage)
            {
                var indexOfCurrentTable = int.Parse(tableId);
                var message = socketMessage.Message;

                var lastActivity = message.EditedTimestamp ?? message.CreatedAt;

                var diff = DateTimeOffset.Now.Subtract(lastActivity).Ticks;

                // TODO: ideally, find a way to remove these buttons entirely after 5 minutes
                if (diff >= (TimeSpan.TicksPerMinute * 5))
                {
                    await RespondAsync("This message cannot be modifed after 5 minutes.", ephemeral: true);
                    return;
                }

                var getTablesResponse = await _gameDataStore.GetTablesByGuild(Context.Guild.Id);

                await DeferAsync();

                if (!getTablesResponse.IsSuccessful)
                {
                    await FollowupAsync("Your command has resulted in an error");
                    return;
                }

                var tables = getTablesResponse.Value!;
                var count = tables.Count;
                var nextIndex = (indexOfCurrentTable + 1) % count;

                (var embed, var components) = GetTablesContent(nextIndex, tables);

                await message.ModifyAsync(m =>
                {
                    m.Embed = embed.Build();
                    m.Components = components.Build();
                });

                await FollowupAsync();
            }
            else
            {
                await _loggingService.LogErrorMessage($"Unkown interaction type {Context.Interaction.GetType().Name} encountered in {nameof(NextTable)}");
            }
        }

        [ComponentInteraction("prevTable:*")]
        public async Task PrevTable(string tableId)
        {
            if (Context.Interaction is SocketMessageComponent socketMessage)
            {
                var indexOfCurrentTable = int.Parse(tableId);
                var message = socketMessage.Message;

                var lastActivity = message.EditedTimestamp ?? message.CreatedAt;

                var diff = DateTimeOffset.Now.Subtract(lastActivity).Ticks;

                // TODO: ideally, find a way to remove these buttons entirely after 5 minutes
                if (diff >= (TimeSpan.TicksPerMinute * 5))
                {
                    await RespondAsync("This message cannot be modifed after 5 minutes.", ephemeral: true);
                    return;
                }
                await DeferAsync();

                var getTablesResponse = await _gameDataStore.GetTablesByGuild(Context.Guild.Id);

                if (!getTablesResponse.IsSuccessful)
                {
                    await FollowupAsync("Your command has resulted in an error");
                    return;
                }

                var tables = getTablesResponse.Value!;
                var count = tables.Count;
                var prevIndex = indexOfCurrentTable == 0 ? count - 1 : indexOfCurrentTable - 1;

                (var embed, var components) = GetTablesContent(prevIndex, tables);

                await message.ModifyAsync(m =>
                {
                    m.Embed = embed.Build();
                    m.Components = components.Build();
                });
                await FollowupAsync();
            }
            else
            {
                await _loggingService.LogErrorMessage($"Unkown interaction type {Context.Interaction.GetType().Name} encountered in {nameof(NextTable)}");
            }
        }

        #endregion

        #region Helpers

        private static (EmbedBuilder embed, ComponentBuilder componentBuilder) GetTablesContent(int indexOfTableToShow, IList<Table> tables)
        {
            var embed = new EmbedBuilder()
                .WithTitle("Tables");

            var components = new ComponentBuilder();

            if (!tables.Any())
            {
                embed.WithDescription("There are no tables in this server yet");
            }
            else
            {
                var count = tables.Count;
                var table = tables[indexOfTableToShow];
                embed.WithDescription($"{table.TableType} {table.TableId}");

                if (count > 1)
                {
                    components.WithButton(emote: _prevButton, customId: $"prevTable:{indexOfTableToShow}");
                    components.WithButton(emote: _nextButton, customId: $"nextTable:{indexOfTableToShow}");
                }
            }

            return (embed, components);
        }

        #endregion

    }
}
