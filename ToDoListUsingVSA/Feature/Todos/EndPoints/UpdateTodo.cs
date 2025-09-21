
using Immediate.Apis.Shared;
using Immediate.Handlers.Shared;
using Immediate.Validations.Shared;
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
     [Validate]   
    public sealed partial record Command : IValidationTarget<Command>
    {
       [Validate]
        public sealed partial record CommandBody : IValidationTarget<CommandBody>
        {
            [NotEmpty]
            [MaxLength(60)]
            public string Title { get; set; }
            [NotEmpty]
            public bool IsComplete { get; set; }
        }
        [FromRoute]
        [GreaterThan(0 ,Message ="The Id must be greater than 0")]
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