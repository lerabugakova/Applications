namespace Zayavki;

public interface IApplicationGetSvc
{
    public Task<GetApplicationResponse?> Handle(GetApplicationsRequest request, CancellationToken cancellationToken = default);
}
