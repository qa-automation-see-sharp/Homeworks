namespace StudentManagementSystem.Models;

public class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public bool IsTeacher { get; set; }
    
    public User(string username, string password, bool isTeacher)
    {
        Username = username;
        Password = password;
        IsTeacher = isTeacher;
    }
}