using CasinoBot.Domain.Enums;
using CasinoBot.Domain.Interfaces;
using CasinoBot.Domain.Models.Tables;

namespace CasinoBot.Games.BlackJack.Domain.Models
{
    public class BlackJack : IGame
    {
        private Table _table;
        public async Task Play(IEnumerable<ulong> players, ulong guild)
        {
            Initialize(players, guild)
        }

        private void Initialize(IEnumerable<ulong> players, ulong guild)
        {
            _table = new Table()
            {
                TableType = TableType.BlackJack,
                PlayerIds = players.ToList()
            };
        }
    }
}
