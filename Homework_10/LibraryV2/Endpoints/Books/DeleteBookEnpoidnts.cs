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
                Console.WriteLine($"Received request to delete book: Title = {title}, Author = {author}, Token = {token}");
                if (!service.IsAuthorizedByToken(token))
                {
                    Console.WriteLine("Unauthorized request.");
                    return Results.Unauthorized();
                }

                var book = repository.GetBook(b => b.Title == title && b.Author == author);

                if (book is null)
                {
                    Console.WriteLine($"Book not found: Title = {title}, Author = {author}");
                    return Results.NotFound($"Book :{title} by {author} not found");
                }

                repository.Delete(b => b.Title == title && b.Author == author);
                Console.WriteLine($"Book deleted: Title = {title}, Author = {author}");

                return Results.Ok($"{title} by {author} deleted");
            })
            .WithName(Name)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        return app;
    }
}