using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoApi.Models;
using ToDoApi.Data;
using Microsoft.EntityFrameworkCore;

namespace ToDoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly ToDoContext _context;

        public ToDoController(ToDoContext context)
        {
            _context = context;
        }

        //Get the list of all existing “to-do” items
        [HttpGet("/GetAll")]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetTodoItems()
        {
            var result = await _context.TodoItems.ToListAsync();

            return result;
        }

        //Get Item by Id
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoItem>> GetTodoItemById(int id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        //	Create a new record
        [HttpPost]
        public async Task<ActionResult<ToDoItem>> CreateTodoItem(ToDoItem item)
        {
            if(item.Id == 0)
            {
                _context.TodoItems.Add(item);
            }

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodoItemById), new { id = item.Id }, item);
        }

        // Mark an item as “done”
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(int id, ToDoItem item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //	Delete records individually
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Delete all records
        [HttpDelete]
        public async Task<IActionResult> DeleteAllTodoItems()
        {
            _context.TodoItems.RemoveRange(_context.TodoItems);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
