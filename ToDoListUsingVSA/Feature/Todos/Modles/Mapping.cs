namespace ToDoListUsingVSA.Feature.Todos.Modles;

public static class Mapping
{
    public static TodoResult ToDto(this Data.Type.TodoItem item) => new(item.TodoId, item.Title, item.IsComplete);
}