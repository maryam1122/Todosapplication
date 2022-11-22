using Todolistapplication.Interface;
using Todolistapplication.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Todolistapplication.Repository
{
    public class TodoItemRepository : ITodolist
    {
        readonly TodolistDbContext _dbContext = new();
        public TodoItemRepository(TodolistDbContext dbContext)
        { _dbContext = dbContext; }

        public async Task<List<TodoItem>> GetTodoItemDetailsAsync()
        {
            try
            {
                return await _dbContext?.TodoItems?.ToListAsync();
            }
            catch
            {
                throw new NpgsqlException();
            }
        }
        
        public async Task<List<TodoItem>> GetTodoItemDetailsByStatusAsync(Status status)
        {
            try
            {
                return await _dbContext?.TodoItems?.Where(todo => todo.Status == status).ToListAsync();
            }
            catch
            {
                throw new NpgsqlException();
            }
        }
        
        public async Task<TodoItem> GetTodoItemDetailsAsync(int id)
        {
            try
            {
                TodoItem? todoItem = await _dbContext.TodoItems.FindAsync(id);
                if (todoItem != null)
                {
                    return todoItem;
                }

                throw new ArgumentNullException();
            }
            catch
            {
                throw new NpgsqlException();
            }

        }
        public async void AddTodoItem(TodoItem item)
        {
            try
            {  
                _dbContext.TodoItems?.Add(item);
                await _dbContext.SaveChangesAsync();
            }
            catch
            {
                throw new NpgsqlException();
            }
        }
        public void UpdateTodoItem(TodoItem item)
        {
            try
            {
                _dbContext.Entry(item).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch
            {
                throw new NpgsqlException();
            }
        }
        public TodoItem DeleteTodoItem(int Id)
        {
            try
            {
                TodoItem? todoItem = _dbContext.TodoItems?.Find(Id);
                if (todoItem != null)
                {
                    _dbContext.TodoItems?.Remove(todoItem);
                    _dbContext.SaveChanges();
                    return todoItem;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw new NpgsqlException();
            }
        }

    }
}
