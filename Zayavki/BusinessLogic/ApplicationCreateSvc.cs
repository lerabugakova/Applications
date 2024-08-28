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
            Abonent = request.Abonent,
            Address = request.Address,
            Type = "New",
            Incident = request.Incident,
            Duty = request.Duty,
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now
        }, cancellationToken);

        return result.Id;
    }
}
