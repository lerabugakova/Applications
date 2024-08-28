namespace Zayavki;

public record GetApplicationResponse(
    Guid ApplicationId,
    string Abonent, 
    string Address,
    string Incident, 
    string Duty, 
    string Type,
    DateTime CreatedDate,
    DateTime UpdatedDate);

public record LoginResponse(string UserName, string Role, string Token);
