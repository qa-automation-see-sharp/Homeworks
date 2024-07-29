namespace LINQHomework.Models;

public class Department
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Budget { get; set; }
    public List<string> Employees { get; set; }
}