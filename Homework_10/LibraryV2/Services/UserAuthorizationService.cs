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

    private readonly ILogger<UserAuthorizationService> _logger;

    public UserAuthorizationService(IUserRepository userRepository, ILogger<UserAuthorizationService> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
        //_tokens.FirstOrDefault().ExpirationTime = DateTime.Now.AddMinutes(15);
    }

    public bool IsAuthorizedByToken(string authorizationToken)
    {
        _logger.LogInformation("Authorizing token: {Token}", authorizationToken);

        var token = _tokens.FirstOrDefault(t => t.Token == authorizationToken);

        if (token is not null)
        {
            if (token.ExpirationTime > DateTime.Now)
            {
                _logger.LogInformation("Token is valid: {Token}", authorizationToken);
                return true;
            }
            else
            {
                _logger.LogWarning("Token expired: {Token}", authorizationToken);
            }
        }
        else
        {
            _logger.LogWarning("Token not found: {Token}", authorizationToken);
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
        _logger.LogInformation("Generating token for user: {NickName}", nickName);

        var user = _userRepository.GetUser(nickName);

        if (user == null || user.Password != password)
        {
            _logger.LogWarning("Invalid credentials for user: {NickName}", nickName);
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

        _logger.LogInformation("Token generated for user: {NickName}, Token: {Token}", nickName, token.Token);
        _logger.LogInformation("Current tokens: {Tokens}", string.Join(", ", _tokens.Select(t => t.Token)));

        return token;
    }

    public AuthorizationToken? GetToken(string nickName)
    {
        return _tokens.FirstOrDefault(t => t.NickName == nickName);
        _logger.LogInformation("Token for user: {NickName} is: {Token}", nickName, _tokens.FirstOrDefault(t => t.NickName == nickName));
    }
}