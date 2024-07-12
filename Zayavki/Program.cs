using Microsoft.Extensions.DependencyInjection;
using Zayavki;
using Zayavki.MinimalApi.Applications;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ApplicationDatabaseSettings>(
    builder.Configuration.GetSection("Applications"));

builder.Services.AddTransient<IApplicationService, ApplicationService>();
builder.Services.AddTransient<IApplicationCreateSvc, ApplicationCreateSvc>();
builder.Services.AddTransient<IApplicationGetSvc, ApplicationGetSvc>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


new ApplicationEndpoints().Register(app);

app.UseHttpsRedirection();
app.Run();