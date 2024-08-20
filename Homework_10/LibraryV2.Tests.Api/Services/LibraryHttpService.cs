using System.Text;
using LibraryV2.Models;
using LibraryV2.Tests.Api.TestHelpers;
using Newtonsoft.Json;

namespace LibraryV2.Tests.Api.Services;

public class LibraryHttpService
{
    private readonly HttpClient _httpClient;
    private AuthorizationToken Token { get; set; }
    private User? DefaultUser { get; set; }

    public LibraryHttpService()
    {
        _httpClient = new HttpClient();
       
    }

    public LibraryHttpService Configure(string baseUrl)
    {
        _httpClient.BaseAddress = new Uri(baseUrl);
        return this;
    }

    public async Task<User>CreateDefaultUser(){
        DefaultUser = DataHelper.UserHelper.CreateRandomUser();

        var url = EndpointsForTest.Users.Register;
        var json = JsonConvert.SerializeObject(DefaultUser);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(url, content);

        Console.WriteLine("Create Default User:");
        Console.WriteLine($"POST request to: \n{url}");
        Console.WriteLine($"Content: \n{json}");
        Console.WriteLine($"Response status codeis : \n{response.StatusCode}");

        return DefaultUser;
    }

    public async Task<LibraryHttpService> Authorize()
    {
        var url = EndpointsForTest.Users.Login(DefaultUser.NickName, DefaultUser.Password);
        var response = await _httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();
        Token = JsonConvert.DeserializeObject<AuthorizationToken>(content);

        ConsoleHelper.GetInfo("Authorize", url, content, response);

        return this;
    }


    public async Task<HttpResponseMessage> CreateUser(User user)
    {
        var url = EndpointsForTest.Users.Register;
        var json = JsonConvert.SerializeObject(user);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(url, content);
        var jsonString = await response.Content.ReadAsStringAsync();

        ConsoleHelper.PostInfo("Create user", url, jsonString, response);

        return response;
    }
    
    public async Task<HttpResponseMessage> LogIn(User user)
    {
        var url = EndpointsForTest.Users.Login(user.NickName, user.Password);
        var response = await _httpClient.GetAsync(url);
        var jsonString = await response.Content.ReadAsStringAsync();

        ConsoleHelper.GetInfo("Login", url, jsonString, response);

        return response;
    }

     public async Task<HttpResponseMessage> LogIn(string nickName, string password)
    {
        var url = EndpointsForTest.Users.Login(nickName, password);
        var response = await _httpClient.GetAsync(url);
        var jsonString = await response.Content.ReadAsStringAsync();

        ConsoleHelper.GetInfo("Login", url, jsonString, response);
        
        return response;
    }
    

    public async Task<HttpResponseMessage> PostBook(Book book)
    {
        var url = EndpointsForTest.Books.Create(Token.Token);
        var json = JsonConvert.SerializeObject(book);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(url, content);
        var jsonString = await response.Content.ReadAsStringAsync();

        ConsoleHelper.PostInfo("Create book", url, jsonString, response);
        // Console.WriteLine("Create Book:");
        // Console.WriteLine($"POST request to: {url}");
        // Console.WriteLine($"Content: {jsonString}");
        // Console.WriteLine($"Response status code is : {response.StatusCode}");

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
        var response = await _httpClient.GetAsync(url);

        return response;
    }

    public async Task<HttpResponseMessage> DeleteBook(string title, string author)
    {
        var url = EndpointsForTest.Books.Delete(title, author, Token.Token);
        var response = await _httpClient.DeleteAsync(url);

        return response;
    }

    public async Task<HttpResponseMessage> DeleteBook(string title, string author, string token)
    {
        var url = EndpointsForTest.Books.Delete(title, author, token);
        var response = await _httpClient.DeleteAsync(url);

        return response;
    }
}