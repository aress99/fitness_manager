using FitnessManagerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessManagerApi.Data
{
    public class FitnessDbContext : DbContext
    {
        public FitnessDbContext(DbContextOptions<FitnessDbContext> options) : base(options) { }

        public DbSet<Member> Members { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Workout> Workouts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member>().HasIndex(m => m.FitnessNumber).IsUnique();
            modelBuilder.Entity<Subscription>().HasOne(s => s.Member).WithMany().HasForeignKey(s => s.MemberId);
            modelBuilder.Entity<Workout>().HasOne(w => w.Member).WithMany().HasForeignKey(w => w.MemberId);

            // Конфигуриране на Price
            modelBuilder.Entity<Subscription>()
                .Property(s => s.Price)
                .HasColumnType("decimal(10,2)");
        }
    }
}