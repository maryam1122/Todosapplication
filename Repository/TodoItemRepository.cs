using Todolistapplication.Interface;
using Todolistapplication.Models;
using Microsoft.EntityFrameworkCore;

namespace Todolistapplication.Repository
{
    public class TodoItemRepository : ITodolist
    {
        readonly TodolistDbContext _dbContext = new();
        public TodoItemRepository(TodolistDbContext dbContext)
        { _dbContext = dbContext; }

        public List<TodoItem> GetTodoItemDetails()
        {
            try
            {
                return _dbContext.TodoItems.ToList();
            }
            catch
            {
                throw;

            }
        }
        public TodoItem GetTodoItemDetails(int id)
        {
            try
            {
                TodoItem? todoItem = _dbContext.TodoItems.Find(id);
                if (todoItem != null)
                {
                    return todoItem;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw;
            }

        }
        public void AddTodoItem(TodoItem item)
        {
            try
            {
                _dbContext.TodoItems.Add(item);
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
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
                throw;
            }
        }
        public TodoItem DeleteTodoItem(int Id)
        {
            try
            {
                TodoItem? todoItem = _dbContext.TodoItems.Find(Id);
                if (todoItem != null)
                {
                    _dbContext.TodoItems.Remove(todoItem);
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
                throw;
            }
        }

    }
}
