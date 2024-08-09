using LibraryV2.Models;
using LibraryV2.Repositories;
using LibraryV2.Services;

namespace LibraryV2.Endpoints.Books;

public static class CreateBookEndpoint
{
    public const string Name = "CreateBook";
    
    public static IEndpointRouteBuilder MapCreateBook(this IEndpointRouteBuilder app)
    {
        app
            .MapPost(ApiEndpoints.Books.Create, (
                string token,
                Book book,
                IBookRepository repository,
                IUserAuthorizationService service) =>
            {
                if (!service.IsAuthorizedByToken(token)) return Results.Unauthorized();
                
                if (repository.Exists(book)) return Results.BadRequest($"{book.Title} by {book.Author}, {book.YearOfRelease} already exists");
                repository.AddBook(book);

                return TypedResults.CreatedAtRoute(book, Name, new { title = book.Title });
            })
            .WithName(Name)
            .Produces<Book>()
            .Produces(StatusCodes.Status400BadRequest);

        return app;
    }
}