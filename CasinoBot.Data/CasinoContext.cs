using CasinoBot.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CasinoBot.Data
{
    public class CasinoContext : DbContext
    {
        public DbSet<Table> Tables { get; set; } = null!;
        public DbSet<UserTable> UserTables { get; set; } = null!;
        public DbSet<LogEntry> LogEntries { get; set; } = null!;

        public CasinoContext(DbContextOptions<CasinoContext> dbContextOptions) : base(dbContextOptions) { }

        #region Required
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CasinoContext).Assembly);
        }
        #endregion

    }
}