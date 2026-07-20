using CRM.Application.Contracts;
using CRM.Application.Foundation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();
builder.Services.AddSingleton<CrmReadinessService>();
builder.Services.AddSingleton<CrmDomainCatalogService>();

var app = builder.Build();

app.MapHealthChecks("/health");
app.MapHealthChecks("/health/live");
app.MapHealthChecks("/health/ready");

app.MapGet("/api/crm/readiness", (CrmReadinessService readiness) => Results.Ok(readiness.GetReadiness()))
    .WithName("GetCrmReadiness");

app.MapGet("/api/crm/domain-catalog", (CrmDomainCatalogService catalog) => Results.Ok(catalog.GetCatalog()))
    .WithName("GetCrmDomainCatalog");

app.MapGet("/api/crm/contracts", (CrmDomainCatalogService catalog) => Results.Ok(catalog.GetContracts()))
    .WithName("GetCrmContracts");

app.MapGet("/api/crm/integration-boundaries", (CrmDomainCatalogService catalog) => Results.Ok(catalog.GetIntegrationBoundaries()))
    .WithName("GetCrmIntegrationBoundaries");

app.Run();

public partial class Program
{
}
