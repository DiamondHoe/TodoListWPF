using Microsoft.EntityFrameworkCore;
using TodoList.Core.Entities;

namespace TodoList.Database
{
    public class TodoListDbContext : DbContext
    {
        public DbSet<Assignment> Assignments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlite($"Filename={Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "TodoListApp.sqlite")}");
        }
    }
}
