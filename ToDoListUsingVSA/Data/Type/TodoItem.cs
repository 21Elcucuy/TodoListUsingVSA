using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ToDoListUsingVSA.Data.Type;

public class TodoItem
{
    [Key]
    public int TodoId { get; set; }
    public string Title { get; set; }
    public bool IsComplete { get; set; }
}