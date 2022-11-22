using Microsoft.AspNetCore.Mvc;
using Todolistapplication.Interface;
using Todolistapplication.Models;
using Microsoft.AspNetCore.Authorization;
using Npgsql;

namespace Todolistapplication.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodolist _iTodolist;
        public TodoController(ITodolist todolist)
        {
            _iTodolist = todolist;
        }
        
        // Get api/Todolist
        [HttpGet]
        public async Task<ActionResult<List<TodoItem>>> Get(Status? status)
        {
            if (status.HasValue)
                return await _iTodolist.GetTodoItemDetailsByStatusAsync(status.Value);
            
            return await _iTodolist.GetTodoItemDetailsAsync();
        }

        // POST api/ Create Todolist
        [HttpPost]
        public async Task<ActionResult<TodoItem>>Post(TodoItemDto todoItem)
        {
            var todoItems = new TodoItem()
            {
                ItemName = todoItem.ItemName,
                ItemDescription = todoItem.ItemDescription,
                ItemCreated = DateTime.UtcNow,
                ItemUpdated = DateTime.UtcNow,
                UserId = todoItem.UserId,

            }; 
            _iTodolist.AddTodoItem(todoItems);
            return await Task.FromResult(todoItems);
        }

        // PUT api/Todolist
        [HttpPut("{id}")]
        public async Task<ActionResult<TodoItem>> Put(int id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }
            try
            {
                _iTodolist.UpdateTodoItem(todoItem);
            }
            catch
            {
                throw new NpgsqlException();
            }
            
            return await Task.FromResult(todoItem);
        }

        // DELETE api/Todolist
        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoItem>> Delete(int id)
        {
            var todoItem = _iTodolist.DeleteTodoItem(id);
            return await Task.FromResult(todoItem);
        }
    }
}
