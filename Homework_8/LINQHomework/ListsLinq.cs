using System.Reflection;
using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;
using LINQHomework.Models;

namespace LINQHomework;

[TestFixture]
public class ListsLinq : StartUpFixture
{
    /* Task 1:
     * Get the names of teachers who teach "Math" and have more than 10 years of experience.
     */
    [Test]
    public void First()
    {
        // Collection of teachers 
        var teachers = new List<Teacher>
        {
            new Teacher { Id = 1, Name = "Mr. Smith", Age = 40, Subject = "Math", Experience = 15 },
            new Teacher { Id = 2, Name = "Mrs. Johnson", Age = 35, Subject = "History", Experience = 10 },
            new Teacher { Id = 3, Name = "Mr. Brown", Age = 50, Subject = "Science", Experience = 25 },
            new Teacher { Id = 4, Name = "Ms. Davis", Age = 29, Subject = "English", Experience = 7 },
            new Teacher { Id = 5, Name = "Mr. Wilson", Age = 45, Subject = "Math", Experience = 20 }
        };
            
        // Query
        List<string> result = new List<string>();
            
        // Assert your query
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result[0], Is.EqualTo("Mr. Wilson"));
            Assert.That(result[1], Is.EqualTo("Mr. Smith"));
        });
    }
        
    /* Task 2:
     * Get the titles of courses that have more than 3 credits and at least one student.
     */
    [Test]
    public void Second()
    {
        // Collection of courses
        var courses = new List<Course>
        {
            new Course { Id = 1, Title = "Algebra", Instructor = "Mr. Smith", Credits = 3, Students = new List<string> { "Alice", "Bob" } },
            new Course { Id = 2, Title = "World History", Instructor = "Mrs. Johnson", Credits = 4, Students = new List<string> { "Charlie" } },
            new Course { Id = 3, Title = "Physics", Instructor = "Mr. Brown", Credits = 4, Students = new List<string> { "David", "Eve" } },
            new Course { Id = 4, Title = "Literature", Instructor = "Ms. Davis", Credits = 2, Students = new List<string> { "Frank" } },
            new Course { Id = 5, Title = "Calculus", Instructor = "Mr. Wilson", Credits = 3, Students = new List<string> { "Alice", "George" } }
        };
            
        // Query
        List<string> result = new List<string>();
            
        // Assert your query
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result[0], Is.EqualTo("Physics"));
            Assert.That(result[1], Is.EqualTo("World History"));
        });
    }

    /* Task 3:
     * Finish query that way to check if all books have more than 200 pages.
     */
    [Test]
    public void Third()
    {
        // Collection of books
        var books = new List<Book>
        {
            new Book { Id = 1, Title = "The Catcher in the Rye", Author = "J.D. Salinger", Pages = 214 },
            new Book { Id = 2, Title = "To Kill a Mockingbird", Author = "Harper Lee", Pages = 281 },
            new Book { Id = 3, Title = "1984", Author = "George Orwell", Pages = 328 },
            new Book { Id = 4, Title = "Moby-Dick", Author = "Herman Melville", Pages = 585 },
            new Book { Id = 5, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Pages = 180 }
        };
            
        // Query
        bool result = true;
            
        // Assert your query
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.False);
        });
    }
        
    /* Task 4:
     * Get the title of the first movie that has a duration longer than 150 minutes.
     */
    [Test]
    public void Fourth()
    {
        // Collection of movies
        var movies = new List<Movie>
        {
            new Movie { Id = 1, Title = "The Shawshank Redemption", Genre = "Drama", Duration = 142 },
            new Movie { Id = 2, Title = "The Godfather", Genre = "Crime", Duration = 175 },
            new Movie { Id = 3, Title = "The Dark Knight", Genre = "Action", Duration = 152 },
            new Movie { Id = 4, Title = "Pulp Fiction", Genre = "Crime", Duration = 154 },
            new Movie { Id = 5, Title = "Forrest Gump", Genre = "Drama", Duration = 142 }
        };
            
        // Query
        string result = "?";
            
        // Assert your query
        Assert.That(result, Is.EqualTo("The Godfather"));
    }
        
    /* Task 5:
     * Check if there are any employees in the "Finance" department with a salary less than 60000.
     */
    [Test]
    public void Fifth()
    {
        // Collection of employees
        var employees = new List<Employee>
        {
            new Employee { Id = 1, Name = "John", Department = "HR", Salary = 50000, Skills = new List<string> { "Communication", "Recruitment" }},
            new Employee { Id = 2, Name = "Jane", Department = "IT", Salary = 60000, Skills = new List<string> { "Programming", "Networking" }},
            new Employee { Id = 3, Name = "Doe", Department = "Finance", Salary = 55000, Skills = new List<string> { "Accounting", "Excel" }},
            new Employee { Id = 4, Name = "Smith", Department = "IT", Salary = 70000, Skills = new List<string> { "Programming", "Security" }},
            new Employee { Id = 5, Name = "Emily", Department = "HR", Salary = 48000, Skills = new List<string> { "Communication", "Training" }}
        };
            
        // Query
        var result = false;
            
        // Assert your query
        Assert.That(result, Is.True);
    }
}