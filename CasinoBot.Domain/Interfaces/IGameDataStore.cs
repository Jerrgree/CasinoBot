using CasinoBot.Domain.Enums;
using CasinoBot.Domain.Models;
using CasinoBot.Domain.Models.Players;
using CasinoBot.Domain.Models.Tables;

namespace CasinoBot.Domain.Interfaces
{
    public interface IGameDataStore
    {
        Task<Response> CreateTable(ulong guildId, TableType tableType);
        Task<Response> DeleteTable(long tableId);
        Task<Response<IEnumerable<Table>?>> GetTablesByGuild(ulong guildId);
        Task<Response<IEnumerable<Table>?>> GetTablesByPlayer(ulong playerId);
        Task<Response> AddPlayerToTable<T>(long tableId, ulong playerId, T playerState) where T : class;
        Task<Response> UpdatePlayer<T>(long tableId, ulong playerId, T playerState) where T : class;
        Task<Response<IEnumerable<Player<T>>?>> GetPlayersByTable<T>(ulong tableId) where T : class;
    }
}
