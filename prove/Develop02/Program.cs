using System;
using System.Collections.Generic;
using System.IO;


class JournalEntry
{
    public string Prompt { get; set; }
    public string Response { get; set; }
    public string Date { get; set; }

    public JournalEntry(string prompt, string response, string date)
    {
        Prompt = prompt;
        Response = response;
        Date = date;
    }

    
    public override string ToString()
    {
        return $"Date: {Date}\nPrompt: {Prompt}\nResponse: {Response}\n";
    }
}


class JournalManager
{
    private List<JournalEntry> entries;

    public JournalManager()
    {
        entries = new List<JournalEntry>();
    }

    
    public void AddEntry(string prompt, string response, string date)
    {
        JournalEntry entry = new JournalEntry(prompt, response, date);
        entries.Add(entry);
    }

    
    public void DisplayJournal()
    {
        foreach (var entry in entries)
        {
            Console.WriteLine(entry);
        }
    }

    
    public void SaveToFile(string fileName)
    {
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            foreach (var entry in entries)
            {
                writer.WriteLine($"{entry.Date},{entry.Prompt},{entry.Response}");
            }
        }
    }

    // Load the journal from a file
    public void LoadFromFile(string fileName)
    {
        entries.Clear(); 
        try
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                while (!reader.EndOfStream)
                {
                    string[] entryData = reader.ReadLine().Split(',');
                    if (entryData.Length == 3)
                    {
                        entries.Add(new JournalEntry(entryData[1], entryData[2], entryData[0]));
                    }
                }
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("File not found. Creating a new journal.");
        }
    }
}


class Program
{
    static void Main()
    {
        JournalManager journalManager = new JournalManager();

        while (true)
        {
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice: ");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    
                    string[] prompts = { "Who was the most interesting person I interacted with today?",
                                         "What was the best part of my day?",
                                         "How did I see the hand of the Lord in my life today?",
                                         "What was the strongest emotion I felt today?",
                                         "If I had one thing I could do over today, what would it be?" };

                    string randomPrompt = prompts[new Random().Next(prompts.Length)];

                    Console.WriteLine($"Prompt: {randomPrompt}");
                    Console.Write("Enter your response: ");
                    string response = Console.ReadLine();
                    string date = DateTime.Now.ToString("yyyy-MM-dd");

                    
                    journalManager.AddEntry(randomPrompt, response, date);
                    break;

                case 2:
                    
                    journalManager.DisplayJournal();
                    break;

                case 3:
                    
                    Console.Write("Enter the filename to save: ");
                    string saveFileName = Console.ReadLine();
                    journalManager.SaveToFile(saveFileName);
                    Console.WriteLine("Journal saved successfully.");
                    break;

                case 4:
                    
                    Console.Write("Enter the filename to load: ");
                    string loadFileName = Console.ReadLine();
                    journalManager.LoadFromFile(loadFileName);
                    Console.WriteLine("Journal loaded successfully.");
                    break;

                case 5:
                    
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}