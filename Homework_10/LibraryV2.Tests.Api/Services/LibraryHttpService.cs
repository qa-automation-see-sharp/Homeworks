using System.Text;
using LibraryV2.Models;
using Newtonsoft.Json;

namespace LibraryV2.Tests.Api.Services;

public class LibraryHttpService
{
    private readonly HttpClient _httpClient;

    private User? DefaultUser { get; set; }
    private AuthorizationToken? AuthorizationToken { get; set; }

    public LibraryHttpService()
    {
        _httpClient = new HttpClient();
    }

    public LibraryHttpService Configure(string baseUrl)
    {
        _httpClient.BaseAddress = new Uri(baseUrl);

        return this;
    }

    public async Task<LibraryHttpService> CreateDefaultUser()
    {
        DefaultUser = new User
        {
            NickName = Guid.NewGuid().ToString(),
            Password = Guid.NewGuid().ToString(),
            FullName = Guid.NewGuid().ToString()
        };

        var url = EndpointsForTest.Users.Register;
        var json = JsonConvert.SerializeObject(DefaultUser);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(url, content);

        return this;
    }

    public async Task<LibraryHttpService> Authorize()
    {
        var url = EndpointsForTest.Users.Login + $"?nickname={DefaultUser.NickName}&password={DefaultUser.Password}";
        var response = await _httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();
        AuthorizationToken = JsonConvert.DeserializeObject<AuthorizationToken>(content);

        return this;
    }


    public async Task<HttpResponseMessage> CreateUser(User user)
    {
        var url = EndpointsForTest.Users.Register;
        var json = JsonConvert.SerializeObject(user);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(url, content);

        return response;
    }

    public async Task<HttpResponseMessage> LogIn(User user)
    {
        var url = EndpointsForTest.Users.Login + $"?nickname={user.NickName}&password={user.Password}";
        var response = await _httpClient.GetAsync(url);

        return response;
    }


    public async Task<HttpResponseMessage> CreateBook(string token, Book book)
    {
        var url = EndpointsForTest.Books.Create + $"?token={token}";
        var json = JsonConvert.SerializeObject(book);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(url, content);

        return response;
    }

    public async Task<HttpResponseMessage> CreateBook(Book book)
    {
        var url = EndpointsForTest.Books.Create + $"?token={AuthorizationToken.Token}";
        var json = JsonConvert.SerializeObject(book);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(url, content);

        return response;
    }

    public async Task<HttpResponseMessage> GetBooksByTitle(string title)
    {
        var url = EndpointsForTest.Books.GetBooksByTitle(title);
        var response = await _httpClient.GetAsync(url);

        return response;
    }

    public async Task<HttpResponseMessage> GetBooksByAuthor(string author)
    {
        var url = EndpointsForTest.Books.GetBooksByAuthor(author);
        var uri = new Uri(_httpClient.BaseAddress, url);
        var response = await _httpClient.GetAsync(uri);

        return response;
    }

    public async Task<HttpResponseMessage> DeleteBook(string token, string title, string author)
    {
        var url = EndpointsForTest.Books.Delete(title, author, token);
        var response = await _httpClient.DeleteAsync(url);

        return response;
    }
}