using Microsoft.AspNetCore.Mvc;

namespace Zayavki.MinimalApi.Applications;

public class AuthorizationEndpoints
{
    private const string Endpoint = "Authorization";

    public void Register(WebApplication app)
    {
        app.MapPost("api/v1/authorization/login", Login)
            .WithTags(Endpoint)
            .WithName($"{nameof(AuthorizationEndpoints)}_{nameof(Login)}");
    }

    private static async Task<IResult> Login(
        [FromBody] LoginRequest request,
        [FromServices] IAuthorizationLoginSvc handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request, cancellationToken);

        return Results.Ok(result);
    }
}

