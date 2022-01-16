using CasinoBot.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CasinoBot.Data.Mappings
{
    internal class LogEntriesMapping : IEntityTypeConfiguration<LogEntry>
    {
        public void Configure(EntityTypeBuilder<LogEntry> builder)
        {
            builder
                .HasKey(l => l.LogEntryId);
        }
    }
}
