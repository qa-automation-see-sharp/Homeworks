namespace LibraryV2.Models;

public sealed class AuthorizationToken
{
    public string? Token { get; set; }
    public string? NickName { get; set; }
    public DateTime? ExpirationTime { get; set; }
}