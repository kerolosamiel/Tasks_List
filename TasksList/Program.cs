using TasksList;

StartApplication();

// Initialize and start the application
void StartApplication()
{
    DisplayMessage("Welcome to Tasks List Application");
    var tasks = new List<TaskItem>(); // Create a list to store tasks

    while (true)
    {
        // Display menu options
        DisplayMessage("Please choose an option: ");
        DisplayMessage("""
                     1- Add a new task
                     2- View tasks and their progress
                     3- Update task name
                     4- Update task description
                     5- Update task progress
                     6- View task details
                     7- Remove a task
                     8- Clear all tasks
                     """);
        Console.Write("=> ");

        // Validate user input and execute the chosen option
        if (!TryParseInput(out int nSelectedOption))
        {
            DisplayMessage("Please enter valid number!!");
            Console.Clear();
            continue;
        }

        Console.Clear();
        ExecuteOption(tasks, nSelectedOption);
    }
}


#region Methods

#region Option Execution

// Handle the chosen menu option
void ExecuteOption(List<TaskItem> tasks, int option)
{
    switch (option)
    {
        case 1:
            AddTask(tasks);
            break;
        case 2:
            DisplayTasks(tasks);
            break;
        case 3:
            UpdateTaskName(tasks);
            break;
        case 4:
            UpdateTaskDescription(tasks);
            break;
        case 5:
            UpdateTaskProgress(tasks);
            break;
        case 6:
            ViewTaskDetails(tasks);
            break;
        case 7:
            RemoveTask(tasks);
            break;
        case 8:
            ClearAllTasks(tasks);
            break;
        default:
            DisplayMessage("Please inter number from (1 : 8)");
            break;
    }
}

#endregion

#region Helper Methods
// Display a message to the user
void DisplayMessage(string message)
{
    Console.WriteLine(message);
}

// Parse user input to an integer
bool TryParseInput(out int number)
{
    bool bIsValid = int.TryParse(Console.ReadLine(), out number);
    Console.Clear();
    return bIsValid;
}

// Confirm action from the user
bool ConfirmAction()
{
    string sResponse = Console.ReadLine()?.Trim().ToLower() ?? "no";

    Console.Clear();
    switch (sResponse)
    {
        case "yes":
        case "y":
            return true;
        default:
            return false;
    }
}

// Pause and clear the console
void PauseAndClear()
{
    DisplayMessage("Press any key to continue..."); // ask him to press any key to get back
    Console.ReadKey();
    Console.Clear();
}

// Display success message and list tasks 
void ShowSuccessMessage(List<TaskItem> tasks)
{
    DisplayMessage("Action completed successfully :)");
    ShowTasks(tasks); // Display the updated task list
}

// Prompt user for task index
void PromptForTaskIndex(List<TaskItem> tasks)
{
    DisplayMessage("Enter the task number: ");
    ShowTasks(tasks);// Show the list of tasks
    Console.Write("=> ");
}

// Ask user if they want to perform another action
bool RepeatAction(string actionDescription)
{
    DisplayMessage($"Do you want to {actionDescription} another task? (Y/N)");
    Console.Write("=> ");

    return ConfirmAction(); // Return user's decision
}
#endregion

#region Task Management Methods
// Add a new task to the list
void AddTask(List<TaskItem> tasks)
{
    bool bKeepAdding = true;

    while (bKeepAdding)
    {
        string sTaskName = GetTaskInput("task name");

        DisplayMessage("Would you like to add Description? (Y/N)");
        Console.Write("=> ");

        // Optionally get a description
        string sTaskDescription = ConfirmAction() ? GetTaskInput("task description :)") : "No description provided";

        // Create and add the new task
        tasks.Add(new TaskItem()
        {
            TaskId = tasks.Count + 1,
            TaskName = sTaskName,
            TaskDescription = sTaskDescription,
            IsCompleted = false
        });

        Console.Clear();
        ShowSuccessMessage(tasks); // Notify user of success
        PauseAndClear();

        bKeepAdding = RepeatAction("add"); // Ask if user wants to add another task
    }
}

// Get input from the user
string GetTaskInput(string inputType)
{
    DisplayMessage($"Enter the {inputType}: ");
    Console.Write("=> ");
    string sInput = Console.ReadLine() ?? "Not Valid";
    Console.Clear();
    return sInput;
}

// Display all tasks in the list
void DisplayTasks(List<TaskItem> tasks)
{
    if (tasks.Count <= 0)
    {
        DisplayMessage("=> No tasks found :(");
        PauseAndClear();
        return;
    }

    ShowTasks(tasks);
    PauseAndClear();
}

// Show each task with its completion status
void ShowTasks(List<TaskItem> list)
{
    DisplayMessage("Your Tasks: ");
    for (int i = 0; i < list.Count; i++)
        DisplayMessage($"{i + 1}- {list[i].TaskName}: {(list[i].IsCompleted ? "Is Completed" : "In Progress")}");
}

