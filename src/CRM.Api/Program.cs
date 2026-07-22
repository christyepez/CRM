using CRM.Application.Contracts;
using CRM.Application.Financial;
using CRM.Application.Foundation;
using CRM.Application.Persistence;
using CRM.Application.Ports.Persistence;
using CRM.Application.Ports.Portal;
using CRM.Application.Portal;
using CRM.Application.Reporting;
using CRM.Application.ReadModels;
using CRM.Infrastructure.Persistence.Foundation;
using CRM.Infrastructure.Portal.Simulation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();
builder.Services.AddSingleton<CrmReadinessService>();
builder.Services.AddSingleton<CrmDomainCatalogService>();
builder.Services.AddSingleton<LeadFoundationService>();
builder.Services.AddSingleton<AccountFoundationService>();
builder.Services.AddSingleton<ContactFoundationService>();
builder.Services.AddSingleton<FoundationLeadCrudService>();
builder.Services.AddSingleton<FoundationAccountCrudService>();
builder.Services.AddSingleton<FoundationContactCrudService>();
builder.Services.AddSingleton<FoundationCrudStatusService>();
builder.Services.AddSingleton<CrmSprint2IntegrationReadinessService>();
builder.Services.AddSingleton<LeadReadModelPreviewService>();
builder.Services.AddSingleton<AccountReadModelPreviewService>();
builder.Services.AddSingleton<ContactReadModelPreviewService>();
builder.Services.AddSingleton<CrmReadModelStatusService>();
builder.Services.AddSingleton<CrmPortalIntegrationStatusService>();
builder.Services.AddSingleton<CrmFinancialIntegrationStatusService>();
builder.Services.AddSingleton<CrmReportingIntegrationStatusService>();
builder.Services.AddSingleton<CrmSprint1ClosureStatusService>();
builder.Services.AddSingleton<CrmPersistenceReadinessService>();
builder.Services.AddSingleton<ILeadFoundationStore, InMemoryLeadFoundationStore>();
builder.Services.AddSingleton<IAccountFoundationStore, InMemoryAccountFoundationStore>();
builder.Services.AddSingleton<IContactFoundationStore, InMemoryContactFoundationStore>();
builder.Services.AddSingleton<ICrmFoundationUnitOfWork, InMemoryCrmFoundationUnitOfWork>();
builder.Services.AddSingleton<ICrmPersistenceFeatureFlagProvider, StaticCrmPersistenceFeatureFlagProvider>();
builder.Services.AddSingleton<CrmPersistenceSeamStatusService>();
builder.Services.AddSingleton<IPortalUserContextProvider, SimulatedPortalUserContextProvider>();
builder.Services.AddSingleton<IPortalPermissionProvider, SimulatedPortalPermissionProvider>();
builder.Services.AddSingleton<IPortalAuthorizationScenarioProvider, SimulatedPortalAuthorizationScenarioProvider>();
builder.Services.AddSingleton<CrmFoundationPermissionGuard>();
builder.Services.AddSingleton<CrmPortalAuthorizationSimulationService>();

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

app.MapGet("/api/crm/foundation/crud/status", (FoundationCrudStatusService service) => Results.Ok(service.GetStatus()))
    .WithName("GetCrmFoundationCrudStatus");

app.MapGet("/api/crm/foundation/sprint-2/integration-readiness", (CrmSprint2IntegrationReadinessService service) => Results.Ok(service.GetStatus()))
    .WithName("GetCrmFoundationSprint2IntegrationReadiness");

app.MapGet("/api/crm/foundation/leads", async (FoundationLeadCrudService service, CancellationToken cancellationToken) => Results.Ok(await service.GetAllAsync(cancellationToken)))
    .WithName("GetCrmFoundationLeads");

app.MapGet("/api/crm/foundation/leads/{id}", async (string id, FoundationLeadCrudService service, CancellationToken cancellationToken) => Results.Ok(await service.GetByIdAsync(id, cancellationToken)))
    .WithName("GetCrmFoundationLeadById");

app.MapPost("/api/crm/foundation/leads", async (FoundationLeadCreateRequest request, FoundationLeadCrudService service, CancellationToken cancellationToken) => Results.Ok(await service.CreateAsync(request, cancellationToken)))
    .WithName("CreateCrmFoundationLead");

app.MapPut("/api/crm/foundation/leads/{id}", async (string id, FoundationLeadUpdateRequest request, FoundationLeadCrudService service, CancellationToken cancellationToken) => Results.Ok(await service.UpdateAsync(id, request, cancellationToken)))
    .WithName("UpdateCrmFoundationLead");

app.MapGet("/api/crm/foundation/accounts", async (FoundationAccountCrudService service, CancellationToken cancellationToken) => Results.Ok(await service.GetAllAsync(cancellationToken)))
    .WithName("GetCrmFoundationAccounts");

app.MapGet("/api/crm/foundation/accounts/{id}", async (string id, FoundationAccountCrudService service, CancellationToken cancellationToken) => Results.Ok(await service.GetByIdAsync(id, cancellationToken)))
    .WithName("GetCrmFoundationAccountById");

