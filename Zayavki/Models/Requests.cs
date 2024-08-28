namespace Zayavki;

public record GetApplicationsRequest(Guid ApplicationId);
public record CreateApplicationsRequest(
    string Abonent, 
    string Address,
    string Incident, 
    string Duty);

public record LoginRequest(string UserName, string Password);
