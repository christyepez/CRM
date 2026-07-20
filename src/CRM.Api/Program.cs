using CRM.Application.Foundation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();
builder.Services.AddSingleton<CrmReadinessService>();

var app = builder.Build();

app.MapHealthChecks("/health");
app.MapHealthChecks("/health/live");
app.MapHealthChecks("/health/ready");

app.MapGet("/api/crm/readiness", (CrmReadinessService readiness) => Results.Ok(readiness.GetReadiness()))
    .WithName("GetCrmReadiness");

app.Run();

public partial class Program
{
}
