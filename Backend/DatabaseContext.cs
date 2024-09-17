using Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend
{
    public class DatabaseContext : DbContext
    {
        public string DbPath { get; }
        
        public DbSet<User> Users { get; set; }

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
