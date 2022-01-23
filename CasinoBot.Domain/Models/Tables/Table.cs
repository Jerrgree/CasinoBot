using CasinoBot.Domain.Enums;

namespace CasinoBot.Domain.Models.Tables
{
    public class Table
    {
        public long TableId { get; set; }

        public TableType TableType { get; set; }
    }
}
