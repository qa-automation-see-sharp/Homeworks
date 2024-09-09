using LibraryV2.Endpoints.Books;
using LibraryV2.Endpoints.User;
using LibraryV2.Repositories;
using LibraryV2.Services;
using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddSingleton<IUserRepository, UserRepository>();
    builder.Services.AddSingleton<LibraryV2.Repositories.IBookRepository, BookRepository>();
builder.Services.AddSingleton<IUserAuthorizationService, UserAuthorizationService>();
builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});

var app = builder.Build();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
    app.MapCreateUser();
    app.MapLogIn();
    app.MapCreateBook();
    app.MapGetBooksByTitle();
    app.MapGetBooksByAuthor(); 
    app.MapGetAllBooks();
    app.MapDeleteBook();  
    app.Run();

