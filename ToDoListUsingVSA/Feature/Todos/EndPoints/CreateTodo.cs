using Immediate.Apis.Shared;
using Immediate.Handlers.Shared;
using Immediate.Validations.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using ToDoListUsingVSA.Data;
using ToDoListUsingVSA.Data.Type;
using ToDoListUsingVSA.Feature.Todos.Modles;

namespace ToDoListUsingVSA.Feature.Todos.EndPoints;

[Handler]
[MapPost("api/Todos/CreateTodo")]
public static partial class CreateTodo
{
    [Validate]
    public  sealed partial record Command : IValidationTarget<Command>
    {
        [NotEmpty]
        [MaxLength(60)]
        public  string Title { get; set; }
        
    }
 
    private static async ValueTask<Ok<TodoResult>> HandleAsync(Command command , AppDbContext context, CancellationToken token)
    {
        var TodoItem = new TodoItem()
        {
            Title = command.Title,
            IsComplete = false,
        };
         await context.Todos.AddAsync(TodoItem, token);
         await context.SaveChangesAsync(token);
         return TypedResults.Ok(TodoItem.ToDto());
    }
}


