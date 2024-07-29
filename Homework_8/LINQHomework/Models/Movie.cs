namespace LINQHomework.Models;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Genre { get; set; }
    public double Rating { get; set; }
    public int Duration { get; set; }
    public List<string> Directors { get; set; }
}