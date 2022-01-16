using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CasinoBot.Data.Migrations
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CasinoContext>
    {
        public CasinoContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("connectionStrings.json")
                    .Build();

            var builder = new DbContextOptionsBuilder<CasinoContext>();
            var connectionString = config.GetConnectionString("database");
            builder.UseSqlServer(connectionString,
                x => x.MigrationsAssembly("CasinoBot.Data.Migrations"));
            return new CasinoContext(builder.Options);
        }
    }
}
