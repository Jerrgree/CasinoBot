using CasinoBot.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CasinoBot.Data.Mappings
{
    internal class UserTablesMapping : IEntityTypeConfiguration<UserTable>
    {
        public void Configure(EntityTypeBuilder<UserTable> builder)
        {
            builder
                .HasKey(ut => new { ut.UserId, ut.TableId });
        }
    }
}
