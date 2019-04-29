using Microsoft.EntityFrameworkCore;
using System;
using VimanetTask.Entities;
using VimanetTask.Persistence.Configurations;

namespace VimanetTask.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<SynonymsRecord> SynonymsRecords { get; set; }

        public AppDbContext()
        {

        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SynonymsApp;MultipleActiveResultSets=True;Integrated Security=True");
            }

            //base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SynonymsRecordConfiguration());
        }
    }
}
