using CRM.Application.Contracts;
using CRM.Application.Financial;
using CRM.Application.Foundation;
using CRM.Application.Portal;
using CRM.Application.Reporting;
using CRM.Application.ReadModels;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();
builder.Services.AddSingleton<CrmReadinessService>();
builder.Services.AddSingleton<CrmDomainCatalogService>();
builder.Services.AddSingleton<LeadFoundationService>();
builder.Services.AddSingleton<AccountFoundationService>();
builder.Services.AddSingleton<ContactFoundationService>();
builder.Services.AddSingleton<LeadReadModelPreviewService>();
builder.Services.AddSingleton<AccountReadModelPreviewService>();
builder.Services.AddSingleton<ContactReadModelPreviewService>();
builder.Services.AddSingleton<CrmReadModelStatusService>();
builder.Services.AddSingleton<CrmPortalIntegrationStatusService>();
builder.Services.AddSingleton<CrmFinancialIntegrationStatusService>();
builder.Services.AddSingleton<CrmReportingIntegrationStatusService>();

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

app.MapGet("/api/crm/foundation/leads/read-model-preview", (LeadReadModelPreviewService service, string? search, int page = 1, int pageSize = 25) => Results.Ok(service.Preview(new CrmReadModelQuery(search, page, pageSize))))
    .WithName("PreviewCrmFoundationLeadReadModel");

app.MapGet("/api/crm/foundation/accounts/read-model-preview", (AccountReadModelPreviewService service, string? search, int page = 1, int pageSize = 25) => Results.Ok(service.Preview(new CrmReadModelQuery(search, page, pageSize))))
    .WithName("PreviewCrmFoundationAccountReadModel");

app.MapGet("/api/crm/foundation/contacts/read-model-preview", (ContactReadModelPreviewService service, string? search, int page = 1, int pageSize = 25) => Results.Ok(service.Preview(new CrmReadModelQuery(search, page, pageSize))))
    .WithName("PreviewCrmFoundationContactReadModel");

app.MapGet("/api/crm/foundation/read-model-status", (CrmReadModelStatusService service) => Results.Ok(service.GetStatus()))
    .WithName("GetCrmFoundationReadModelStatus");

app.MapGet("/api/crm/foundation/portal-integration/status", (CrmPortalIntegrationStatusService service) => Results.Ok(service.GetStatus()))
    .WithName("GetCrmFoundationPortalIntegrationStatus");

app.MapGet("/api/crm/foundation/portal-integration/contracts", (CrmPortalIntegrationStatusService service) => Results.Ok(service.GetContracts()))
    .WithName("GetCrmFoundationPortalIntegrationContracts");

app.MapGet("/api/crm/foundation/portal-integration/required-capabilities", (CrmPortalIntegrationStatusService service) => Results.Ok(service.GetRequiredCapabilities()))
    .WithName("GetCrmFoundationPortalRequiredCapabilities");

app.MapGet("/api/crm/foundation/financial-integration/status", (CrmFinancialIntegrationStatusService service) => Results.Ok(service.GetStatus()))
    .WithName("GetCrmFoundationFinancialIntegrationStatus");

app.MapGet("/api/crm/foundation/financial-integration/contracts", (CrmFinancialIntegrationStatusService service) => Results.Ok(service.GetContracts()))
    .WithName("GetCrmFoundationFinancialIntegrationContracts");

app.MapGet("/api/crm/foundation/financial-integration/required-capabilities", (CrmFinancialIntegrationStatusService service) => Results.Ok(service.GetRequiredCapabilities()))
    .WithName("GetCrmFoundationFinancialRequiredCapabilities");

app.MapGet("/api/crm/foundation/financial-integration/events", (CrmFinancialIntegrationStatusService service) => Results.Ok(service.GetConceptualEvents()))
    .WithName("GetCrmFoundationFinancialIntegrationEvents");

app.MapGet("/api/crm/foundation/reporting/status", (CrmReportingIntegrationStatusService service) => Results.Ok(service.GetStatus()))
    .WithName("GetCrmFoundationReportingStatus");

app.MapGet("/api/crm/foundation/reporting/kpis", (CrmReportingIntegrationStatusService service) => Results.Ok(service.GetKpis()))
    .WithName("GetCrmFoundationReportingKpis");

app.MapGet("/api/crm/foundation/reporting/dashboards", (CrmReportingIntegrationStatusService service) => Results.Ok(service.GetDashboards()))
    .WithName("GetCrmFoundationReportingDashboards");

app.MapGet("/api/crm/foundation/reporting/analytics-read-models", (CrmReportingIntegrationStatusService service) => Results.Ok(service.GetAnalyticsReadModels()))
    .WithName("GetCrmFoundationReportingAnalyticsReadModels");

app.Run();

public partial class Program
{
}
