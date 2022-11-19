
using Microsoft.EntityFrameworkCore;
namespace Todolistapplication.Models
{
    public class TodolistDbContext :DbContext
    {
        public TodolistDbContext()
        {
        }
        public TodolistDbContext(DbContextOptions<TodolistDbContext> options)
          : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();

        }
        public DbSet<User>? userInfos { get; set; }
        public DbSet<TodoItem>? TodoItems { get; set; }





    }
}
