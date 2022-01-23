using CasinoBot.Data;
using CasinoBot.Data.Entities;
using CasinoBot.Domain.Enums;
using CasinoBot.Domain.Interfaces;
using Newtonsoft.Json;
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

        public async Task<(bool isSuccessful, string message)> CreateTable(ulong guildId, TableType tableType)
        {
            throw new NotImplementedException();
        }

        public async Task<(bool isSuccessful, string message)> DeleteTable(long tableId)
        {
            throw new NotImplementedException();
        }

        public async Task<(bool isSuccessful, IEnumerable<ITable<T>> tables, string message)> GetTablesByGuild<T>(ulong guildId) where T : class
        {
            throw new NotImplementedException();
        }

        public async Task<(bool isSuccessful, string message, IEnumerable<ITable<T>> tables)> GetTablesByPlayer<T>(ulong playerId) where T : class
        {
            throw new NotImplementedException();
        }

        public async Task<(bool isSuccessful, string message)> AddPlayerToTable<T>(long tableId, ulong playerId, T playerState) where T : class
        {
            try
            {
                var userTable = new UserTable()
                {
                    TableId = tableId,
                    UserId = playerId,
                    State = JsonConvert.SerializeObject(playerState)
                };

                _dbContext.UserTables.Add(userTable);

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var message = HandleException(ex); // Maybe return an enum instead?
                return (false, message);
            }

            return (true, null);
        }


        public async Task<(bool isSuccessful, string message)> UpdatePlayer<T>(long tableId, ulong playerId, T playerState) where T : class
        {
            try
            {
                var playerTable = _dbContext.UserTables.First(ut => ut.UserId == playerId && ut.TableId == tableId);
            }
            catch (Exception ex)
            {
                var message = HandleException(ex); // Maybe return an enum instead?
                return (false, message);
            }

            return (true, null);
        }

        private string HandleException(Exception ex, [CallerMemberName] string callerName = "")
        {
            _loggingService.LogErrorMessage($"Exception encountered in {callerName}. Ex = {ex}");
            return "TODO";
        }
    }
}