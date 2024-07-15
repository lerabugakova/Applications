namespace Zayavki;

public interface IAuthorizationLoginsvc
{
    Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken);
}
