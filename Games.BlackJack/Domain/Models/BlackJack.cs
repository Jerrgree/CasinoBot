using CasinoBot.Domain.Enums;
using CasinoBot.Domain.Interfaces;
using CasinoBot.Domain.Models.Tables;

namespace CasinoBot.Games.BlackJack.Domain.Models
{
    public class BlackJack : IGame
    {
        private Table _table;
        private readonly IGameDataStore _gameDataStore;

        public BlackJack(IGameDataStore gameDataStore)
        {
            _table = new();
            _gameDataStore = gameDataStore;
        }

        public async Task Play(IEnumerable<ulong> players, ulong guild)
        {
            Initialize(players, guild);
        }

        private void Initialize(IEnumerable<ulong> players, ulong guild)
        {
            _table = new Table()
            {
                TableType = TableType.BlackJack,
                PlayerIds = players
            };
        }
    }
}
