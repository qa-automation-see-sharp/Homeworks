using System.Reflection;
using LINQHomework.Models;

namespace LINQHomework;

[TestFixture]
public class ArraysLinq : StartUpFixture
{
    /* Task 1:
     * Get the names of students who have "Math" as one of their subjects,
     * ordered by their grades in descending order.
     */
    [Test]
    public void First()
    {
        // Collection of students 
        var students = new Student[]
        {
            new Student { Id = 1, Name = "Alice", Age = 20, Grade = 85, Subjects = new List<string> { "Math", "Science" }},
            new Student { Id = 2, Name = "Bob", Age = 22, Grade = 90, Subjects = new List<string> { "Math", "History" }},
            new Student { Id = 3, Name = "Charlie", Age = 23, Grade = 80, Subjects = new List<string> { "English", "Science" }},
            new Student { Id = 4, Name = "David", Age = 21, Grade = 88, Subjects = new List<string> { "Math", "Science" }},
            new Student { Id = 5, Name = "Eve", Age = 22, Grade = 92, Subjects = new List<string> { "History", "Science" }}
        };
        
        // Query
        var newSortedArray = students
                            .Where(s => s.Subjects.Contains("Math"))
                            .OrderByDescending(s => s.Grade)
                            .ToArray();
        
        // Assert your query
        Assert.Multiple(() =>
        {
            Assert.That(newSortedArray, Has.Length.EqualTo(3));
            Assert.That(newSortedArray[0].Name, Is.EqualTo("Bob"));
            Assert.That(newSortedArray[1].Name, Is.EqualTo("David"));
            Assert.That(newSortedArray[2].Name, Is.EqualTo("Alice"));
        });
    }
    
    /* Task 2:
     * Get the names of products that belong to the "Computers" category,
     * ordered by price in ascending order.
     */
    [Test]
    public void Second()
    {
        // Collection of products
        var products = new Product[]
        {
            new Product { Id = 1, Name = "Laptop", Price = 1000, Quantity = 5, Categories = new List<string> { "Electronics", "Computers" }},
            new Product { Id = 2, Name = "Smartphone", Price = 800, Quantity = 10, Categories = new List<string> { "Electronics", "Mobile" }},
            new Product { Id = 3, Name = "Tablet", Price = 600, Quantity = 15, Categories = new List<string> { "Electronics", "Mobile" }},
            new Product { Id = 4, Name = "Monitor", Price = 200, Quantity = 20, Categories = new List<string> { "Electronics", "Computers" }},
            new Product { Id = 5, Name = "Keyboard", Price = 50, Quantity = 50, Categories = new List<string> { "Computers", "Accessories" }}
        };
        
        // Query: 
        var newSortedArray = products
                            .Where(p => p.Categories.Contains("Computers"))
                            .OrderBy(p => p.Price)
                            .ToArray();
        
        // Assert your query
        Assert.Multiple(() =>
        {
            Assert.That(newSortedArray, Has.Length.EqualTo(3));
            Assert.That(newSortedArray[0].Name, Is.EqualTo("Keyboard"));
            Assert.That(newSortedArray[1].Name, Is.EqualTo("Monitor"));
            Assert.That(newSortedArray[2].Name, Is.EqualTo("Laptop"));
        });
    }

    /* Task 3:
     * Get the names of employees who have "Programming" as a skill, ordered by salary in descending order.
     */
    [Test]
    public void Third()
    {
        // Collection of employees
        var employees = new Employee[]
        {
            new Employee { Id = 1, Name = "John", Department = "HR", Salary = 50000, Skills = new List<string> { "Communication", "Recruitment" }},
            new Employee { Id = 2, Name = "Jane", Department = "IT", Salary = 60000, Skills = new List<string> { "Programming", "Networking" }},
            new Employee { Id = 3, Name = "Doe", Department = "Finance", Salary = 55000, Skills = new List<string> { "Accounting", "Excel" }},
            new Employee { Id = 4, Name = "Smith", Department = "IT", Salary = 70000, Skills = new List<string> { "Programming", "Security" }},
            new Employee { Id = 5, Name = "Emily", Department = "HR", Salary = 48000, Skills = new List<string> { "Communication", "Training" }}
        };
        
        // Query
        string[] newSortedArray = employees
                                .Where(e => e.Skills.Contains("Programming"))
                                .OrderByDescending(e => e.Salary)
                                .Select(e => e.Name)
                                .ToArray();
        
        // Assert your query
        Assert.Multiple(() =>
        {
            Assert.That(newSortedArray, Has.Length.EqualTo(2));
            Assert.That(newSortedArray[0], Is.EqualTo("Smith"));
            Assert.That(newSortedArray[1], Is.EqualTo("Jane"));
        });
    }
    
