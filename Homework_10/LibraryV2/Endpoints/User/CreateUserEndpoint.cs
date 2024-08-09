using LibraryV2.Repositories;
using LibraryV2.Services;

namespace LibraryV2.Endpoints.User;

public static class CreateUserEndpoint
{
    public const string Name = "CreateUser";

    public static IEndpointRouteBuilder MapCreateUser(this IEndpointRouteBuilder app)
    {
        app.MapPost(ApiEndpoints.Users.Register, (
                Models.User user,
                IUserRepository repository) =>
            {
                if (repository.GetUser(user.NickName!) is not null)
                {
                    return Results.BadRequest($"User with nickname {user.NickName} already exists");
                }

                repository.AddUser(user);

                return Results.Created(ApiEndpoints.Users.Register, user);
            })
            .WithName(Name)
            .Produces<Models.User>()
            .Produces(StatusCodes.Status404NotFound);

        return app;
    }
}