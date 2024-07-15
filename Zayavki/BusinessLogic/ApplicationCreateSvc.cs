using MongoDB.Driver;

namespace Zayavki;

public class ApplicationCreateSvc : IApplicationCreateSvc
{
    private readonly IApplicationRepository _applications;

    public ApplicationCreateSvc(IApplicationRepository applications)
    {
        _applications = applications;
    }

    public async Task<Guid> Handle(CreateApplicationsRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _applications.CreateAsync(new ApplicationDbEntity
        {
            Id = Guid.NewGuid(),
            ApplicationName = request.ApplicationName,
            ApplicationDescription = request.ApplicationDescription,
            ApplicationType = "New",
            ApplicationPriority = request.ApplicationPriority,
            ApplicationAuthor = request.ApplicationAuthor,
            ApplicationExecutor = "",
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now
        }, cancellationToken);

        return result.Id;
    }
}