// Update the name of a specific task
void UpdateTaskName(List<TaskItem> tasks)
{
    if (tasks.Count <= 0)
    {
        DisplayMessage("=> No tasks found :(");
        PauseAndClear();
        return;
    }

    bool bKeepUpdating = true;

    while (bKeepUpdating)
    {
        PromptForTaskIndex(tasks); // Prompt user for task index

        if (!TryParseInput(out int nIndex))
        {
            DisplayMessage("Invalid number!");
            PauseAndClear();
            continue;
        }

        try
        {
            tasks[nIndex - 1].TaskName = GetTaskInput("new task name"); // Update the task name
            Console.Clear();

            ShowSuccessMessage(tasks);
        }
        catch (ArgumentOutOfRangeException)
        {
            DisplayMessage($"Enter number between (1 : {tasks.Count})");
        }
        finally
        {
            PauseAndClear();
        }

        bKeepUpdating = RepeatAction("update"); // Ask if user wants to update another task
    }
}

// Update the description of a specific task
void UpdateTaskDescription(List<TaskItem> tasks)
{
    if (tasks.Count <= 0)
    {
        DisplayMessage("=> No tasks found :(");
        PauseAndClear();
        return;
    }

    bool bKeepUpdating = true;
    while (bKeepUpdating)
    {
        PromptForTaskIndex(tasks);

        if (!TryParseInput(out int nIndex))
        {
            DisplayMessage("Invalid number!");
            PauseAndClear();
            continue;
        }

        try
        {
            tasks[nIndex - 1].TaskDescription = GetTaskInput("new task description"); // Update the description

            DisplayMessage("Action completed successfully :)");
        }
        catch (ArgumentOutOfRangeException)
        {
            DisplayMessage($"Please enter number between (1 : {tasks.Count})");
        }
        finally
        {
            PauseAndClear();
        }

        bKeepUpdating = RepeatAction("update");
    }
}

// Update the progress status of a task
void UpdateTaskProgress(List<TaskItem> tasks)
{
    if (tasks.Count <= 0)
    {
        DisplayMessage("=> No tasks found :(");
        PauseAndClear();
        return;
    }

    bool bKeepUpdating = true;
    while (bKeepUpdating)
    {
        PromptForTaskIndex(tasks);

        if (!TryParseInput(out int nIndex))
        {
            DisplayMessage("Invalid number!");
            PauseAndClear();
            continue;
        }

        try
        {
            DisplayMessage("Is the task completed? (Y/N)");
            tasks[nIndex - 1].IsCompleted = ConfirmAction(); // Update the completion status

            Console.Clear();
            ShowSuccessMessage(tasks);
        }
        catch (ArgumentOutOfRangeException)
        {
            DisplayMessage($"Please enter number between (1 : {tasks.Count}");
        }
        finally
        {
            PauseAndClear();
        }

        bKeepUpdating = RepeatAction("update");
    }
}

// View detailed information about a specific task
void ViewTaskDetails(List<TaskItem> tasks)
{
    if (tasks.Count <= 0)
    {
        DisplayMessage("=> No tasks found :(");
        PauseAndClear();
        return;
    }

    bool bKeepViewing = true;
    while (bKeepViewing)
    {
        PromptForTaskIndex(tasks);

        if (!TryParseInput(out int nIndex))
        {
            DisplayMessage("Invalid number!!");
            continue;
        }

        try
        {
            ShowTaskInfo(tasks, nIndex - 1);
            DisplayMessage("Done...");
        }
        catch (ArgumentOutOfRangeException)
        {
            DisplayMessage($"Please enter number between (1 : {tasks.Count}");
        }
        finally
        {
            PauseAndClear();
        }

        bKeepViewing = RepeatAction("view details of"); // Ask if user wants to view another task
    }
}

// Display task details
void ShowTaskInfo(List<TaskItem> tasks, int index)
{
    DisplayMessage("Task Details: ");
    DisplayMessage($"=> Task ID: {tasks[index].TaskId}");
    DisplayMessage($"=> Task Name: {tasks[index].TaskName}");
    DisplayMessage($"=> Task Progress: {(tasks[index].IsCompleted ? "Completed" : "In Progress")}");
    DisplayMessage($"=> Task Description: {tasks[index].TaskDescription}");
}

// Remove a task from the list
void RemoveTask(List<TaskItem> tasks)
{
    if (tasks.Count <= 0)
    {
        DisplayMessage("=> No tasks found :(");
        PauseAndClear();
        return;
    }

    bool bKeepRemoving = true;
    while (bKeepRemoving)
    {
        PromptForTaskIndex(tasks);

        if (!TryParseInput(out int nIndex))
        {
            DisplayMessage("Invalid number!");
            return;
        }

        try
        {
            tasks.RemoveAt(nIndex - 1); // Remove the task

            ReassignTaskIds(tasks); // Reassign task IDs to maintain sequence
            ShowSuccessMessage(tasks);
        }
        catch (ArgumentOutOfRangeException)
        {
            DisplayMessage($"Please enter number between (1 :{tasks.Count})");
        }
        finally
        {
            PauseAndClear();
        }

        bKeepRemoving = RepeatAction("remove");
    }
}

// Reassign task IDs to keep them sequential
void ReassignTaskIds(List<TaskItem> tasks)
{
    for (int i = 0; i < tasks.Count; i++)
        tasks[i].TaskId = i + 1;
}

// Clear all tasks from the list
void ClearAllTasks(List<TaskItem> tasks)
{
    DisplayMessage("Are you sure you want to clear all tasks? (Y/N)");
    Console.Write("=> ");

    if (!ConfirmAction())
    {
        DisplayMessage("Operation cancelled.");
        PauseAndClear();
        return;
    }
    tasks.Clear(); // Clear the task list

    DisplayMessage("All tasks cleared successfully.");
    PauseAndClear();
}

#endregion

#endregion