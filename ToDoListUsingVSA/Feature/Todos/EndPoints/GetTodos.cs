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
    private static async ValueTask<Ok<List<TodoResult>>> HandleAsync(object _, AppDbContext context, CancellationToken token)
    {
        var result = await context.Todos.ToListAsync(token);
        List<TodoResult> ItemsResult = new List<TodoResult>();
        
        foreach (var item in result)
        {
            ItemsResult.Add(item.ToDto());
        }
        return TypedResults.Ok(ItemsResult);
    }
}