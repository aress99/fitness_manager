using FitnessManagerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessManagerApi.Data
{
    public class FitnessDbContext : DbContext
    {
        public FitnessDbContext(DbContextOptions<FitnessDbContext> options) : base(options) { }

        public DbSet<Member> Members { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member>().HasIndex(m => m.FitnessNumber).IsUnique();
        }
    }
}