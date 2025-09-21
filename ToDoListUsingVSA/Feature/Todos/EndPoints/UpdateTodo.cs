using Immediate.Apis.Shared;
using Immediate.Handlers.Shared;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoListUsingVSA.Data;
using ToDoListUsingVSA.Feature.Todos.Modles;

namespace ToDoListUsingVSA.Feature.Todos.EndPoints;

[Handler]
[MapPut("api/todos/update/{todoId}")]
public static partial class UpdateTodo
{
    
    public record Command
    {

        public partial record CommandBody
        {
            public string Title { get; set; }
            public bool IsComplete { get; set; }
        }
        [FromRoute]
        public int todoId { get; init; }
        [FromBody]
        public CommandBody commandBody { get; init; }
        
        
    }

    private static async ValueTask<Results<Ok<TodoResult> , NotFound>> HandleAsync(Command commnad , AppDbContext context, CancellationToken token)
    {
        var result = await context.Todos.FirstOrDefaultAsync(x => x.TodoId == commnad.todoId , token);
        if (result == null)
        {
            return TypedResults.NotFound();

        }
        result.Title = commnad.commandBody.Title;
        result.IsComplete = commnad.commandBody.IsComplete;
        await context.SaveChangesAsync(token);
        return TypedResults.Ok(result.ToDto());
    }
}