namespace Zayavki;

public interface IAuthorizationLoginSvc
{
    Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken);
}
