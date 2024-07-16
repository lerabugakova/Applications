using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Zayavki;
using Zayavki.MinimalApi.Applications;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.Configure<ApplicationDatabaseSettings>(
    builder.Configuration.GetSection("Applications"));

builder.Services.Configure<AuthorizationDatabaseSettings>(
    builder.Configuration.GetSection("Auth"));

builder.Services.AddTransient<IApplicationRepository, ApplicationRepository>();
builder.Services.AddTransient<IApplicationCreateSvc, ApplicationCreateSvc>();
builder.Services.AddTransient<IApplicationGetSvc, ApplicationGetSvc>();

builder.Services.AddTransient<IAuthorizationRepository, AuthorizationRepository>();
builder.Services.AddTransient<IAuthorizationLoginsvc, AuthorizationLoginSvc>();

 builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


new ApplicationEndpoints().Register(app);
new AuthorizationEndpoints().Register(app);

app.UseHttpsRedirection();
app.Run();