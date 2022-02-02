using CasinoBot.Domain.Enums;

namespace CasinoBot.Data.Entities
{
    public class Table
    {
        public long TableId { get; set; }
        public ulong GuildId { get; set; }
        public TableType TableType { get; set; }

        public List<UserTable> UserTables { get; set; } = null!;
    }
}
