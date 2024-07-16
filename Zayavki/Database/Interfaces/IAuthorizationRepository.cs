namespace Zayavki;

public interface IAuthorizationRepository
{
    public Task<AuthorizationDbEntity> GetUser(string userName, string encryptedPassword, CancellationToken cancellationToken = default);
}
