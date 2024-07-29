namespace LINQHomework.Models;

public class City
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Population { get; set; }
    public List<string> Landmarks { get; set; }
}