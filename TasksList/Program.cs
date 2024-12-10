using TasksList;

Start();

void Start()
{
    // welcome message
    PrintMessage("Welcome to Tasks List Application");
    var tasks = new List<TaskItem>();

    while (true)
    {
        PrintMessage("Please Choose one of them: ");
        PrintMessage("""
                     1- Add new task
                     2- Show your tasks and progress
                     3- Edit task name
                     4- Edit task description
                     5- Edit Progress
                     6- Tasks information
                     7- Remove task from the list
                     8- Clear all tasks 
                     """);
        Console.Write("=> ");
        
        // if user insert not valid number warn him
        if (!CheckIsConverted(out int nNumberOfFeature))
        {
            PrintMessage("Please enter valid number!!");
            Console.Clear();
            continue;
        }
        
        Console.Clear();
        RunFeatures(tasks, nNumberOfFeature);
    }
}


#region Methods

#region Run Features

void RunFeatures(List<TaskItem> list, int index)
{
    switch (index)
    {
        case 1:
            AddNewTask(list);
            break;
        case 2:
            ShowTasksFeature(list);
            break;
        case 3:
            EditTaskName(list);
            break;
        case 4:
            EditDescription(list);
            break;
        case 5:
            EditProgress(list);
            break;
        case 6:
            TasksInformation(list);
            break;
        case 7:
            ClearTask(list);
            break;
        case 8:
            ClearAll(list);
            break;
        default:
            PrintMessage("Please inter number from (1 : 8)");
            break;
    }
}

#endregion

#region Public
// Print Message for user
void PrintMessage(string message)
{
    Console.WriteLine(message);
}

// get number from user and check if it's valid or not (return bool)
bool CheckIsConverted (out int number)
{
    bool bIsConvert = int.TryParse(Console.ReadLine(), out number);
    Console.Clear();
    return bIsConvert;
}

// handle if user accept or not 
bool HandleAccepted()
{
    string sAnswer = Console.ReadLine() ?? "No";

    switch (sAnswer)
    {
        case "Yes": case "Y": case "YES": case "yes": case "y":
            Console.Clear();
            return true;
        default:
            Console.Clear();
            return false;
    }
}

// Method to clear console
void ClearConsole()
{
    PrintMessage("Press any key :)"); // ask him to press any key to get back
    Console.ReadKey();
    Console.Clear();
}

// Success Message 
void Success(List<TaskItem> list)
{
    PrintMessage("Success :)");
    ShowTasks(list); // show all tasks
}

void AskIndex(List<TaskItem> list)
{
    PrintMessage("Please enter task number: ");
    ShowTasks(list);
    Console.Write("=> ");
}

bool RunAgain(string happened)
{
    PrintMessage($"Do you want to {happened} another task? (Y/N)");
    Console.Write("=> ");

    return HandleAccepted();
}
#endregion

#region Add Task Methods
// method for Add task into the list
void AddNewTask(List<TaskItem> list)
{
    bool bIsValid = true;
    
    while (bIsValid)
    {
        string sTaskName, sTaskDescription;

        // ask user to enter task name 
        sTaskName = HandleTask("task name");
        PrintMessage("Do you want to add Description? (Y/N)"); // Ask him if user want to add description or not 
        Console.Write("=> ");
    
        // if user add desc initialize the variable by this input if no this default val
        sTaskDescription = HandleAccepted() ? HandleTask("task description :)") : "No Description Found";
    
        // after that add the information to the class and insert it to list 
        list.Add(new TaskItem()
        {
            Id = list.Count + 1,
            Name = sTaskName,
            Description = sTaskDescription,
            Completed = false
        });
    
        // tell user that is success
        Console.Clear();
        Success(list);
    
        ClearConsole();

        bIsValid = RunAgain("add");
    }
}

// Handle the task name and description
string HandleTask(string complete)
{
    PrintMessage($"Please enter {complete}: ");
    Console.Write("=> ");
    string sTaskNd = Console.ReadLine() ?? "Not Valid";
    Console.Clear();
    return sTaskNd;
}

#endregion

#region Show Tasks

void ShowTasksFeature(List<TaskItem> list)
{
    if (!CheckIsContain(list))
    {
        PrintMessage("=> No tasks founded in the list :(");
        ClearConsole();
        return;
    }

    ShowTasks(list);
    ClearConsole();
}

bool CheckIsContain(List<TaskItem> list)
{
    return list.Count > 0;
}

// Show tasks to user 
void ShowTasks(List<TaskItem> list)
{
    PrintMessage("Your Tasks: ");
    for(int i =0; i < list.Count; i++)
        PrintMessage($"{i + 1}- {list[i].Name}: {(list[i].Completed ? "Is Completed" : "In Progress")}");
}

#endregion

#region Edit Task Name

