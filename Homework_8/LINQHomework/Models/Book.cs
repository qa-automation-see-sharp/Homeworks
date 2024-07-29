namespace LINQHomework.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public decimal Price { get; set; }
    
    public int Pages { get; set; }
    public List<Review> Reviews { get; set; }
}