using LibraryV2.Endpoints.Books;
using LibraryV2.Endpoints.User;
using LibraryV2.Repositories;
using LibraryV2.Services;

var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddSingleton<IUserRepository, UserRepository>();
    builder.Services.AddSingleton<IBookRepository, BookRepository>();
    builder.Services.AddSingleton<IUserAuthorizationService, UserAuthorizationService>();

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