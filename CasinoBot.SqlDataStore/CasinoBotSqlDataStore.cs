using CasinoBot.Data;
using CasinoBot.Domain.Enums;
using CasinoBot.Domain.Interfaces;
using CasinoBot.SqlDataStore.Models;
using System.Runtime.CompilerServices;

namespace CasinoBot.SqlDataStore
{
    public class CasinoBotSqlDataStore : IGameDataStore
    {
        private readonly CasinoContext _dbContext;
        private readonly ILoggingService _loggingService;
        public CasinoBotSqlDataStore(ILoggingService loggingService,
             CasinoContext dbContext)
        {
            _loggingService = loggingService;
            _dbContext = dbContext;
        }
        public Task<(bool isSuccessful, string message)> AddPlayerToTable<T>(long tableId, ulong playerId, T playerState) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<(bool isSuccessful, string message)> CreateTable(ulong guildId, TableType tableType)
        {
            throw new NotImplementedException();
        }

        public Task<(bool isSuccessful, string message)> DeleteTable(long tableId)
        {
            throw new NotImplementedException();
        }

        public Task<(bool isSuccessful, IEnumerable<ITable<T>> tables, string message)> GetTablesByGuild<T>(ulong guildId) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<(bool isSuccessful, string message, IEnumerable<ITable<T>> tables)> GetTablesByPlayer<T>(ulong playerId) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<(bool isSuccessful, string message)> UpdatePlayer<T>(long tableId, ulong playerId, T playerState) where T : class
        {
            throw new NotImplementedException();
        }

        private string HandleException(Exception ex, ulong? userId, [CallerMemberName] string callerName = "")
        {
            var logMessage = new LogEntry($"Exception encountered in {callerName}. Ex = {ex}")
            {
                UserId = userId
            };
            _loggingService.LogErrorMessage(logMessage)
            throw new Exception("TODO");
        }
    }
}