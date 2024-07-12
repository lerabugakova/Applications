using MongoDB.Driver;

namespace Zayavki;

public class ApplicationGetSvc : IApplicationGetSvc
{
    private readonly IApplicationService _applications;

    public ApplicationGetSvc(IApplicationService applications)
    {
        _applications = applications;
    }

    public async Task<GetApplicationResponse?> Handle(GetApplicationsRequest request, CancellationToken cancellationToken = default)
    {
        var result =  await _applications.GetAsync(request.ApplicationId, cancellationToken);
        return result == null? null : new GetApplicationResponse
        (
            ApplicationId : result.Id,
            ApplicationName : result.ApplicationName,
            ApplicationDescription : result.ApplicationDescription,
            ApplicationType : result.ApplicationType,
            ApplicationPriority : result.ApplicationPriority,
            ApplicationAuthor : result.ApplicationAuthor,
            ApplicationExecutor : result.ApplicationExecutor,
            CreatedDate : result.CreatedDate,
            UpdatedDate : result.UpdatedDate
        );
    }
}
