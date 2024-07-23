using StudentManagementSystem.Actions;
using StudentManagementSystem.Models;

namespace StudentManagementSystem;

public static class Program
{
    
    public static void Main()
    {
        var schoolClasses = new List<SchoolClass>();
        var users = new List<User>();
        
        var manageSchool = new LogInMenu(users, schoolClasses);
        manageSchool.ShowMenu();
    }
}