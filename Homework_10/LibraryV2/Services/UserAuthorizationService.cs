using LibraryV2.Models;
using LibraryV2.Repositories;

namespace LibraryV2.Services;

public interface IUserAuthorizationService
{
    public bool IsAuthorizedByToken(string authorizationToken);
    public bool IsAuthorizedByNickName(string nickName);

    public AuthorizationToken? GenerateToken(string nickName, string password);

    public AuthorizationToken? GetToken(string nickName);
}

public class UserAuthorizationService : IUserAuthorizationService
{
    private readonly IUserRepository _userRepository;
    private readonly List<AuthorizationToken> _tokens = new();

    public UserAuthorizationService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public bool IsAuthorizedByToken(string authorizationToken)
    {
        var token = _tokens.FirstOrDefault(t => t.Token == authorizationToken);

        if (token is not null)
        {
            if (token.ExpirationTime > DateTime.Now)
            {
                return true;
            }
        }

        return false;
    }

    public bool IsAuthorizedByNickName(string nickName)
    {
        var token = _tokens.FirstOrDefault(t => t.NickName == nickName);

        if (token is not null)
        {
            if (token.ExpirationTime > DateTime.Now)
            {
                return true;
            }
        }

        return false;
    }

    public AuthorizationToken? GenerateToken(string nickName, string password)
    {
        var user = _userRepository.GetUser(nickName);

        if (user == null || user.Password != password)
        {
            return null;
        }

        var tmp = _tokens.FirstOrDefault(t => t.NickName == nickName);
        if (tmp is not null)
        {
            _tokens.Remove(tmp);
        }

        var token = new AuthorizationToken
        {
            Token = Guid.NewGuid().ToString(),
            NickName = nickName,
            ExpirationTime = DateTime.Now.AddMinutes(15)
        };

        _tokens.Add(token);

        return token;
    }

    public AuthorizationToken? GetToken(string nickName)
    {
        return _tokens.FirstOrDefault(t => t.NickName == nickName);
    }
}