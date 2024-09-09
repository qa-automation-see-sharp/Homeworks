using LibraryV2.Repositories;
using LibraryV2.Services;
using Microsoft.AspNetCore.Identity;

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

            if (string.IsNullOrWhiteSpace(user.NickName))
            {
                return Results.BadRequest($"Impossible to create user with blank name or spaced name");
            }
          

            repository.AddUser(user);

            return Results.Created(ApiEndpoints.Users.Register, new { nickName = user.NickName , fullName = user.FullName, password = user.Password});
            })
            .WithName(Name)
            .Produces<Models.User>()
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest);

        return app;
    }
}