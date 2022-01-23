using CasinoBot.Domain.Enums;
using CasinoBot.Domain.Models;
using CasinoBot.Domain.Models.Players;
using CasinoBot.Domain.Models.Tables;

namespace CasinoBot.Domain.Interfaces
{
    public interface IGameDataStore
    {
        /// <summary>
        /// Creates a table in the guild
        /// </summary>
        /// <param name="guildId">The guild that the table is associated with</param>
        /// <param name="tableType">The type of game that the table plays</param>
        /// <returns></returns>
        Task<Response> CreateTable(ulong guildId, TableType tableType);
        /// <summary>
        /// Deletes the associated table
        /// </summary>
        /// <param name="tableId">The table to delete</param>
        /// <returns></returns>
        Task<Response> DeleteTable(long tableId);
        /// <summary>
        /// Retrieves the tables in the guild
        /// </summary>
        /// <param name="guildId">The guild id</param>
        /// <returns></returns>
        Task<Response<IEnumerable<Table>?>> GetTablesByGuild(ulong guildId);
        /// <summary>
        /// Retrieves tables that a player is playing in
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        Task<Response<IEnumerable<Table>?>> GetTablesByPlayer(ulong playerId);
        /// <summary>
        /// Adds a player to the table
        /// </summary>
        /// <typeparam name="T">The type of state that the player uses</typeparam>
        /// <param name="tableId">The id of the table to add to</param>
        /// <param name="playerId">The id of the player to add</param>
        /// <param name="playerState">The current state of the player</param>
        /// <returns></returns>
        Task<Response> AddPlayerToTable<T>(long tableId, ulong playerId, T playerState) where T : class;
        /// <summary>
        /// Updates an existing player at a table
        /// </summary>
        /// <typeparam name="T">The type of state that the player uses</typeparam>
        /// <param name="tableId">The id of the table that the player is at</param>
        /// <param name="playerId">The id of the player to update</param>
        /// <param name="playerState">The current state of the player</param>
        /// <returns></returns>
        Task<Response> UpdatePlayer<T>(long tableId, ulong playerId, T playerState) where T : class;
        /// <summary>
        /// Retrieves the players at a given table
        /// </summary>
        /// <typeparam name="T">The type of state that the players are using</typeparam>
        /// <param name="tableId">The id of the table</param>
        /// <returns></returns>
        Task<Response<IEnumerable<Player<T>>?>> GetPlayersByTable<T>(ulong tableId) where T : class;
    }
}
