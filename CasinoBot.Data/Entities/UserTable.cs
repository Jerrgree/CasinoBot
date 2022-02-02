namespace CasinoBot.Data.Entities
{
    public class UserTable
    {
        public ulong UserId { get; set; }
        public long TableId { get; set; }
        public string State { get; set; } = null!;

        public Table Table { get; set; } = null!;
    }
}
