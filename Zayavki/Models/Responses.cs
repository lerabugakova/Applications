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
