using Microsoft.EntityFrameworkCore;
using ToDoApi.Models;

namespace ToDoApi.Data
{
    public class ToDoContext : DbContext
    {
        public DbSet<ToDoItem> TodoItems { get; set; }

        public ToDoContext(DbContextOptions<ToDoContext> options)
        : base(options)
        {
        }
    }
}
