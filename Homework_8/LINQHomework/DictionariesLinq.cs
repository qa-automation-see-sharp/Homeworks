using System.Reflection;
using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;
using LINQHomework.Models;

namespace LINQHomework;

[TestFixture]
public class DictionariesLinq : StartUpFixture
{
    /* Task 1:
     * Get the names of teachers who teach "Math" and have more than 10 years of experience.
     */
    [Test]
    public void First()
    {
        // Collection of teachers 
        var teachers = new Dictionary<int, Teacher>
        {
            { 1, new Teacher { Id = 1, Name = "Mr. Smith", Age = 40, Subject = "Math", Experience = 15 }},
            { 2, new Teacher { Id = 2, Name = "Mrs. Johnson", Age = 35, Subject = "History", Experience = 10 }},
            { 3, new Teacher { Id = 3, Name = "Mr. Brown", Age = 50, Subject = "Science", Experience = 25 }},
            { 4, new Teacher { Id = 4, Name = "Ms. Davis", Age = 29, Subject = "English", Experience = 7 }},
            { 5, new Teacher { Id = 5, Name = "Mr. Wilson", Age = 45, Subject = "Math", Experience = 20 }}
        };
            
        // Query
        var result = teachers.Values
			.Where(y=>y.Subject.Contains("Math") && y.Experience > 10)
			.OrderByDescending(x=>x.Experience)
            .Select(x=>x.Name)
            .ToList();
      
            
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
        var courses = new Dictionary<int, Course>
        {
            { 1, new Course { Id = 1, Title = "Algebra", Instructor = "Mr. Smith", Credits = 3, Students = new List<string> { "Alice", "Bob" }}},
            { 2, new Course { Id = 2, Title = "World History", Instructor = "Mrs. Johnson", Credits = 4, Students = new List<string> { "Charlie" }}},
            { 3, new Course { Id = 3, Title = "Physics", Instructor = "Mr. Brown", Credits = 4, Students = new List<string> { "David", "Eve" }}},
            { 4, new Course { Id = 4, Title = "Literature", Instructor = "Ms. Davis", Credits = 2, Students = new List<string> { "Frank" }}},
            { 5, new Course { Id = 5, Title = "Calculus", Instructor = "Mr. Wilson", Credits = 3, Students = new List<string> { "Alice", "George" }}}
        };

        // Query
        var result = courses.Values
                    .Where(x => x.Credits > 3 && x.Students.Count > 0)
                    .OrderBy(x => x.Title)
                    .ToList();

        // Assert your query
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result[0].Title, Is.EqualTo("Physics"));
            Assert.That(result[1].Title, Is.EqualTo("World History"));
        });
    }

    /* Task 3:
     * Get the titles of books where all books have more than 200 pages,
     * result should be ordered by pages in descending order.
     */
    [Test]
    public void Third()
    {
        // Collection of books
        var books = new Dictionary<int, Book>
        {
            { 1, new Book { Id = 1, Title = "The Catcher in the Rye", Author = "J.D. Salinger", Pages = 214 }},
            { 2, new Book { Id = 2, Title = "To Kill a Mockingbird", Author = "Harper Lee", Pages = 281 }},
            { 3, new Book { Id = 3, Title = "1984", Author = "George Orwell", Pages = 328 }},
            { 4, new Book { Id = 4, Title = "Moby-Dick", Author = "Herman Melville", Pages = 585 }},
            { 5, new Book { Id = 5, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Pages = 180 }}
        };
            
        // Query
         var result = books.Values
                    .Where(x=>x.Pages>200)
                    .OrderByDescending(x=>x.Pages)
                    .Select(x=>x.Title)
                    .ToList();
            
        // Assert your query
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(4));
            Assert.That(result[0], Is.EqualTo("Moby-Dick"));
            Assert.That(result[1], Is.EqualTo("1984"));
            Assert.That(result[2], Is.EqualTo("To Kill a Mockingbird"));
            Assert.That(result[3], Is.EqualTo("The Catcher in the Rye"));
        });
    }
        
    /* Task 4:
     * Get the title of the first movie that has a duration longer than 150 minutes.
     */
    [Test]
    public void Fourth()
    {
        // Collection of movies
        var movies = new Dictionary<int, Movie>
        {
            { 1, new Movie { Id = 1, Title = "The Shawshank Redemption", Genre = "Drama", Duration = 142 }},
            { 2, new Movie { Id = 2, Title = "The Godfather", Genre = "Crime", Duration = 175 }},
            { 3, new Movie { Id = 3, Title = "The Dark Knight", Genre = "Action", Duration = 152 }},
            { 4, new Movie { Id = 4, Title = "Pulp Fiction", Genre = "Crime", Duration = 154 }},
            { 5, new Movie { Id = 5, Title = "Forrest Gump", Genre = "Drama", Duration = 142 }}
        };
            
        // Query
        var result = movies.FirstOrDefault(x=>x.Value.Duration>150).Value.Title;
            
        // Assert your query
        Assert.That(result, Is.EqualTo("The Godfather"));
    }
        
    /* Task 5:
     * Check if there are any employees in the "IT" department with a salary greater than 60000.
     */
    [Test]
    public void Fifth()
    {
        // Collection of employees
        var employees = new Dictionary<int, Employee>
        {
            { 1, new Employee { Id = 1, Name = "John", Department = "HR", Salary = 50000, Skills = new List<string> { "Communication", "Recruitment" }}},
            { 2, new Employee { Id = 2, Name = "Jane", Department = "IT", Salary = 60000, Skills = new List<string> { "Programming", "Networking" }}},
            { 3, new Employee { Id = 3, Name = "Doe", Department = "Finance", Salary = 55000, Skills = new List<string> { "Accounting", "Excel" }}},
            { 4, new Employee { Id = 4, Name = "Smith", Department = "IT", Salary = 70000, Skills = new List<string> { "Programming", "Security" }}},
            { 5, new Employee { Id = 5, Name = "Emily", Department = "HR", Salary = 48000, Skills = new List<string> { "Communication", "Training" }}}
        };
            
        // Query
        var result = employees.Any(x=>x.Value.Department=="IT" && x.Value.Salary>60000);
            
        // Assert your query
        Assert.That(result, Is.True);
    }
}