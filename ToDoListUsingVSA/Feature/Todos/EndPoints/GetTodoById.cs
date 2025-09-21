using Immediate.Apis.Shared;
using Immediate.Handlers.Shared;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ToDoListUsingVSA.Data;
using ToDoListUsingVSA.Data.Type;
using ToDoListUsingVSA.Feature.Todos.Modles;

namespace ToDoListUsingVSA.Feature.Todos.EndPoints;

[Handler]
[MapGet("/api/todos/{id}")]
public static partial class GetTodoById 
{
   
    
    public sealed record Query
    {
        public required int TodoId { get; set; }
    }



    private static async ValueTask<Results<Ok<TodoResult> ,NotFound>> HandleAsync(Query query, AppDbContext context, CancellationToken token)
    {
        var result = context.Todos.FirstOrDefault(x => x.TodoId == query.TodoId);
        if (result == null)
        {
            return TypedResults.NotFound();
        }
 
        return TypedResults.Ok(result.ToDto());        
    }
}