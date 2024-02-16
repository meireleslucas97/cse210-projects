using System;
using System.Collections.Generic;

class Comment
{
    public string CommenterName { get; set; }
    public string Text { get; set; }
}

class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int DurationInSeconds { get; set; }
    public List<Comment> Comments { get; set; } = new List<Comment>();

    public int GetNumberOfComments()
    {
        return Comments.Count;
    }
}

class Program
{
    static void Main()
    {
        // Criando vídeos
        Video video1 = new Video
        {
            Title = "Video 1",
            Author = "Author 1",
            DurationInSeconds = 300 // Exemplo de duração de 5 minutos
        };

        Video video2 = new Video
        {
            Title = "Video 2",
            Author = "Author 2",
            DurationInSeconds = 240 // Exemplo de duração de 4 minutos
        };

        Video video3 = new Video
        {
            Title = "Video 3",
            Author = "Author 3",
            DurationInSeconds = 180 // Exemplo de duração de 3 minutos
        };

        // Adicionando comentários aos vídeos
        video1.Comments.Add(new Comment { CommenterName = "User1", Text = "Great video!" });
        video1.Comments.Add(new Comment { CommenterName = "User2", Text = "I learned a lot." });
        video1.Comments.Add(new Comment { CommenterName = "User3", Text = "Nice work!" });

        video2.Comments.Add(new Comment { CommenterName = "User4", Text = "Interesting content." });
        video2.Comments.Add(new Comment { CommenterName = "User5", Text = "Could you make more videos like this?" });

        video3.Comments.Add(new Comment { CommenterName = "User6", Text = "Short and sweet." });
        video3.Comments.Add(new Comment { CommenterName = "User7", Text = "Well explained." });

        // Criando uma lista de vídeos
        List<Video> videos = new List<Video> { video1, video2, video3 };

        // Exibindo informações sobre cada vídeo
        foreach (var video in videos)
        {
            Console.WriteLine($"Title: {video.Title}");
            Console.WriteLine($"Author: {video.Author}");
            Console.WriteLine($"Duration: {video.DurationInSeconds} seconds");
            Console.WriteLine($"Number of comments: {video.GetNumberOfComments()}");

            Console.WriteLine("Comments:");
            foreach (var comment in video.Comments)
            {
                Console.WriteLine($"{comment.CommenterName}: {comment.Text}");
            }

            Console.WriteLine();
        }
    }
}
