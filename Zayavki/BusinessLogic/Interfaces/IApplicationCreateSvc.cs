namespace Zayavki;

public interface IApplicationCreateSvc
{
    public Task<Guid> Handle(CreateApplicationsRequest request, CancellationToken cancellationToken = default);
}