    /* Task 4:
     * Get the names of customers who ordered a "Laptop", ordered by total amount in ascending order.
     */
    [Test]
    public void Fourth()
    {
        // Collection of orders
        var orders = new Order[]
        {
            new Order { Id = 1, CustomerName = "Alice", TotalAmount = 120, Items = new List<string> { "Laptop", "Mouse" }},
            new Order { Id = 2, CustomerName = "Bob", TotalAmount = 150, Items = new List<string> { "Smartphone", "Charger" }},
            new Order { Id = 3, CustomerName = "Charlie", TotalAmount = 200, Items = new List<string> { "Tablet", "Case" }},
            new Order { Id = 4, CustomerName = "David", TotalAmount = 90, Items = new List<string> { "Monitor", "Keyboard" }},
            new Order { Id = 5, CustomerName = "Eve", TotalAmount = 110, Items = new List<string> { "Laptop", "Bag" }}
        };
        
        // Finish query
        string[] newSortedArray = orders
                                .Where(o => o.Items.Contains("Laptop"))
                                .OrderBy(o => o.TotalAmount)
                                .Select(o => o.CustomerName)
                                .ToArray();
        
        // Assert your query
        Assert.Multiple(() =>
        {
            Assert.That(newSortedArray, Has.Length.EqualTo(2));
            Assert.That(newSortedArray[0], Is.EqualTo("Eve"));
            Assert.That(newSortedArray[1], Is.EqualTo("Alice"));
        });
    }
    
    /* Task 5:
     * Get the titles and authors of books with an average review rating above 4.5,
     * ordered by price in descending order.
     */
    [Test]
    public void Fifth()
    {
        // Here is collection of students 
        var books = new Book[]
        {
            new Book { Id = 1, Title = "C# Programming", Author = "John Doe", Price = 45, Reviews = new List<Review> { new Review { Reviewer = "Alice", Rating = 4.5 } }},
            new Book { Id = 2, Title = "ASP.NET MVC", Author = "Jane Smith", Price = 50, Reviews = new List<Review> { new Review { Reviewer = "Bob", Rating = 4.0 } }},
            new Book { Id = 3, Title = "LINQ in Action", Author = "James Brown", Price = 40, Reviews = new List<Review> { new Review { Reviewer = "Charlie", Rating = 4.8 } }},
            new Book { Id = 4, Title = "Entity Framework", Author = "Patricia Johnson", Price = 60, Reviews = new List<Review> { new Review { Reviewer = "David", Rating = 4.7 } }},
            new Book { Id = 5, Title = "Design Patterns", Author = "Robert C. Martin", Price = 70, Reviews = new List<Review> { new Review { Reviewer = "Eve", Rating = 4.9 } }}
        };
        
        // Finish query:
        (string Title, string Author)[] newSortedArray = books
                                                        .Where(b => b.Reviews
                                                        .All(r => r.Rating > 4.5))
                                                        .OrderByDescending(b => b.Price)
                                                        .Select(b => (b.Title, b.Author))
                                                        .ToArray();
        
        // Assert your query
        Assert.Multiple(() =>
        {
            Assert.That(newSortedArray, Has.Length.EqualTo(3));
            
            Assert.That(newSortedArray[0].Title, Is.EqualTo("Design Patterns"));
            Assert.That(newSortedArray[0].Author, Is.EqualTo("Robert C. Martin"));
            
            Assert.That(newSortedArray[1].Title, Is.EqualTo("Entity Framework"));
            Assert.That(newSortedArray[1].Author, Is.EqualTo("Patricia Johnson"));
            
            Assert.That(newSortedArray[2].Title, Is.EqualTo("LINQ in Action"));
            Assert.That(newSortedArray[2].Author, Is.EqualTo("James Brown"));
        });
    }
}