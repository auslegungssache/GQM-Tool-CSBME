using Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend;

public class DatabaseContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Project> Projects { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    { }

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
    }
}