app.MapPost("/api/crm/foundation/accounts", async (FoundationAccountCreateRequest request, FoundationAccountCrudService service, CancellationToken cancellationToken) => Results.Ok(await service.CreateAsync(request, cancellationToken)))
    .WithName("CreateCrmFoundationAccount");

app.MapPut("/api/crm/foundation/accounts/{id}", async (string id, FoundationAccountUpdateRequest request, FoundationAccountCrudService service, CancellationToken cancellationToken) => Results.Ok(await service.UpdateAsync(id, request, cancellationToken)))
    .WithName("UpdateCrmFoundationAccount");

app.MapGet("/api/crm/foundation/contacts", async (FoundationContactCrudService service, CancellationToken cancellationToken) => Results.Ok(await service.GetAllAsync(cancellationToken)))
    .WithName("GetCrmFoundationContacts");

app.MapGet("/api/crm/foundation/contacts/{id}", async (string id, FoundationContactCrudService service, CancellationToken cancellationToken) => Results.Ok(await service.GetByIdAsync(id, cancellationToken)))
    .WithName("GetCrmFoundationContactById");

app.MapPost("/api/crm/foundation/contacts", async (FoundationContactCreateRequest request, FoundationContactCrudService service, CancellationToken cancellationToken) => Results.Ok(await service.CreateAsync(request, cancellationToken)))
    .WithName("CreateCrmFoundationContact");

app.MapPut("/api/crm/foundation/contacts/{id}", async (string id, FoundationContactUpdateRequest request, FoundationContactCrudService service, CancellationToken cancellationToken) => Results.Ok(await service.UpdateAsync(id, request, cancellationToken)))
    .WithName("UpdateCrmFoundationContact");

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

app.MapGet("/api/crm/foundation/sprint-1/closure-status", (CrmSprint1ClosureStatusService service) => Results.Ok(service.GetStatus()))
    .WithName("GetCrmFoundationSprint1ClosureStatus");

app.MapGet("/api/crm/foundation/persistence/readiness", (CrmPersistenceReadinessService service) => Results.Ok(service.GetReadiness()))
    .WithName("GetCrmFoundationPersistenceReadiness");

app.MapGet("/api/crm/foundation/persistence/seam-status", async (CrmPersistenceSeamStatusService service, CancellationToken cancellationToken) => Results.Ok(await service.GetStatusAsync(cancellationToken)))
    .WithName("GetCrmFoundationPersistenceSeamStatus");

app.MapGet("/api/crm/foundation/persistence/feature-flags", (CrmPersistenceSeamStatusService service) => Results.Ok(service.GetFeatureFlags()))
    .WithName("GetCrmFoundationPersistenceFeatureFlags");

app.MapGet("/api/crm/foundation/persistence/stores/status", async (CrmPersistenceSeamStatusService service, CancellationToken cancellationToken) => Results.Ok(await service.GetStoresStatusAsync(cancellationToken)))
    .WithName("GetCrmFoundationPersistenceStoresStatus");

app.MapPost("/api/crm/foundation/persistence/stores/clear-preview", async (CrmPersistenceSeamStatusService service, CrmFoundationPermissionGuard permissionGuard, CancellationToken cancellationToken) =>
{
    var permission = await permissionGuard.CheckAsync("crm.foundation.preview.clear", cancellationToken);
    var seam = permission.Allowed ? await service.ClearPreviewAsync(cancellationToken) : await service.GetStatusAsync(cancellationToken);
    return Results.Ok(new
    {
        foundationMode = true,
        permissionSimulation = permission.SimulationMode,
        requiredPermission = permission.Permission,
        allowed = permission.Allowed,
        warning = permission.Warning,
        seam
    });
})
    .WithName("ClearCrmFoundationPersistenceStorePreview");

app.MapGet("/api/crm/foundation/portal-authorization/simulation-status", async (CrmPortalAuthorizationSimulationService service, CancellationToken cancellationToken) => Results.Ok(await service.GetStatusAsync(cancellationToken)))
    .WithName("GetCrmFoundationPortalAuthorizationSimulationStatus");

app.MapGet("/api/crm/foundation/portal-authorization/scenarios", (CrmPortalAuthorizationSimulationService service) => Results.Ok(service.GetScenarios()))
    .WithName("GetCrmFoundationPortalAuthorizationScenarios");

app.MapGet("/api/crm/foundation/portal-authorization/permissions", (CrmPortalAuthorizationSimulationService service) => Results.Ok(service.GetPermissions()))
    .WithName("GetCrmFoundationPortalAuthorizationPermissions");

app.MapGet("/api/crm/foundation/portal-authorization/sample-user-context", async (CrmPortalAuthorizationSimulationService service, CancellationToken cancellationToken) => Results.Ok(await service.GetSampleUserContextAsync(cancellationToken)))
    .WithName("GetCrmFoundationPortalAuthorizationSampleUserContext");

app.MapPost("/api/crm/foundation/portal-authorization/check-permission", async (CrmPortalPermissionCheckRequest request, CrmPortalAuthorizationSimulationService service, CancellationToken cancellationToken) => Results.Ok(await service.CheckPermissionAsync(request.RequiredPermission, cancellationToken)))
    .WithName("CheckCrmFoundationPortalAuthorizationPermission");

app.Run();

public partial class Program
{
}
