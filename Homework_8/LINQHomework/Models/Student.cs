namespace LINQHomework.Models;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public int Grade { get; set; }
    public List<string> Subjects { get; set; }
}