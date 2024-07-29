namespace LINQHomework.Models;

public class Course
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Instructor { get; set; }
    public int Credits { get; set; }
    public List<string> Students { get; set; }
    public List<string> Topics { get; set; }
}