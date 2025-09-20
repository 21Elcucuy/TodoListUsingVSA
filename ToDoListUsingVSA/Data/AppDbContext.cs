using Microsoft.EntityFrameworkCore;
using ToDoListUsingVSA.Data.Type;

namespace ToDoListUsingVSA.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
   public  DbSet<TodoItem> Todos { get; set; }
}