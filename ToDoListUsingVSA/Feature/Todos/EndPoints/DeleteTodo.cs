using Immediate.Apis.Shared;
using Immediate.Handlers.Shared;
using Immediate.Validations.Shared;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ToDoListUsingVSA.Data;
using ToDoListUsingVSA.Feature.Todos.Modles;

namespace ToDoListUsingVSA.Feature.Todos.EndPoints;

[Handler]
[MapDelete("api/todos/deleteTodo/{todoId}")]
public static partial class DeleteTodo
{
    [Validate]
    public sealed partial record Query: IValidationTarget<Query>
    {
        [GreaterThan(0 ,Message ="The Id must be greater than 0")]
        public int todoId { get; init; }
    }
    
    private async static ValueTask<Ok<TodoResult>> HandleAsync(Query query, AppDbContext context,CancellationToken token)
    {
        var result = await context.Todos.FirstOrDefaultAsync(x => x.TodoId == query.todoId, token);
        context.Todos.Remove(result);
        await context.SaveChangesAsync(token);
        return TypedResults.Ok(result.ToDto());
    }
}