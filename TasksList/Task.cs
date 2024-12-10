namespace TasksList;

public class TaskItem
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required bool Completed { get; set; }
    public string? Description { get; set; }
}