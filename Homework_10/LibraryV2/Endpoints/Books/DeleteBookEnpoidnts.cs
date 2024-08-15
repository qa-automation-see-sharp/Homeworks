using LibraryV2.Repositories;
using LibraryV2.Services;

namespace LibraryV2.Endpoints.Books;

public static class DeleteBookEnpoidnts
{
    public const string Name = "DeleteBook";

    public static IEndpointRouteBuilder MapDeleteBook(this IEndpointRouteBuilder app)
    {
        app
            .MapDelete(ApiEndpoints.Books.Delete, (
                string title,
                string author,
                string token,
                IBookRepository repository,
                IUserAuthorizationService service) =>
            {
                if (!service.IsAuthorizedByToken(token)) return Results.Unauthorized();
                
                var book = repository.GetBook(b => b.Title == title && b.Author == author);
                
                if (book is null) return Results.NotFound($"Book :{title} by {author} not found");

                repository.Delete(b => b.Title == title && b.Author == author);
                
                return Results.Ok($"{title} by {author} deleted");
            })
            .WithName(Name)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        return app;
    }
}