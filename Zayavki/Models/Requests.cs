﻿namespace Zayavki;

public record GetApplicationsRequest(Guid ApplicationId);
public record CreateApplicationsRequest(
    string ApplicationName, 
    string ApplicationDescription,
    string ApplicationType, 
    string ApplicationPriority, 
    string ApplicationAuthor);
