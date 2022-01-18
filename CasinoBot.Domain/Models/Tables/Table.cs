using CasinoBot.Domain.Enums;

namespace CasinoBot.Domain.Models.Tables
{
    public class Table
    {
        public ulong GuildId {get;set;}
        public TableType TableType { get; set; }

        public List<object> Users { get; set; }
    }
}