void EditTaskName(List<TaskItem> list)
{
    if (!CheckIsContain(list))
    {
        PrintMessage("=> No tasks founded in the list :(");
        ClearConsole();
        return;
    }
    
    bool bIsValid = true;

    while (bIsValid)
    {
        AskIndex(list);

        if (!CheckIsConverted(out int nIndex))
        {
            PrintMessage("Please enter valid number!!");
            ClearConsole();
            return;
        }

        try
        {
            list[nIndex - 1].Name = HandleTask("new task name");
            Console.Clear();

            Success(list);
        }
        catch (ArgumentOutOfRangeException)
        {
            PrintMessage($"Please enter number between (1 : {list.Count})");
        }
        finally
        {
            ClearConsole();
        }

        bIsValid = RunAgain("edit");
    }
}

#endregion

#region Edit Task Description

void EditDescription(List<TaskItem> list)
{
    if (!CheckIsContain(list))
    {
        PrintMessage("=> No tasks founded in the list :(");
        ClearConsole();
        return;
    }

    bool bIsValid = true;
    while (bIsValid)
    {
        AskIndex(list);

        if (!CheckIsConverted(out int nIndex))
        {
            PrintMessage("Please enter valid number!!");
            ClearConsole();
            return;
        }

        try
        {
            list[nIndex - 1].Description = HandleTask("new description");

            PrintMessage("Success :)");
        }
        catch (ArgumentOutOfRangeException)
        {
            PrintMessage($"Please enter number between (1 : {list.Count})");
        }
        finally
        {
            ClearConsole();
        }

        bIsValid = RunAgain("edit");
    }
}

#endregion

#region Edit Task Progress

void EditProgress(List<TaskItem> list)
{
    if (!CheckIsContain(list))
    {
        PrintMessage("=> No tasks founded in the list :(");
        ClearConsole();
        return;
    }

    bool bIsValid = true;
    while (bIsValid)
    {
        AskIndex(list);

        if (!CheckIsConverted(out int nIndex))
        {
            PrintMessage("Please enter valid number!!");
            ClearConsole();
            return;
        }

        try
        {
            PrintMessage("The task is completed? (Y/N)");
            list[nIndex - 1].Completed = HandleAccepted();

            Console.Clear();
            Success(list);
        }
        catch (ArgumentOutOfRangeException)
        {
            PrintMessage($"Please enter number between (1 : {list.Count}");
        }
        finally
        {
            ClearConsole();
        }

        bIsValid = RunAgain("edit");
    }
}

#endregion

#region Task Information

void TasksInformation(List<TaskItem> list)
{
    if (!CheckIsContain(list))
    {
        PrintMessage("=> No tasks founded in the list :(");
        ClearConsole();
        return;
    }

    bool bIsValid = true;
    while (bIsValid)
    {
        AskIndex(list);

        if (!CheckIsConverted(out int nIndex))
        {
            PrintMessage("Please enter valid number!!");
            return;
        }
    
        try
        {
            ShowTaskInfo(list, nIndex - 1);
            PrintMessage("Done...");
        }
        catch (ArgumentOutOfRangeException)
        {
            PrintMessage($"Please enter number between (1 : {list.Count}");
        }
        finally
        {
            ClearConsole();
        }

        bIsValid = RunAgain("show information about ");
    }
}

void ShowTaskInfo(List<TaskItem> list, int index)
{
    PrintMessage("Task Information: ");
    PrintMessage($"=> Task ID: {list[index].Id}");
    PrintMessage($"=> Task Name: {list[index].Name}");
    PrintMessage($"=> Task Progress: {(list[index].Completed ? "Completed" : "In Progress")}");
    PrintMessage($"=> Task Description: {list[index].Description}");
}

#endregion

#region Clear Task

void ClearTask(List<TaskItem> list)
{
    if (!CheckIsContain(list))
    {
        PrintMessage("=> No tasks founded in the list :(");
        ClearConsole();
        return;
    }

    bool bIsValid = true;
    while (bIsValid)
    {
        AskIndex(list);
    
        if (!CheckIsConverted(out int nIndex))
        {
            PrintMessage("Please enter valid number!!");
            return;
        }

        try
        {
            list.RemoveAt(nIndex - 1);
        
            SetId(list);
            Success(list);
        }
        catch (ArgumentOutOfRangeException)
        {
            PrintMessage($"Please enter number between (1 :{list.Count})");
        }
        finally
        {
            ClearConsole();
        }

        bIsValid = RunAgain("remove");
    }
}

void SetId(List<TaskItem> list)
{
    for (int i = 0; i < list.Count; i++)
        list[i].Id = i + 1;
}

#endregion

#region CLear All Tasks

void ClearAll(List<TaskItem> list)
{
    PrintMessage("Are your sure? (Y/N)");
    Console.Write("=> ");

    if (!HandleAccepted())
    {
        PrintMessage("Okay :)");
        ClearConsole();
        return;
    }
    list.Clear();
    
    PrintMessage("Success :)");
    ClearConsole();
}

#endregion
#endregion