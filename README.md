# Task List Application

This console application allows users to manage a list of tasks efficiently. Users can add tasks, view their details, update task information, and remove tasks from the list.

## Features

- **Add Tasks:** Add new tasks with an optional description.
- **View Tasks:** View all tasks along with their progress (Completed/In Progress).
- **Update Tasks:** Update the name, description, or progress of existing tasks.
- **View Task Details:** View detailed information about a specific task.
- **Remove Tasks:** Remove a specific task from the list.
- **Clear All Tasks:** Remove all tasks from the list with an option to create a backup.

## How to Use

1. Run the application.
2. Choose an option from the menu by entering the corresponding number.
3. Follow the prompts to perform the desired action.

## Code Structure

### `StartApplication`

Initializes and starts the application, presenting the user with a menu to manage tasks.

### Methods

- **Helper Methods:**

  - `DisplayMessage`: Displays messages to the user.
  - `TryParseInput`: Parses user input into an integer.
  - `ConfirmAction`: Validates yes/no responses.
  - `PauseAndClear`: Pauses for user acknowledgment and clears the console.
  - `ShowSuccessMessage`: Displays a success message and the updated task list.

- **Task Management Methods:**
  - `AddTask`: Adds a new task with optional description.
  - `DisplayTasks`: Displays all tasks in the list.
  - `UpdateTaskName`: Updates the name of a specific task.
  - `UpdateTaskDescription`: Updates the description of a specific task.
  - `UpdateTaskProgress`: Updates the completion status of a task.
  - `ViewTaskDetails`: Displays detailed information about a specific task.
  - `RemoveTask`: Removes a specific task from the list.
  - `ClearAllTasks`: Clears all tasks from the list, with an option to create a backup.

## Example Usage

```plaintext
Welcome to the Task List Application
Please choose an option:
1- Add a new task
2- View tasks and their progress
3- Update task name
4- Update task description
5- Update task progress
6- View task details
7- Remove a task
8- Clear all tasks
=>
```

Enter the corresponding number for your desired action and follow the prompts.

## Requirements

- .NET runtime
