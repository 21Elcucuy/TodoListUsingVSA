using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ToDoListUsingVSA.Data;
using ToDoListUsingVSA.Data.Type;

namespace ToDoListUsingVSA.Feature.Todos.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class GetTodoById : ControllerBase
{
    private readonly AppDbContext _context;

    public GetTodoById(AppDbContext context)
    {
        _context = context;
    }

    public sealed record GetTodoQuery
    {
        public int TodoId { get; set; }
    }

    [HttpGet("/Todos/{TodoId}")]
    public async Task<ActionResult> GetById([FromRoute] int TodoId)
    { 
        GetTodobyIdHandler getTodobyIdHandler = new GetTodobyIdHandler(_context);
        var result = await getTodobyIdHandler.Handle(new GetTodoQuery()  { TodoId = TodoId });
        if (result == null)
        {
            return BadRequest() ;
            
        }
        return Ok(result);
    }
    
    public class GetTodobyIdHandler
    {
        
        private readonly AppDbContext _context;

        public GetTodobyIdHandler(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<TodoItem> Handle(GetTodoQuery query)
        {
            var result = _context.Todos.FirstOrDefault(x => x.TodoId == query.TodoId);
            return result;
        }
    }
    
}