using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using SeidoDemoModels;

namespace SeidoDemoDb
{
    public class SeidoDemoDbContext:DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

        public SeidoDemoDbContext() { }
        public SeidoDemoDbContext(DbContextOptions options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = DBConnection.ConfigurationRoot.GetConnectionString("SQLite_pearlv2");
                optionsBuilder.UseSqlite(connectionString);
            }
            base.OnConfiguring(optionsBuilder);
        }
    }
}
