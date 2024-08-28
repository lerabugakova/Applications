using MongoDB.Driver;

namespace Zayavki;

public class ApplicationGetSvc : IApplicationGetSvc
{
    private readonly IApplicationRepository _applications;

    public ApplicationGetSvc(IApplicationRepository applications)
    {
        _applications = applications;
    }

    public async Task<GetApplicationResponse?> Handle(GetApplicationsRequest request, CancellationToken cancellationToken = default)
    {
        var result =  await _applications.GetAsync(request.ApplicationId, cancellationToken);
        return result == null? null : new GetApplicationResponse
        (
            ApplicationId : result.Id,
            Abonent : result.Abonent,
            Address : result.Address,
            Incident : result.Incident,
            Duty : result.Duty,
            Type : result.Type,
            CreatedDate : result.CreatedDate,
            UpdatedDate : result.UpdatedDate
        );
    }
}
