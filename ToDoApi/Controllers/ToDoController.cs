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
            try
            {
                var result = await _context.TodoItems.ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }

        }

        //Get Item by Id
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoItem>> GetTodoItemById(int id)
        {
            try
            {
                var todoItem = await _context.TodoItems.FindAsync(id);

                if (todoItem == null)
                {
                    return NotFound();
                }

                return todoItem;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }

        }

        //	Create a new record
        [HttpPost]
        public async Task<ActionResult<ToDoItem>> CreateTodoItem(ToDoItem item)
        {
            try
            {
                if (item.Id == 0)
                {
                    _context.TodoItems.Add(item);
                }

                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetTodoItemById), new { id = item.Id }, item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }


        }

        // Mark an item as “done”
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(int id, ToDoItem item)
        {
            try
            {
                if (id != item.Id)
                {
                    return BadRequest();
                }

                _context.Entry(item).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }

        }

        //	Delete records individually
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            try
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
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }

        }

        // Delete all records
        [HttpDelete]
        public async Task<IActionResult> DeleteAllTodoItems()
        {
            try
            {
                _context.TodoItems.RemoveRange(_context.TodoItems);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }

        }
    }
}
