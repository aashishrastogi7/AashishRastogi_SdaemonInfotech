using JetBrains.Annotations;

namespace CRUD_Operation.Data;

public class Task
{
    public Guid Id { get; set; }
    public string Title { get; set; } = " ERROR - Title not inserted";
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public bool IsComplete { get; set; }
}
