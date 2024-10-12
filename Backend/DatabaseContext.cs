using Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend
{
    public class DatabaseContext : DbContext
    {
        public string DbPath { get; }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.Id)
                .HasConversion(id => id.Value, id => new UserId(id))
                .IsRequired();
            modelBuilder.Entity<Project>()
                .Property(u => u.Id)
                .HasConversion(id => id.Value, id => new ProjectId(id))
                .IsRequired();
            modelBuilder.Entity<Goal>()
                .Property(u => u.Id)
                .HasConversion(id => id.Value, id => new GoalId(id))
                .IsRequired();
            modelBuilder.Entity<Question>()
                .Property(u => u.Id)
                .HasConversion(id => id.Value, id => new QuestionId(id))
                .IsRequired();
            
            modelBuilder.Entity<Project>()
                .HasMany(p => p.Goals)
                .WithOne(g => g.Project)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Goal>()
                .HasMany(g => g.Questions)
                .WithOne(q => q.Goal)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Question>()
                .HasOne(q => q.Goal)
                .WithMany(g => g.Questions)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DatabaseContext()
        {
            var path = Directory.GetCurrentDirectory();
            DbPath = System.IO.Path.Join(path, "gqm.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // current folder
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}
