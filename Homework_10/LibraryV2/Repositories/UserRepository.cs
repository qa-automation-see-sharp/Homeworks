using LibraryV2.Models;

namespace LibraryV2.Repositories;

public class UserRepository : IUserRepository
{
    private readonly List<User> _users = new();

    public User? GetUser(string nickName)
    {
        return _users.FirstOrDefault(u => u.NickName == nickName);
    }

    public bool AddUser(User user)
    {
        if (_users.Exists(u => u.NickName == user.NickName))
        {
            return false;
        }

        _users.Add(user);
        return true;
    }
}