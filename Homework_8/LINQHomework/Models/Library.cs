namespace LINQHomework.Models;

public class Library
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int BooksCount { get; set; }
    public List<string> Sections { get; set; }
}