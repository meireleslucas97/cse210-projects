using System;
using System.Collections.Generic;
using System.Threading;

class Activity
{
    protected int duration;

    public Activity(int duration)
    {
        this.duration = duration;
    }

    public virtual void StartActivity(string name, string description)
    {
        Console.WriteLine($"{name} Activity - {description}");
        Console.WriteLine($"Set duration for {duration} seconds.");
        PrepareToBegin();
    }

    public virtual void EndActivity(string name)
    {
        Console.WriteLine($"Good job! You have completed the {name} activity for {duration} seconds.");
        Thread.Sleep(2000); 
    }

    protected void PrepareToBegin()
    {
        Console.WriteLine("Prepare to begin...");
        for (int i = 3; i > 0; i--)
        {
            Console.Write($"{i} ");
            Thread.Sleep(1000); 
        }
        Console.WriteLine();
        Thread.Sleep(2000); 
    }
}


class BreathingActivity : Activity
{
    public BreathingActivity(int duration) : base(duration) { }

    public override void StartActivity(string name, string description)
    {
        base.StartActivity(name, description);
        Console.WriteLine("This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.");

        for (int i = 0; i < duration; i++)
        {
            Console.WriteLine("Breathe in...");
            ShowSpinner();
            Thread.Sleep(1000); 

            Console.WriteLine("Breathe out...");
            ShowSpinner();
            Thread.Sleep(1000); 
        }

        EndActivity(name);
    }

    private void ShowSpinner()
    {
        Console.Write("   ");
        for (int i = 0; i < 3; i++)
        {
            Console.Write(".");
            Thread.Sleep(500); 
        }
        Console.WriteLine();
    }
}


class ReflectionActivity : Activity
{
    private List<string> prompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private List<string> questions = new List<string>
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    public ReflectionActivity(int duration) : base(duration) { }

    public override void StartActivity(string name, string description)
    {
        base.StartActivity(name, description);
        Random random = new Random();

        for (int i = 0; i < duration; i++)
        {
            string prompt = prompts[random.Next(prompts.Count)];
            Console.WriteLine(prompt);
            ShowSpinner();
            Thread.Sleep(1000); 

            foreach (var question in questions)
            {
                Console.WriteLine(question);
                ShowSpinner();
                Thread.Sleep(1000); 
            }
        }

        EndActivity(name);
    }

    private void ShowSpinner()
    {
        Console.Write("   ");
        for (int i = 0; i < 3; i++)
        {
            Console.Write(".");
            Thread.Sleep(500); 
        }
        Console.WriteLine();
    }
}


class ListingActivity : Activity
{
    private List<string> prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity(int duration) : base(duration) { }

    public override void StartActivity(string name, string description)
    {
        base.StartActivity(name, description);
        Random random = new Random();

        string prompt = prompts[random.Next(prompts.Count)];
        Console.WriteLine(prompt);
        Console.WriteLine("Get ready to list...");

        for (int i = 3; i > 0; i--)
        {
            Console.Write($"{i} ");
            Thread.Sleep(1000); 
        }
        Console.WriteLine();

        
        Console.WriteLine("List as many items as you can...");
        Thread.Sleep(duration * 1000); 

        Console.WriteLine($"You listed {duration} items!");
        EndActivity(name);
    }
}


class StretchingActivity : Activity
{
    public StretchingActivity(int duration) : base(duration) { }

    public override void StartActivity(string name, string description)
    {
        base.StartActivity(name, description);
        Console.WriteLine("This activity will help you relax and improve flexibility. Follow the instructions for each stretch.");

        
        for (int i = 0; i < duration; i++)
        {
            Console.WriteLine("Stretch your arms overhead...");
            ShowSpinner();
            Thread.Sleep(1000); 

            Console.WriteLine("Touch your toes...");
            ShowSpinner();
            Thread.Sleep(1000); 
        }

        EndActivity(name);
    }

    private void ShowSpinner()
    {
        Console.Write("   ");
        for (int i = 0; i < 3; i++)
        {
            Console.Write(".");
            Thread.Sleep(500); 
        }
        Console.WriteLine();
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Welcome to the Relaxation Program!");

        while (true)
        {
            Console.WriteLine("Choose an activity:");
            Console.WriteLine("1. Breathing");
            Console.WriteLine("2. Reflection");
            Console.WriteLine("3. Listing");
            Console.WriteLine("4. Stretching (Bonus)");
            Console.WriteLine("5. Quit");

            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        StartActivity(new BreathingActivity(GetDuration()));
                        break;
                    case 2:
                        StartActivity(new ReflectionActivity(GetDuration()));
                        break;
                    case 3:
                        StartActivity(new ListingActivity(GetDuration()));
                        break;
                    case 4:
                        StartActivity(new StretchingActivity(GetDuration()));
                        break;
                    case 5:
                        Environment.Exit(0); 
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please choose a valid activity.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }
        }
    }

    static void StartActivity(Activity activity)
    {
        activity.StartActivity(activity.GetType().Name, "Description placeholder");
    }

    static int GetDuration()
    {
        Console.WriteLine("Enter the duration in seconds:");
        int duration;
        while (!int.TryParse(Console.ReadLine(), out duration) || duration <= 0)
        {
            Console.WriteLine("Invalid input. Please enter a positive integer for the duration.");
        }
        return duration;
    }
}
