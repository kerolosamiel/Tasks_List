namespace TasksList;

/// <summary>
/// Represents a task in the task management application.
/// </summary>

public class TaskItem
{
    public required int TaskId { get; set; }
    public required string TaskName { get; set; }
    public required bool IsCompleted { get; set; }
    public string? TaskDescription { get; set; }
}