using LibraryV2.Models;

namespace LibraryV2.Repositories;

public interface IUserRepository
{
    public User? GetUser(string nickName);
    public bool AddUser(User user);
}