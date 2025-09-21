using Immediate.Apis.Shared;
using Immediate.Handlers.Shared;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ToDoListUsingVSA.Data;
using ToDoListUsingVSA.Feature.Todos.Modles;

namespace ToDoListUsingVSA.Feature.Todos.EndPoints;

[Handler]
[MapDelete("api/todos/deleteTodo/{todoId}")]
public static partial class DeleteTodo
{
    public record Query
    {
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