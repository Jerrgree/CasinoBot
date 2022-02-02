using CasinoBot.Data;
using CasinoBot.Data.Entities;
using CasinoBot.Domain.Enums;
using CasinoBot.Domain.Interfaces;
using CasinoBot.Domain.Models;
using CasinoBot.Domain.Models.Players;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using DomainTable = CasinoBot.Domain.Models.Tables.Table;

namespace CasinoBot.SqlDataStore
{
    public class CasinoBotSqlGameDataStore : IGameDataStore
    {
        private readonly CasinoContext _dbContext;
        private readonly ILoggingService _loggingService;
        public CasinoBotSqlGameDataStore(ILoggingService loggingService,
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
                var errorCode = HandleException(ex); // Maybe return an enum instead?
                return new Response(false, errorCode);
            }

            return new Response();
        }

        public async Task<Response> DeleteTable(long tableId)
        {
            try
            {
                var tableToRemove = await _dbContext
                    .Tables
                    .Include(t => t.UserTables)
                    .SingleAsync(t => t.TableId == tableId);

                _dbContext.UserTables.RemoveRange(tableToRemove.UserTables);
                _dbContext.Tables.Remove(tableToRemove);

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var errorCode = HandleException(ex);
                return new Response(false, errorCode);
            }

            return new Response();
        }

        public async Task<Response<IEnumerable<DomainTable>?>> GetTablesByGuild(ulong guildId)
        {
            try
            {
                var tables = await _dbContext
                        .Tables
                        .Include(t => t.UserTables)
                        .Where(t => t.GuildId == guildId)
                        .ToListAsync();

                var domainTables = tables.Select(t => new DomainTable()
                {
                    TableId = t.TableId,
                    TableType = t.TableType,
                    PlayerIds = t.UserTables.Select(ut => ut.UserId).ToList()
                }).ToList();

                return new Response<IEnumerable<DomainTable>?>(domainTables);
            }
            catch (Exception ex)
            {
                var errorCode = HandleException(ex);
                return new Response<IEnumerable<DomainTable>?>(false, errorCode, null);
            }
        }

        public async Task<Response<IEnumerable<DomainTable>?>> GetTablesByPlayer(ulong playerId)
        {
            try
            {
                var tables = await _dbContext
                        .Tables
                        .Include(t => t.UserTables)
                        .Where(t => t.UserTables.Any(ut => ut.UserId == playerId))
                        .ToListAsync();

                var domainTables = tables.Select(t => new DomainTable()
                {
                    TableId = t.TableId,
                    TableType = t.TableType,
                    PlayerIds = t.UserTables.Select(ut => ut.UserId).ToList()
                }).ToList();

                return new Response<IEnumerable<DomainTable>?>(domainTables);
            }
            catch (Exception ex)
            {
                var errorCode = HandleException(ex);
                return new Response<IEnumerable<DomainTable>?>(false, errorCode, null);
            }
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
                var errorCode = HandleException(ex); // Maybe return an enum instead?
                return new Response(false, errorCode);
            }

            return new Response();
        }

        public async Task<Response> UpdatePlayer<T>(long tableId, ulong playerId, T playerState) where T : class
        {
            try
            {
                var playerTable = await _dbContext
                    .UserTables
                    .FirstAsync(ut => ut.UserId == playerId && ut.TableId == tableId);
                playerTable.State = JsonConvert.SerializeObject(playerState);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var errorCode = HandleException(ex); // Maybe return an enum instead?
                return new Response(false, errorCode);
            }

            return new Response();
        }

        public async Task<Response<IEnumerable<Player<T>>?>> GetPlayersByTable<T>(long tableId) where T : class
        {
            try
            {
                var userTables = await _dbContext
                    .UserTables
                    .Where(ut => ut.TableId == tableId)
                    .ToListAsync();

                var players = userTables.Select(ut => new Player<T>(ut.UserId, JsonConvert.DeserializeObject<T>(ut.State)!))
                    .ToList();

                return new Response<IEnumerable<Player<T>>?>(players);
            }
            catch (Exception ex)
            {
                var errorCode = HandleException(ex); // Maybe return an enum instead?
                return new Response<IEnumerable<Player<T>>?>(false, errorCode, null);
            }
        }

        private ResponseCode HandleException(Exception ex, [CallerMemberName] string callerName = "")
        {
            _loggingService.LogErrorMessage($"Exception encountered in {callerName}. Ex = {ex}");

            if (ex is InvalidOperationException)
            {
                return ex.Message switch
                {
                    "Sequence contains no elements." => ResponseCode.DoesNotExist,
                    _ => ResponseCode.OtherError
                };
            }
            else if (ex is DataException && ex.InnerException is SqlException sqlException)
            {
                return sqlException.Number switch
                {
                    2627 => ResponseCode.AlreadyExists,
                    _ => ResponseCode.OtherError
                };
            }
            return ResponseCode.OtherError;
        }
    }
}