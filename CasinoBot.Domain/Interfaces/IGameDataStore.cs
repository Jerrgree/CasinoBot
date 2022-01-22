using CasinoBot.Domain.Enums;

namespace CasinoBot.Domain.Interfaces
{
    public interface IGameDataStore
    {
        Task<(bool isSuccessful, string message)> CreateTable(ulong guildId, TableType tableType);
        Task<(bool isSuccessful, string message)> DeleteTable(long tableId);
        Task<(bool isSuccessful, IEnumerable<ITable<T>> tables, string message)> GetTablesByGuild<T>(ulong guildId) where T : class;
        Task<(bool isSuccessful, string message, IEnumerable<ITable<T>> tables)> GetTablesByPlayer<T>(ulong playerId) where T : class;
        Task<(bool isSuccessful, string message)> AddPlayerToTable<T>(long tableId, ulong playerId, T playerState) where T : class;
        Task<(bool isSuccessful, string message)> UpdatePlayer<T>(long tableId, ulong playerId, T playerState) where T : class;
    }
}
