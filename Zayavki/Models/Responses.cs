using System.IdentityModel.Tokens.Jwt;

namespace Zayavki;

public record GetApplicationResponse(
    Guid ApplicationId,
    string ApplicationName, 
    string ApplicationDescription,
    string ApplicationType, 
    string ApplicationPriority, 
    string ApplicationAuthor,
    string ApplicationExecutor,
    DateTime CreatedDate,
    DateTime UpdatedDate);

public record LoginResponse(string UserName, string Role, string Token);
