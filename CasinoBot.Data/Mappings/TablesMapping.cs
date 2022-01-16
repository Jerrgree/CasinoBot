using CasinoBot.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CasinoBot.Data.Mappings
{
    internal class TablesMapping : IEntityTypeConfiguration<Table>
    {
        public void Configure(EntityTypeBuilder<Table> builder)
        {
            builder
                .HasKey(t => t.TableId);
        }
    }
}
