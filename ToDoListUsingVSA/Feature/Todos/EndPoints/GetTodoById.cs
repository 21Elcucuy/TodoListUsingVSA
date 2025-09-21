using Immediate.Apis.Shared;
using Immediate.Handlers.Shared;
using Immediate.Validations.Shared;
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
   
    [Validate]
    public sealed partial record Query : IValidationTarget<Query>
    {
        [GreaterThan(0 ,Message ="The Id must be greater than 0")]
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