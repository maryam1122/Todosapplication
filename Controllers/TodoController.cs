using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todolistapplication.Interface;
using Todolistapplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;






namespace Todolistapplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodolist _ITodolist;
        public TodoController(ITodolist ITodolist)
        {
            _ITodolist = ITodolist;
        }

        // POST api/Todolist
        [HttpPost]
        public async Task<ActionResult<TodoItem>>Post(TodoItem todoItem)
        {
            _ITodolist.AddTodoItem(todoItem);
            return await Task.FromResult(todoItem);

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
                _ITodolist.UpdateTodoItem(todoItem);
            }
            catch
            {
                throw;
            }
            
            return await Task.FromResult(todoItem);
        }

        // DELETE api/Todolist
        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoItem>> Delete(int id)
        {
            var todoItem = _ITodolist.DeleteTodoItem(id);
            return await Task.FromResult(todoItem);
        }

       

    }
}
