using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zayavki.MinimalApi;

namespace Zayavki.MinimalApi.Applications;

internal class ApplicationEndpoints 
{
    private const string EndpointTag = "Applications";

    public void Register(WebApplication app)
    {
        app.MapGet("api/v1/applications/{applicationId:guid}", GetApplication)
            //.ProducesModel<GlobalCampaignTrigger>()
            //.ProducesUnifiedResponse(new[] { StatusCodes.Status404NotFound })
            //.ProducesServerErrors()
            .WithTags(EndpointTag)
            .WithName($"{nameof(Applications)}_{nameof(GetApplication)}");

        app.MapPost("api/v1/applications/create", CreateApplications)
            //.AcceptsModel<GlobalCampaignTrigger>()
            //.ProducesUnifiedResponse(new[] { StatusCodes.Status201Created })
            //.ProducesServerErrors()
            .WithTags(EndpointTag)
            .WithName($"{nameof(Applications)}_{nameof(CreateApplications)}");
    }

    /// <summary>
    /// Get application by id
    /// </summary>
    [HttpGet(Name = "GetApplication"), Authorize]
    private static async Task<IResult> GetApplication(
        [FromRoute] Guid applicationId,
        [FromServices] IApplicationGetSvc handler,
        CancellationToken cancellationToken)
    {
        var request = new GetApplicationsRequest(ApplicationId: applicationId);

        var result = await handler.Handle(request, cancellationToken);

        return result == null ? Results.NotFound() : Results.Ok(result);
    }

    /// <summary>
    /// Create application
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost(Name = "CreateApplications"), Authorize]
    public static async Task<IResult> CreateApplications(
        [FromBody] CreateApplicationsRequest request,
        [FromServices] IApplicationCreateSvc handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request, cancellationToken);

        return Results.Ok(result);
    }
}
