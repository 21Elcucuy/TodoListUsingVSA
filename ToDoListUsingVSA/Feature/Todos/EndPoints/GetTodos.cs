using Immediate.Apis.Shared;
using Immediate.Handlers.Shared;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ToDoListUsingVSA.Data;
using ToDoListUsingVSA.Feature.Todos.Modles;

namespace ToDoListUsingVSA.Feature.Todos.EndPoints;

[Handler]
[MapGet("/api/todos/GetTodos")]
public static partial class GetTodos
{
    private static async ValueTask<Ok<IReadOnlyList<TodoResult>>> HandleAsync(object _, AppDbContext context, CancellationToken token)
    {
        var result = await context.Todos.Select(t => t.ToDto()).ToListAsync(token);
        
        
        return TypedResults.Ok<IReadOnlyList<TodoResult>>(result);
    }
}