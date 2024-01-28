using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        
        Scripture scripture = new Scripture("John 3:16", "For God so loved the world...");

        
        Console.Clear();
        scripture.Display();

        while (true)
        {
            Console.WriteLine("\nPress Enter to hide words, or type 'quit' to exit:");
            string input = Console.ReadLine();

            if (input.ToLower() == "quit")
                break;

            
            scripture.HideRandomWords();

            
            Console.Clear();
            scripture.Display();
            
            
            if (scripture.AllWordsHidden())
            {
                Console.WriteLine("\nAll words are hidden. Program ending.");
                break;
            }
        }
    }
}


class Word
{
    public string Text { get; set; }
    public bool Hidden { get; set; }

    public Word(string text)
    {
        Text = text;
        Hidden = false;
    }
}


class Reference
{
    public string Verse { get; set; }

    public Reference(string verse)
    {
        Verse = verse;
    }
}


class Scripture
{
    private Reference reference;
    private List<Word> words;

    public Scripture(string verse, string text)
    {
        reference = new Reference(verse);
        words = text.Split(' ').Select(word => new Word(word)).ToList();
    }

    public void Display()
    {
        Console.WriteLine($"Reference: {reference.Verse}\n");

        foreach (var word in words)
        {
            if (word.Hidden)
                Console.Write("_____ ");
            else
                Console.Write($"{word.Text} ");
        }
    }

    public void HideRandomWords()
    {
        Random random = new Random();
        int wordsToHide = random.Next(1, words.Count / 2); 

        for (int i = 0; i < wordsToHide; i++)
        {
            int index = random.Next(words.Count);
            words[index].Hidden = true;
        }
    }

    public bool AllWordsHidden()
    {
        return words.All(word => word.Hidden);
    }
}
