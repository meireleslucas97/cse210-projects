using System;
using System.Collections.Generic;

public abstract class Goal
{
    public string ShortName { get; protected set; }
    public string Description { get; protected set; }
    public int Points { get; protected set; }
    public bool IsComplete { get; set; }

    public Goal(string name, string description, int points)
    {
        ShortName = name;
        Description = description;
        Points = points;
        IsComplete = false;
    }

    public abstract void RecordEvent();
    public abstract bool CheckCompletion();
    public abstract string GetDetailsString();
    public abstract string GetStringRepresentation();
}

public class SimpleGoal : Goal
{
    public SimpleGoal(string name, string description, int points) : base(name, description, points)
    {
    }

    public override void RecordEvent()
    {
        IsComplete = true;
    }

    public override bool CheckCompletion()
    {
        return IsComplete;
    }

    public override string GetDetailsString()
    {
        return $"[{(IsComplete ? "X" : " ")}] {ShortName}: ({Description})";
    }

    public override string GetStringRepresentation()
    {
        return $"{ShortName}|{Description}|{Points}|{IsComplete}";
    }
}

public class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points) : base(name, description, points)
    {
    }

    public override void RecordEvent()
    {
        
    }

    public override bool CheckCompletion()
    {
        return IsComplete;
    }

    public override string GetDetailsString()
    {
        return $"[ ] {ShortName}: ({Description})";
    }

    public override string GetStringRepresentation()
    {
        return $"{ShortName}|{Description}|{Points}|{IsComplete}";
    }
}

public class ChecklistGoal : Goal
{
    private int _amountCompleted;
    private int _target;
    private int _bonus;

    public ChecklistGoal(string name, string description, int points, int target, int bonus) : base(name, description, points)
    {
        _amountCompleted = 0;
        _target = target;
        _bonus = bonus;
    }

    public override void RecordEvent()
    {
        _amountCompleted++;
        if (_amountCompleted == _target)
        {
            Points += _bonus; // Apply bonus when the target is reached
            IsComplete = true;
        }
    }

    public override bool CheckCompletion()
    {
        return IsComplete;
    }

    public override string GetDetailsString()
    {
        return $"[{_amountCompleted}/{_target}] {ShortName}: ({Description})";
    }

    public override string GetStringRepresentation()
    {
        return $"{ShortName}|{Description}|{Points}|{IsComplete}|{_amountCompleted}|{_target}|{_bonus}";
    }
}

public class GoalManager
{
    private List<Goal> _goals;
    private int _score;

    private const string GoalsFilePath = "goals.txt";

    public GoalManager()
    {
        _goals = LoadGoalsFromFile();
        _score = 0;
    }

    private List<Goal> LoadGoalsFromFile()
    {
        List<Goal> loadedGoals = new List<Goal>();

        if (System.IO.File.Exists(GoalsFilePath))
        {
            try
            {
                string[] lines = System.IO.File.ReadAllLines(GoalsFilePath);

                foreach (string line in lines)
                {
                    Goal loadedGoal = CreateGoalFromLine(line);
                    if (loadedGoal != null)
                    {
                        loadedGoals.Add(loadedGoal);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading goals from file: {ex.Message}");
            }
        }

        return loadedGoals;
    }

    private void SaveGoals()
    {
        try
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(GoalsFilePath, true))
            {
                foreach (Goal goal in _goals)
                {
                    file.WriteLine(goal.GetStringRepresentation());
                }
            }

            Console.WriteLine("Goals saved to file.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving goals to file: {ex.Message}");
        }
    }

    private Goal CreateGoalFromLine(string line)
    {
        string[] parts = line.Split('|');

        if (parts.Length >= 4)
        {
            string name = parts[0];
            string description = parts[1];
            int points = int.Parse(parts[2]);
            bool isComplete = bool.Parse(parts[3]);

            if (parts.Length == 4)
            {
                return new SimpleGoal(name, description, points) { IsComplete = isComplete };
            }
            else if (parts.Length == 7)
            {
                int amountCompleted = int.Parse(parts[4]);
                int target = int.Parse(parts[5]);
                int bonus = int.Parse(parts[6]);
                return new ChecklistGoal(name, description, points, target, bonus) { IsComplete = isComplete };
            }
        }

        return null;
    }

    public void Start()
    {
        int choice;
        do
        {
            Console.WriteLine("Menu Options:");
            Console.WriteLine("1. Create New Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Save Goals");
            Console.WriteLine("4. Load Goals");
            Console.WriteLine("5. Record Event");
            Console.WriteLine("6. Quit");

            choice = GetUserInputInt("Enter your choice: ");

            switch (choice)
            {
                case 1:
                    CreateGoal();
                    break;
                case 2:
                    ListGoals();
                    break;
                case 3:
                    SaveGoals();
                    break;
                case 4:
                    _goals = LoadGoalsFromFile();  // Update the goals list after loading from file
                    break;
                case 5:
                    RecordEvent();
                    break;
                case 6:
                    Console.WriteLine("Quitting the program. Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please enter a valid option.");
                    break;
            }

        } while (choice != 6);
    }

    private void CreateGoal()
    {
        Console.WriteLine("Enter goal details:");
        Console.Write("Name: ");
        string name = Console.ReadLine();
        Console.Write("Description: ");
        string description = Console.ReadLine();
        Console.Write("Points: ");
        int points = GetUserInputInt("");

        Console.WriteLine("Select goal type:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");

        int goalTypeChoice = GetUserInputInt("");

        switch (goalTypeChoice)
        {
            case 1:
                _goals.Add(new SimpleGoal(name, description, points));
                break;
            case 2:
                _goals.Add(new EternalGoal(name, description, points));
                break;
            case 3:
                Console.Write("Target: ");
                int target = GetUserInputInt("");
                Console.Write("Bonus: ");
                int bonus = GetUserInputInt("");
                _goals.Add(new ChecklistGoal(name, description, points, target, bonus));
                break;
            default:
                Console.WriteLine("Invalid choice. Creating a Simple Goal by default.");
                _goals.Add(new SimpleGoal(name, description, points));
                break;
        }

        Console.WriteLine($"Goal '{name}' created successfully.");
    }

    private void ListGoals()
    {
        Console.WriteLine("List of Goals:");

        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
        }
    }

    private void RecordEvent()
    {
        Console.WriteLine("Select a goal to record an event for:");

        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
        }

        int selectedGoalIndex = GetUserInputInt("Enter the number of the goal: ") - 1;

        if (selectedGoalIndex >= 0 && selectedGoalIndex < _goals.Count)
        {
            Goal selectedGoal = _goals[selectedGoalIndex];

            if (!selectedGoal.CheckCompletion())
            {
                selectedGoal.RecordEvent();
                _score += selectedGoal.Points;
                Console.WriteLine($"Event recorded for {selectedGoal.ShortName}. You earned {selectedGoal.Points} points.");
            }
            else
            {
                Console.WriteLine($"{selectedGoal.ShortName} is already marked as complete.");
            }
        }
        else
        {
            Console.WriteLine("Invalid goal selection.");
        }
    }

    private static int GetUserInputInt(string prompt)
    {
        int userInput;
        bool isValidInput;

        do
        {
            Console.Write(prompt);
            isValidInput = int.TryParse(Console.ReadLine(), out userInput);

            if (!isValidInput)
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        } while (!isValidInput);

        return userInput;
    }

    public static void Main()
    {
        GoalManager goalManager = new GoalManager();
        goalManager.Start();
    }
}


//FINALLY DONE!!!!!!!!!!! /YAYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYY \O/