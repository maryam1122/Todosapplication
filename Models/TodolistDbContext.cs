
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
        public DbSet<user_info>? user_infos { get; set; }
        public DbSet<TodoItem>? TodoItems { get; set; }





    }
}
