using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Backend
{
    public class DatabaaseContext : DbContext
    {
        //DBSets


        public string DbPath { get; }

        public DatabaaseContext()
        {
            var path = Directory.GetCurrentDirectory();
            DbPath = System.IO.Path.Join(path, "database.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // current folder
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}
