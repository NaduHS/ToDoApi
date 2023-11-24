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
    }
}
