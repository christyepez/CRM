using CRM.Application.Contracts;
using CRM.Application.Foundation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();
builder.Services.AddSingleton<CrmReadinessService>();
builder.Services.AddSingleton<CrmDomainCatalogService>();
builder.Services.AddSingleton<LeadFoundationService>();
builder.Services.AddSingleton<AccountFoundationService>();
builder.Services.AddSingleton<ContactFoundationService>();

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

app.MapPost("/api/crm/foundation/leads/preview", (LeadFoundationRequest request, LeadFoundationService service) => Results.Ok(service.Preview(request)))
    .WithName("PreviewCrmFoundationLead");

app.MapPost("/api/crm/foundation/accounts/preview", (AccountFoundationRequest request, AccountFoundationService service) => Results.Ok(service.Preview(request)))
    .WithName("PreviewCrmFoundationAccount");

app.MapPost("/api/crm/foundation/contacts/preview", (ContactFoundationRequest request, ContactFoundationService service) => Results.Ok(service.Preview(request)))
    .WithName("PreviewCrmFoundationContact");

app.Run();

public partial class Program
{
}
