using CasinoBot.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CasinoBot.Data
{
    public class CasinoContext : DbContext
    {
        public DbSet<Table> Tables { get; set; }
        public DbSet<UserTable> UserTables { get; set; }
        public DbSet<LogEntry> LogEntries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer(@"Server=localhost;Database=CasinoBot;Trusted_Connection=True;");

        #region Required
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CasinoContext).Assembly);

        }
        #endregion

    }
}