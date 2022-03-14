using System;
using BlackPearl;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SeidoDemoDb
{
    public class PearlDbContext : DbContext
    {
        public DbSet<PearlList> PearlLists { get; set; }
        public DbSet<Pearl> Pearls { get; set; }

        public PearlDbContext() { }
        public PearlDbContext(DbContextOptions options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = DBConnection.ConfigurationRoot.GetConnectionString("SQLite_pearlDB1");
                optionsBuilder.UseSqlite(connectionString);
            }
            base.OnConfiguring(optionsBuilder);
        }
    }
}
