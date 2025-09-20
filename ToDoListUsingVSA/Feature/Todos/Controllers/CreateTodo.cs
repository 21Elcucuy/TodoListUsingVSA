using Microsoft.AspNetCore.Mvc;
using ToDoListUsingVSA.Data;
using ToDoListUsingVSA.Data.Type;
namespace ToDoListUsingVSA.Feature.Todos.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public sealed partial class CreateTodo : ControllerBase
{
    private readonly AppDbContext _context;

    public CreateTodo(AppDbContext context)
    {
        _context = context;
    }
    public sealed record CreateTodoCommand
    {
        
        public string Title { get; set; }
        
    }
   [HttpPost("/Todos")]
    public async Task<ActionResult> Create([FromBody]CreateTodoCommand command)
    {
        CreateTodoHandler todoHandler = new CreateTodoHandler(_context);
        var result = await todoHandler.Handle(command);
        return Ok(result);
        
    }

    public class CreateTodoHandler
    {
        private readonly AppDbContext _context;

        public CreateTodoHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TodoItem> Handle(CreateTodoCommand createTodo)
        {
            var TodoItem = new TodoItem()
            {
                Title = createTodo.Title,
                IsComplete = false,
            };
            await _context.Todos.AddAsync(TodoItem);
            await _context.SaveChangesAsync();
            return TodoItem;
        }
    }
}


