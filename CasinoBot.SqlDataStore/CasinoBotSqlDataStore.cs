using CasinoBot.Data;
using CasinoBot.Data.Entities;
using CasinoBot.Domain.Enums;
using CasinoBot.Domain.Interfaces;
using CasinoBot.Domain.Models;
using CasinoBot.Domain.Models.Players;
using Newtonsoft.Json;
using System.Data.Entity;
using System.Runtime.CompilerServices;

using DomainTable = CasinoBot.Domain.Models.Tables.Table;

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

        public async Task<Response> CreateTable(ulong guildId, TableType tableType)
        {
            try
            {
                _dbContext.Tables.Add(new Table()
                {
                    TableType = tableType,
                    GuildId = guildId
                });

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var message = HandleException(ex); // Maybe return an enum instead?
                return new Response(false, message);
            }

            return new Response();
        }

        public async Task<Response> DeleteTable(long tableId)
        {
            try
            {
                var tableToRemove = await _dbContext.Tables
                    .Include(t => t.UserTables)
                    .SingleAsync(t => t.TableId == tableId);

                _dbContext.UserTables.RemoveRange(tableToRemove.UserTables);
                _dbContext.Tables.Remove(tableToRemove);

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var message = HandleException(ex);
                return new Response(false, message);
            }

            return new Response();
        }

        public async Task<Response<IEnumerable<DomainTable>?>> GetTablesByGuild(ulong guildId)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<IEnumerable<DomainTable>?>> GetTablesByPlayer(ulong playerId)
        {
            throw new NotImplementedException();
        }

        public async Task<Response> AddPlayerToTable<T>(long tableId, ulong playerId, T playerState) where T : class
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
                return new Response(false, message);
            }

            return new Response();
        }

        public async Task<Response> UpdatePlayer<T>(long tableId, ulong playerId, T playerState) where T : class
        {
            try
            {
                var playerTable = _dbContext.UserTables.First(ut => ut.UserId == playerId && ut.TableId == tableId);
                playerTable.State = JsonConvert.SerializeObject(playerState);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var message = HandleException(ex); // Maybe return an enum instead?
                return new Response(false, message);
            }

            return new Response();
        }

        public async Task<Response<IEnumerable<Player<T>>?>> GetPlayersByTable<T>(ulong tableId) where T : class
        {
            throw new NotImplementedException();
        }

        private string HandleException(Exception ex, [CallerMemberName] string callerName = "")
        {
            _loggingService.LogErrorMessage($"Exception encountered in {callerName}. Ex = {ex}");
            return "TODO";
        }
    }
}