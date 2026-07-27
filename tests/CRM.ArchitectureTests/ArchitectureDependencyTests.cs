using System.Reflection;
using Xunit;

namespace CRM.ArchitectureTests;

public sealed class ArchitectureDependencyTests
{
    [Fact]
    public void Domain_DoesNotDependOnApplicationInfrastructureOrApi()
    {
        var references = ReferencedAssemblyNames(typeof(Domain.AssemblyMarker).Assembly);

        Assert.DoesNotContain("CRM.Application", references);
        Assert.DoesNotContain("CRM.Infrastructure", references);
        Assert.DoesNotContain("CRM.Api", references);
    }

    [Fact]
    public void Application_DoesNotDependOnInfrastructureOrApi()
    {
        var references = ReferencedAssemblyNames(typeof(Application.Foundation.CrmReadinessService).Assembly);

        Assert.DoesNotContain("CRM.Infrastructure", references);
        Assert.DoesNotContain("CRM.Api", references);
    }

    [Fact]
    public void Domain_IsNotCoupledToIdentity()
    {
        var references = ReferencedAssemblyNames(typeof(Domain.AssemblyMarker).Assembly);

        Assert.DoesNotContain(references, name => name.Contains("Identity", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void Infrastructure_DoesNotContainProductivePersistenceYet()
    {
        var source = ReadSourceFiles(Path.Combine("src", "CRM.Infrastructure"));
        var dbContextScanSource = StripAllowedEfPrototypeMarkers(source);

        Assert.DoesNotContain("DbContext", dbContextScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbSet<", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MigrationBuilder", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Add" + "DbContext", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseNpgsql", source, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Api_OnlyExposesAllowedContractAndFoundationPreviewEndpoints()
    {
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));

        Assert.Contains("/api/crm/domain-catalog", program);
        Assert.Contains("/api/crm/contracts", program);
        Assert.Contains("/api/crm/integration-boundaries", program);
        Assert.Contains("/api/crm/foundation/leads/preview", program);
        Assert.Contains("/api/crm/foundation/accounts/preview", program);
        Assert.Contains("/api/crm/foundation/contacts/preview", program);
        Assert.Contains("/api/crm/foundation/leads/read-model-preview", program);
        Assert.Contains("/api/crm/foundation/accounts/read-model-preview", program);
        Assert.Contains("/api/crm/foundation/contacts/read-model-preview", program);
        Assert.Contains("/api/crm/foundation/read-model-status", program);
        Assert.Contains("/api/crm/foundation/portal-integration/status", program);
        Assert.Contains("/api/crm/foundation/portal-integration/contracts", program);
        Assert.Contains("/api/crm/foundation/portal-integration/required-capabilities", program);
        Assert.Contains("/api/crm/foundation/financial-integration/status", program);
        Assert.Contains("/api/crm/foundation/financial-integration/contracts", program);
        Assert.Contains("/api/crm/foundation/financial-integration/required-capabilities", program);
        Assert.Contains("/api/crm/foundation/financial-integration/events", program);
        Assert.Contains("/api/crm/foundation/reporting/status", program);
        Assert.Contains("/api/crm/foundation/reporting/kpis", program);
        Assert.Contains("/api/crm/foundation/reporting/dashboards", program);
        Assert.Contains("/api/crm/foundation/reporting/analytics-read-models", program);
        Assert.Contains("/api/crm/foundation/sprint-1/closure-status", program);
        Assert.Contains("/api/crm/foundation/persistence/readiness", program);
        Assert.Contains("/api/crm/foundation/persistence/seam-status", program);
        Assert.Contains("/api/crm/foundation/persistence/feature-flags", program);
        Assert.Contains("/api/crm/foundation/persistence/stores/status", program);
        Assert.Contains("/api/crm/foundation/persistence/stores/clear-preview", program);
        Assert.Contains("/api/crm/foundation/portal-authorization/simulation-status", program);
        Assert.Contains("/api/crm/foundation/portal-authorization/scenarios", program);
        Assert.Contains("/api/crm/foundation/portal-authorization/permissions", program);
        Assert.Contains("/api/crm/foundation/portal-authorization/sample-user-context", program);
        Assert.Contains("/api/crm/foundation/portal-authorization/check-permission", program);
        Assert.Contains("/api/crm/foundation/crud/status", program);
        Assert.Contains("/api/crm/foundation/sprint-2/integration-readiness", program);
        Assert.Contains("/api/crm/foundation/sprint-2/productization-gate", program);
        Assert.Contains("/api/crm/foundation/sprint-3/durable-persistence-setup", program);
        Assert.Contains("/api/crm/foundation/sprint-3/common-db-connection-strategy", program);
        Assert.Contains("/api/crm/foundation/sprint-3/ef-prototype-status", program);
        Assert.Contains("/api/crm/foundation/sprint-3/portal-auth-runtime-contract", program);
        Assert.Contains("/api/crm/foundation/sprint-3/productive-api-route-draft", program);
        Assert.Contains("/api/crm/foundation/sprint-3/productization-review", program);
        Assert.Contains("/api/crm/foundation/sprint-4/runtime-readiness", program);
        Assert.Contains("/api/crm/foundation/sprint-4/common-db-runtime-probe", program);
        Assert.Contains("/api/crm/foundation/sprint-4/portal-auth-runtime-probe", program);
        Assert.Contains("/api/crm/foundation/sprint-4/productive-routes-locked-stub", program);
        Assert.Contains("/api/crm/foundation/sprint-4/nonproduction-e2e-pilot-readiness", program);
        Assert.Contains("/api/crm/foundation/sprint-4/gate-decision", program);
        Assert.Contains("/api/crm/foundation/sprint-5/runtime-probe-activation-plan", program);
        Assert.Contains("/api/crm/foundation/sprint-5/secret-provider-runtime-contract", program);
        Assert.Contains("/api/crm/foundation/sprint-5/common-db-probe-optional-activation", program);
        Assert.Contains("/api/crm/foundation/sprint-5/portal-auth-probe-optional-activation", program);
        Assert.Contains("/api/crm/foundation/sprint-5/locked-productive-route-stub-trial", program);
        Assert.Contains("/api/crm/foundation/leads", program);
        Assert.Contains("/api/crm/foundation/accounts", program);
        Assert.Contains("/api/crm/foundation/contacts", program);
        Assert.DoesNotContain("\"/api/crm/leads\"", program);
        Assert.DoesNotContain("\"/api/crm/accounts\"", program);
        Assert.DoesNotContain("\"/api/crm/contacts\"", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/", program.Replace("MapPut(\"/api/crm/foundation/leads/{id}\"", string.Empty, StringComparison.Ordinal)
            .Replace("MapPut(\"/api/crm/foundation/accounts/{id}\"", string.Empty, StringComparison.Ordinal)
            .Replace("MapPut(\"/api/crm/foundation/contacts/{id}\"", string.Empty, StringComparison.Ordinal));
        Assert.DoesNotContain("MapPatch", program);
        Assert.DoesNotContain("MapDelete", program);
        Assert.DoesNotContain("Create" + "Lead", program);
    }

    [Fact]
    public void EfPrototype_IsDisabledAndDoesNotActivateRuntime()
    {
        var source = ReadSourceFiles("src", "frontend", "docker-compose.yml", "docker-compose.crm.yml");
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));
        var dbContextScanSource = StripAllowedEfPrototypeMarkers(source);
        var connectionScanSource = StripAllowedConnectionStringMarkers(source);

        Assert.Contains("CrmEfPrototypeStatusService", source);
        Assert.Contains("CrmDbContextPrototype", source);
        Assert.Contains("CrmEfPrototypeMarker", source);
        Assert.Contains("EF/DbContext prototype only; runtime disabled and no database configured", source);
        Assert.Contains("CRM_EF_RUNTIME_ENABLED=false", source);
        Assert.Contains("CRM_DBCONTEXT_RUNTIME_ACTIVE=false", source);
        Assert.Contains("Sprint3P4PortalAuthRuntimeContractValidation", source);
        Assert.Contains("MapGet(\"/api/crm/foundation/sprint-3/ef-prototype-status\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/sprint-3/ef-prototype-status", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/sprint-3/ef-prototype-status", program);
        Assert.DoesNotContain("MapDelete", program);
        Assert.DoesNotContain("\"/api/crm/leads\"", program);
        Assert.DoesNotContain("\"/api/crm/accounts\"", program);
        Assert.DoesNotContain("\"/api/crm/contacts\"", program);
        Assert.DoesNotContain("DbContext", dbContextScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbSet<", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MigrationBuilder", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseNpgsql", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Add" + "DbContext", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ConnectionString", connectionScanSource, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void PortalAuthRuntimeContract_IsContractOnlyAndDoesNotActivateAuth()
    {
        var source = ReadSourceFiles("src", "frontend", "docker-compose.yml", "docker-compose.crm.yml");
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));
        var dbContextScanSource = StripAllowedEfPrototypeMarkers(source);
        var connectionScanSource = StripAllowedConnectionStringMarkers(source);

        Assert.Contains("CrmPortalAuthRuntimeContractStatusService", source);
        Assert.Contains("PortalAuthRuntimeAdapterPlaceholder", source);
        Assert.Contains("PortalAuthContextMapperPlaceholder", source);
        Assert.Contains("Portal Auth runtime contract validation only; no real Auth runtime configured", source);
        Assert.Contains("PortalAuthRuntimeContractValidation", source);
        Assert.Contains("Sprint3P5ProductiveApiRouteDraftBehindDisabledFlag", source);
        Assert.Contains("MapGet(\"/api/crm/foundation/sprint-3/portal-auth-runtime-contract\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/sprint-3/portal-auth-runtime-contract", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/sprint-3/portal-auth-runtime-contract", program);
        Assert.DoesNotContain("MapDelete", program);
        Assert.DoesNotContain("\"/api/crm/leads\"", program);
        Assert.DoesNotContain("\"/api/crm/accounts\"", program);
        Assert.DoesNotContain("\"/api/crm/contacts\"", program);
        Assert.DoesNotContain("/login", program, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("/logout", program, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Add" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authorization", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("AuthorizeAttribute", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Jwt" + "Bearer", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Cookie" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("local" + "Storage", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("session" + "Storage", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalBaseUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalCorporativoUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbContext", dbContextScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbSet<", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MigrationBuilder", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ConnectionString", connectionScanSource, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void PortalAuthRuntimeProbe_IsDisabledAndDoesNotReadTokensOrCallPortal()
    {
        var source = ReadSourceFiles("src", "frontend", "docker-compose.yml", "docker-compose.crm.yml");
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));
        var dbContextScanSource = StripAllowedEfPrototypeMarkers(source);
        var connectionScanSource = StripAllowedConnectionStringMarkers(source);

        Assert.Contains("CrmPortalAuthRuntimeProbeStatusService", source);
        Assert.Contains("PortalAuthRuntimeProbePlaceholder", source);
        Assert.Contains("Portal Auth runtime probe exists but is disabled; no tokens are read and no Portal HTTP calls are attempted", source);
        Assert.Contains("PortalAuthRuntimeProbe", source);
        Assert.Contains("Sprint4P4ProductiveRoutesLockedStubValidation", source);
        Assert.Contains("tokenReadAttemptedByRuntime: false", source);
        Assert.Contains("portalHttpAttemptedByRuntime: false", source);
        Assert.Contains("MapGet(\"/api/crm/foundation/sprint-4/portal-auth-runtime-probe\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/sprint-4/portal-auth-runtime-probe", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/sprint-4/portal-auth-runtime-probe", program);
        Assert.DoesNotContain("MapDelete", program);
        Assert.DoesNotContain("\"/api/crm/leads\"", program);
        Assert.DoesNotContain("\"/api/crm/accounts\"", program);
        Assert.DoesNotContain("\"/api/crm/contacts\"", program);
        Assert.DoesNotContain("/login", program, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("/logout", program, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Add" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authorization", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("AuthorizeAttribute", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Jwt" + "Bearer", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Cookie" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("local" + "Storage", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("session" + "Storage", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalBaseUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalCorporativoUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbContext", dbContextScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbSet<", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MigrationBuilder", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ConnectionString", connectionScanSource, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void ProductiveRoutesLockedStubValidation_DoesNotRegisterProductiveRoutes()
    {
        var source = ReadSourceFiles("src", "frontend", "docker-compose.yml", "docker-compose.crm.yml");
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));
        var dbContextScanSource = StripAllowedEfPrototypeMarkers(source);
        var connectionScanSource = StripAllowedConnectionStringMarkers(source);

        Assert.Contains("CrmProductiveRoutesLockedStubStatusService", source);
        Assert.Contains("ProductiveRoutesLockedStubValidation", source);
        Assert.Contains("DocumentOnlyPreferred", source);
        Assert.Contains("Productive routes locked stub validation only; no productive routes are active", source);
        Assert.Contains("Sprint4P5NonProductionE2EPilotReadiness", source);
        Assert.Contains("lockedStubsRegistered: false", source);
        Assert.Contains("p4FoundationCrudStillSeparate: true", source);
        Assert.Contains("MapGet(\"/api/crm/foundation/sprint-4/productive-routes-locked-stub\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/sprint-4/productive-routes-locked-stub", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/sprint-4/productive-routes-locked-stub", program);
        Assert.DoesNotContain("MapDelete", program);
        Assert.DoesNotContain("\"/api/crm/leads\"", program);
        Assert.DoesNotContain("\"/api/crm/accounts\"", program);
        Assert.DoesNotContain("\"/api/crm/contacts\"", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/contacts", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/contacts", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/contacts", program);
        Assert.DoesNotContain("Add" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authorization", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("AuthorizeAttribute", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalBaseUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalCorporativoUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbContext", dbContextScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbSet<", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MigrationBuilder", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ConnectionString", connectionScanSource, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void NonProductionE2EPilotReadiness_IsFoundationOnlyAndDoesNotActivateRuntime()
    {
        var source = ReadSourceFiles("src", "frontend", "docker-compose.yml", "docker-compose.crm.yml");
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));
        var dbContextScanSource = StripAllowedEfPrototypeMarkers(source);
        var connectionScanSource = StripAllowedConnectionStringMarkers(source);

        Assert.Contains("CrmNonProductionE2EPilotReadinessStatusService", source);
        Assert.Contains("NonProductionE2EPilotReadiness", source);
        Assert.Contains("Non-production E2E pilot readiness only; no real activation", source);
        Assert.Contains("Sprint4P6Sprint4GateDecision", source);
        Assert.Contains("e2ePilotCanRun: true", source);
        Assert.Contains("e2ePilotScope: 'FoundationOnly'", source);
        Assert.Contains("productiveRoutesUsed: false", source);
        Assert.Contains("realDatabaseUsed: false", source);
        Assert.Contains("portalAuthRuntimeUsed: false", source);
        Assert.Contains("durablePersistenceUsed: false", source);
        Assert.Contains("deleteOperationsUsed: false", source);
        Assert.Contains("syntheticDataOnly: true", source);
        Assert.Contains("foundationEndpointsOnly: true", source);
        Assert.Contains("negativeRouteValidationRequired: true", source);
        Assert.Contains("MapGet(\"/api/crm/foundation/sprint-4/nonproduction-e2e-pilot-readiness\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/sprint-4/nonproduction-e2e-pilot-readiness", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/sprint-4/nonproduction-e2e-pilot-readiness", program);
        Assert.DoesNotContain("MapDelete", program);
        Assert.DoesNotContain("\"/api/crm/leads\"", program);
        Assert.DoesNotContain("\"/api/crm/accounts\"", program);
        Assert.DoesNotContain("\"/api/crm/contacts\"", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/contacts", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/contacts", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/contacts", program);
        Assert.DoesNotContain("Add" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authorization", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("AuthorizeAttribute", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalBaseUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalCorporativoUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbContext", dbContextScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbSet<", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MigrationBuilder", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ConnectionString", connectionScanSource, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Sprint4GateDecision_IsFoundationOnlyAndDoesNotActivateRuntime()
    {
        var source = ReadSourceFiles("src", "frontend", "docker-compose.yml", "docker-compose.crm.yml");
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));
        var dbContextScanSource = StripAllowedEfPrototypeMarkers(source);
        var connectionScanSource = StripAllowedConnectionStringMarkers(source);

        Assert.Contains("CrmSprint4GateDecisionStatusService", source);
        Assert.Contains("Sprint4GateDecision", source);
        Assert.Contains("Sprint 4 gate decision only; no real activation", source);
        Assert.Contains("GoForNonProductionFoundationPilot", source);
        Assert.Contains("NoGoForRuntimeActivation", source);
        Assert.Contains("Sprint5P1ControlledRuntimeProbeActivationPlan", source);
        Assert.Contains("sprint4: 'Closed'", source);
        Assert.Contains("sprint4GateDecision: 'Completed'", source);
        Assert.Contains("sprint4OverallDecision: 'GoForNonProductionFoundationPilot'", source);
        Assert.Contains("realActivationDecision: 'NoGo'", source);
        Assert.Contains("commonDbRuntimeDecision: 'NoGoForRuntimeActivation'", source);
        Assert.Contains("sprint4PortalAuthRuntimeDecision: 'NoGoForRuntimeActivation'", source);
        Assert.Contains("productiveRoutesDecision: 'NoGo'", source);
        Assert.Contains("deleteDecision: 'NoGo'", source);
        Assert.Contains("nonProductionE2EPilotDecision: 'GoFoundationOnly'", source);
        Assert.Contains("sprint5PlanningDecision: 'Go'", source);
        Assert.Contains("MapGet(\"/api/crm/foundation/sprint-4/gate-decision\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/sprint-4/gate-decision", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/sprint-4/gate-decision", program);
        Assert.DoesNotContain("MapDelete", program);
        Assert.DoesNotContain("\"/api/crm/leads\"", program);
        Assert.DoesNotContain("\"/api/crm/accounts\"", program);
        Assert.DoesNotContain("\"/api/crm/contacts\"", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/contacts", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/contacts", program);
        Assert.DoesNotContain("Add" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authorization", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("AuthorizeAttribute", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalBaseUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalCorporativoUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbContext", dbContextScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbSet<", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MigrationBuilder", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ConnectionString", connectionScanSource, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void ProductiveApiRouteDraft_IsDocumentedOnlyAndDoesNotRegisterProductiveRoutes()
    {
        var source = ReadSourceFiles("src", "frontend", "docker-compose.yml", "docker-compose.crm.yml");
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));
        var dbContextScanSource = StripAllowedEfPrototypeMarkers(source);
        var connectionScanSource = StripAllowedConnectionStringMarkers(source);

        Assert.Contains("CrmProductiveApiRouteDraftStatusService", source);
        Assert.Contains("ProductiveApiRouteDraft", source);
        Assert.Contains("Productive API route draft only; routes are not active", source);
        Assert.Contains("Sprint3P6Sprint3ProductizationReview", source);
        Assert.Contains("MapGet(\"/api/crm/foundation/sprint-3/productive-api-route-draft\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/sprint-3/productive-api-route-draft", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/sprint-3/productive-api-route-draft", program);
        Assert.DoesNotContain("MapDelete", program);
        Assert.DoesNotContain("\"/api/crm/leads\"", program);
        Assert.DoesNotContain("\"/api/crm/accounts\"", program);
        Assert.DoesNotContain("\"/api/crm/contacts\"", program);
        Assert.DoesNotContain("Add" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authorization", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("AuthorizeAttribute", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Jwt" + "Bearer", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Cookie" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalBaseUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalCorporativoUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbContext", dbContextScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbSet<", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MigrationBuilder", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ConnectionString", connectionScanSource, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Sprint3ProductizationReview_IsNoGoAndDoesNotActivateRuntime()
    {
        var source = ReadSourceFiles("src", "frontend", "docker-compose.yml", "docker-compose.crm.yml");
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));
        var dbContextScanSource = StripAllowedEfPrototypeMarkers(source);
        var connectionScanSource = StripAllowedConnectionStringMarkers(source);

        Assert.Contains("CrmSprint3ProductizationReviewStatusService", source);
        Assert.Contains("Sprint3ProductizationReview", source);
        Assert.Contains("NoGoForRealActivation", source);
        Assert.Contains("Sprint 3 productization review only; no real activation", source);
        Assert.Contains("Sprint4P1RuntimeEnvironmentReadinessAndLocalToolingHardening", source);
        Assert.Contains("MapGet(\"/api/crm/foundation/sprint-3/productization-review\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/sprint-3/productization-review", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/sprint-3/productization-review", program);
        Assert.DoesNotContain("MapDelete", program);
        Assert.DoesNotContain("\"/api/crm/leads\"", program);
        Assert.DoesNotContain("\"/api/crm/accounts\"", program);
        Assert.DoesNotContain("\"/api/crm/contacts\"", program);
        Assert.DoesNotContain("Add" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authorization", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("AuthorizeAttribute", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Jwt" + "Bearer", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Cookie" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("local" + "Storage", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("session" + "Storage", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalBaseUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalCorporativoUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbContext", dbContextScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbSet<", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MigrationBuilder", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ConnectionString", connectionScanSource, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void RuntimeEnvironmentReadiness_IsFoundationOnlyAndDoesNotActivateRuntime()
    {
        var source = ReadSourceFiles("src", "frontend", "docker-compose.yml", "docker-compose.crm.yml");
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));
        var dbContextScanSource = StripAllowedEfPrototypeMarkers(source);
        var connectionScanSource = StripAllowedConnectionStringMarkers(source);

        Assert.Contains("CrmRuntimeEnvironmentReadinessStatusService", source);
        Assert.Contains("RuntimeEnvironmentReadiness", source);
        Assert.Contains("Runtime readiness only; no real activation", source);
        Assert.Contains("Sprint4P2ControlledCommonDbRuntimeProbeBehindDisabledFlag", source);
        Assert.Contains("MapGet(\"/api/crm/foundation/sprint-4/runtime-readiness\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/sprint-4/runtime-readiness", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/sprint-4/runtime-readiness", program);
        Assert.DoesNotContain("MapDelete", program);
        Assert.DoesNotContain("\"/api/crm/leads\"", program);
        Assert.DoesNotContain("\"/api/crm/accounts\"", program);
        Assert.DoesNotContain("\"/api/crm/contacts\"", program);
        Assert.DoesNotContain("Add" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authorization", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("AuthorizeAttribute", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Jwt" + "Bearer", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Cookie" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("local" + "Storage", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("session" + "Storage", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalBaseUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalCorporativoUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbContext", dbContextScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbSet<", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MigrationBuilder", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ConnectionString", connectionScanSource, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void CommonDbRuntimeProbe_IsDisabledAndDoesNotConnectDatabase()
    {
        var source = ReadSourceFiles("src", "frontend", "docker-compose.yml", "docker-compose.crm.yml");
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));
        var dbContextScanSource = StripAllowedEfPrototypeMarkers(source);
        var connectionScanSource = StripAllowedConnectionStringMarkers(source);

        Assert.Contains("CrmCommonDbRuntimeProbeStatusService", source);
        Assert.Contains("CommonDbRuntimeProbePlaceholder", source);
        Assert.Contains("CommonDbRuntimeProbe", source);
        Assert.Contains("Common DB runtime probe exists but is disabled; no database connection is attempted", source);
        Assert.Contains("Sprint4P3PortalAuthRuntimeProbeBehindDisabledFlag", source);
        Assert.Contains("MapGet(\"/api/crm/foundation/sprint-4/common-db-runtime-probe\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/sprint-4/common-db-runtime-probe", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/sprint-4/common-db-runtime-probe", program);
        Assert.DoesNotContain("MapDelete", program);
        Assert.DoesNotContain("\"/api/crm/leads\"", program);
        Assert.DoesNotContain("\"/api/crm/accounts\"", program);
        Assert.DoesNotContain("\"/api/crm/contacts\"", program);
        Assert.DoesNotContain("Add" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authorization", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("AuthorizeAttribute", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Jwt" + "Bearer", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Cookie" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalBaseUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalCorporativoUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbContext", dbContextScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbSet<", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MigrationBuilder", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ConnectionString", connectionScanSource, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Source_DoesNotContainIdentityTokenStorageOrOwnSqlServer()
    {
        var source = ReadSourceFiles("src", "tests", "frontend", "docker-compose.yml", "docker-compose.crm.yml");

        Assert.DoesNotContain("Microsoft.AspNetCore." + "Identity", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Add" + "Identity", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("local" + "Storage", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("session" + "Storage", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("mcr.microsoft.com/" + "mssql", source, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void PortalPorts_AreApplicationInterfacesOnly()
    {
        var root = FindRepositoryRoot();
        var portsPath = Path.Combine(root, "src", "CRM.Application", "Ports", "Portal");

        Assert.True(Directory.Exists(portsPath));
        foreach (var file in Directory.EnumerateFiles(portsPath, "*.cs"))
        {
            var source = File.ReadAllText(file);

            Assert.Contains("interface", source, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("FuturePortalAdapter", source, StringComparison.Ordinal);
            Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
            Assert.DoesNotContain("BaseUrl", source, StringComparison.OrdinalIgnoreCase);
        }
    }

    [Fact]
    public void Domain_DoesNotReferencePortalIntegrationPorts()
    {
        var source = ReadSourceFiles(Path.Combine("src", "CRM.Domain"));

        Assert.DoesNotContain("CRM.Application.Ports.Portal", source, StringComparison.Ordinal);
        Assert.DoesNotContain("IPortal", source, StringComparison.Ordinal);
    }

    [Fact]
    public void PortalPlaceholder_DoesNotMakeRuntimePortalCalls()
    {
        var source = ReadSourceFiles("src", "CRM.Infrastructure");

        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalCorporativoUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.Contains("NonProductionPlaceholder", source, StringComparison.Ordinal);
    }

    [Fact]
    public void PortalEndpoints_AreFoundationGetOnly()
    {
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));

        Assert.Contains("MapGet(\"/api/crm/foundation/portal-integration/status\"", program);
        Assert.Contains("MapGet(\"/api/crm/foundation/portal-integration/contracts\"", program);
        Assert.Contains("MapGet(\"/api/crm/foundation/portal-integration/required-capabilities\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/portal-integration", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/portal-integration", program);
        Assert.DoesNotContain("MapDelete(\"/api/crm/foundation/portal-integration", program);
    }

    [Fact]
    public void FinancialPorts_AreApplicationInterfacesOnly()
    {
        var root = FindRepositoryRoot();
        var portsPath = Path.Combine(root, "src", "CRM.Application", "Ports", "Financial");

        Assert.True(Directory.Exists(portsPath));
        foreach (var file in Directory.EnumerateFiles(portsPath, "*.cs"))
        {
            var source = File.ReadAllText(file);

            Assert.Contains("interface", source, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("FutureFinancialAdapter", source, StringComparison.Ordinal);
            Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
            Assert.DoesNotContain("ConnectionString", source.Replace("ConnectionStringsConfigured", string.Empty, StringComparison.Ordinal).Replace("connectionStringsConfigured", string.Empty, StringComparison.Ordinal).Replace("Connection Strings Configured", string.Empty, StringComparison.Ordinal).Replace("CrmConnectionStringPolicyContract", string.Empty, StringComparison.Ordinal).Replace("ConnectionStringPolicy", string.Empty, StringComparison.Ordinal).Replace("connectionStringPolicy", string.Empty, StringComparison.Ordinal), StringComparison.OrdinalIgnoreCase);
            Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
        }
    }

    [Fact]
    public void Domain_DoesNotReferenceFinancialIntegrationPorts()
    {
        var source = ReadSourceFiles(Path.Combine("src", "CRM.Domain"));

        Assert.DoesNotContain("CRM.Application.Ports.Financial", source, StringComparison.Ordinal);
        Assert.DoesNotContain("IFinancial", source, StringComparison.Ordinal);
    }

    [Fact]
    public void FinancialPlaceholder_DoesNotMakeRuntimeFinancialCallsOrDbAccess()
    {
        var source = ReadSourceFiles("src", "CRM.Infrastructure");

        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("FinancieroUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ConnectionString", source.Replace("ConnectionStringsConfigured", string.Empty, StringComparison.Ordinal).Replace("connectionStringsConfigured", string.Empty, StringComparison.Ordinal).Replace("Connection Strings Configured", string.Empty, StringComparison.Ordinal).Replace("CrmConnectionStringPolicyContract", string.Empty, StringComparison.Ordinal).Replace("ConnectionStringPolicy", string.Empty, StringComparison.Ordinal).Replace("connectionStringPolicy", string.Empty, StringComparison.Ordinal), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.Contains("NonProductionPlaceholder", source, StringComparison.Ordinal);
    }

    [Fact]
    public void FinancialEndpoints_AreFoundationGetOnly()
    {
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));

        Assert.Contains("MapGet(\"/api/crm/foundation/financial-integration/status\"", program);
        Assert.Contains("MapGet(\"/api/crm/foundation/financial-integration/contracts\"", program);
        Assert.Contains("MapGet(\"/api/crm/foundation/financial-integration/required-capabilities\"", program);
        Assert.Contains("MapGet(\"/api/crm/foundation/financial-integration/events\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/financial-integration", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/financial-integration", program);
        Assert.DoesNotContain("MapDelete(\"/api/crm/foundation/financial-integration", program);
    }

    [Fact]
    public void Source_DoesNotReferenceFinancieroRuntimeSriOrSharedDatabase()
    {
        var source = ReadSourceFiles("src", "docker-compose.yml", "docker-compose.crm.yml");

        Assert.DoesNotContain("ProjectReference Include=\"..\\..\\Financiero", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("FinancieroDb", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("SharedDatabase", source.Replace("NoSharedDatabase", string.Empty, StringComparison.Ordinal), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("SriClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("RIDE", source, StringComparison.Ordinal);
        Assert.DoesNotContain("XAdES", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ATS", source, StringComparison.Ordinal);
    }

    [Fact]
    public void ReportingPorts_AreApplicationInterfacesOnly()
    {
        var root = FindRepositoryRoot();
        var portsPath = Path.Combine(root, "src", "CRM.Application", "Ports", "Reporting");

        Assert.True(Directory.Exists(portsPath));
        foreach (var file in Directory.EnumerateFiles(portsPath, "*.cs"))
        {
            var source = File.ReadAllText(file);

            Assert.Contains("interface", source, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("FutureReportingAdapter", source, StringComparison.Ordinal);
            Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
            Assert.DoesNotContain("PowerBI", source, StringComparison.Ordinal);
            Assert.DoesNotContain("EmbedToken", source, StringComparison.OrdinalIgnoreCase);
        }
    }

    [Fact]
    public void Domain_DoesNotReferenceReportingPorts()
    {
        var source = ReadSourceFiles(Path.Combine("src", "CRM.Domain"));

        Assert.DoesNotContain("CRM.Application.Ports.Reporting", source, StringComparison.Ordinal);
        Assert.DoesNotContain("ICrmKpi", source, StringComparison.Ordinal);
        Assert.DoesNotContain("ICrmDashboard", source, StringComparison.Ordinal);
    }

    [Fact]
    public void ReportingPlaceholder_DoesNotUsePowerBiRuntimeOrIds()
    {
        var source = ReadSourceFiles(Path.Combine("src", "CRM.Infrastructure"))
            .Replace("FuturePowerBiEmbedding", string.Empty, StringComparison.Ordinal)
            .Replace("\"EmbedToken\"", string.Empty, StringComparison.Ordinal);

        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Microsoft.PowerBI", source, StringComparison.Ordinal);
        Assert.DoesNotContain("EmbedToken", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("WorkspaceId", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ReportId", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DatasetId", source, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("NonProductionPlaceholder", source, StringComparison.Ordinal);
    }

    [Fact]
    public void ReportingEndpoints_AreFoundationGetOnly()
    {
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));

        Assert.Contains("MapGet(\"/api/crm/foundation/reporting/status\"", program);
        Assert.Contains("MapGet(\"/api/crm/foundation/reporting/kpis\"", program);
        Assert.Contains("MapGet(\"/api/crm/foundation/reporting/dashboards\"", program);
        Assert.Contains("MapGet(\"/api/crm/foundation/reporting/analytics-read-models\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/reporting", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/reporting", program);
        Assert.DoesNotContain("MapDelete(\"/api/crm/foundation/reporting", program);
    }

    [Fact]
    public void ClosureEndpoint_IsFoundationGetOnly()
    {
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));

        Assert.Contains("MapGet(\"/api/crm/foundation/sprint-1/closure-status\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/sprint-1", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/sprint-1", program);
        Assert.DoesNotContain("MapDelete(\"/api/crm/foundation/sprint-1", program);
        Assert.Contains("Foundation closure only; no productive activation", ReadSourceFiles("src", "CRM.Application"));
    }

    [Fact]
    public void PersistenceReadinessEndpoint_IsFoundationGetOnly()
    {
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));

        Assert.Contains("MapGet(\"/api/crm/foundation/persistence/readiness\"", program);
        Assert.Contains("MapGet(\"/api/crm/foundation/persistence/seam-status\"", program);
        Assert.Contains("MapGet(\"/api/crm/foundation/persistence/feature-flags\"", program);
        Assert.Contains("MapGet(\"/api/crm/foundation/persistence/stores/status\"", program);
        Assert.Contains("MapPost(\"/api/crm/foundation/persistence/stores/clear-preview\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/persistence/readiness", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/persistence/seam-status", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/persistence/feature-flags", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/persistence/stores/status", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/persistence", program);
        Assert.DoesNotContain("MapDelete(\"/api/crm/foundation/persistence", program);
        Assert.Contains("Persistence design review only; no database configured", ReadSourceFiles("src", "CRM.Application"));
        Assert.Contains("Non-production persistence seam only; no database configured", ReadSourceFiles("src", "CRM.Application"));
    }

    [Fact]
    public void PortalAuthorizationSimulation_DoesNotAddProductiveAuthentication()
    {
        var source = ReadSourceFiles("src", "frontend", "docker-compose.yml", "docker-compose.crm.yml");
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));

        Assert.Contains("CrmPortalAuthorizationSimulationService", source);
        Assert.Contains("SimulatedPortalUserContextProvider", source);
        Assert.Contains("SimulatedPortalPermissionProvider", source);
        Assert.Contains("SimulatedPortalAuthorizationScenarioProvider", source);
        Assert.Contains("CrmFoundationPermissionGuard", source);
        Assert.Contains("FoundationSimulation", source);
        Assert.Contains("Portal authorization simulation only; no real Portal runtime configured", source);
        Assert.Contains("MapGet(\"/api/crm/foundation/portal-authorization/simulation-status\"", program);
        Assert.Contains("MapGet(\"/api/crm/foundation/portal-authorization/scenarios\"", program);
        Assert.Contains("MapGet(\"/api/crm/foundation/portal-authorization/permissions\"", program);
        Assert.Contains("MapGet(\"/api/crm/foundation/portal-authorization/sample-user-context\"", program);
        Assert.Contains("MapPost(\"/api/crm/foundation/portal-authorization/check-permission\"", program);
        Assert.DoesNotContain("Add" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authorization", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("AuthorizeAttribute", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Jwt" + "Bearer", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalBaseUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalCorporativoUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MapDelete", program, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void FoundationCrud_IsOnlyFoundationAndNonProductive()
    {
        var source = ReadSourceFiles("src", "frontend", "docker-compose.yml", "docker-compose.crm.yml");
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));

        Assert.Contains("FoundationLeadCrudService", source);
        Assert.Contains("FoundationAccountCrudService", source);
        Assert.Contains("FoundationContactCrudService", source);
        Assert.Contains("FoundationCrudStatusService", source);
        Assert.Contains("Foundation CRUD only; no productive endpoint or database configured", source);
        Assert.Contains("MapGet(\"/api/crm/foundation/crud/status\"", program);
        Assert.Contains("MapGet(\"/api/crm/foundation/leads\"", program);
        Assert.Contains("MapPost(\"/api/crm/foundation/leads\"", program);
        Assert.Contains("MapPut(\"/api/crm/foundation/leads/{id}\"", program);
        Assert.Contains("MapGet(\"/api/crm/foundation/accounts\"", program);
        Assert.Contains("MapPost(\"/api/crm/foundation/accounts\"", program);
        Assert.Contains("MapPut(\"/api/crm/foundation/accounts/{id}\"", program);
        Assert.Contains("MapGet(\"/api/crm/foundation/contacts\"", program);
        Assert.Contains("MapPost(\"/api/crm/foundation/contacts\"", program);
        Assert.Contains("MapPut(\"/api/crm/foundation/contacts/{id}\"", program);
        Assert.DoesNotContain("\"/api/crm/leads\"", program);
        Assert.DoesNotContain("\"/api/crm/accounts\"", program);
        Assert.DoesNotContain("\"/api/crm/contacts\"", program);
        Assert.DoesNotContain("MapDelete", program);
        Assert.DoesNotContain("DbContext", StripAllowedEfPrototypeMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbSet<", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MigrationBuilder", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void IntegrationReadinessReview_IsFoundationReadOnlyAndNonProductive()
    {
        var source = ReadSourceFiles("src", "frontend", "docker-compose.yml", "docker-compose.crm.yml");
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));

        Assert.Contains("CrmSprint2IntegrationReadinessService", source);
        Assert.Contains("Integration readiness review only; no productive activation", source);
        Assert.Contains("Sprint2P6ProductizationGateDecision", source);
        Assert.Contains("MapGet(\"/api/crm/foundation/sprint-2/integration-readiness\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/sprint-2", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/sprint-2", program);
        Assert.DoesNotContain("MapDelete", program);
        Assert.DoesNotContain("\"/api/crm/leads\"", program);
        Assert.DoesNotContain("\"/api/crm/accounts\"", program);
        Assert.DoesNotContain("\"/api/crm/contacts\"", program);
        Assert.DoesNotContain("DbContext", StripAllowedEfPrototypeMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbSet<", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MigrationBuilder", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void ProductizationGateDecision_ClosesSprint2WithoutProductiveActivation()
    {
        var source = ReadSourceFiles("src", "frontend", "docker-compose.yml", "docker-compose.crm.yml");
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));

        Assert.Contains("CrmSprint2ProductizationGateService", source);
        Assert.Contains("Productization gate decision only; no productive activation", source);
        Assert.Contains("Sprint2Closed", source);
        Assert.Contains("NoGoForProductiveActivation", source);
        Assert.Contains("GoFoundationOnly", source);
        Assert.Contains("Sprint3P1DurablePersistenceSetupDesign", source);
        Assert.Contains("MapGet(\"/api/crm/foundation/sprint-2/productization-gate\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/sprint-2/productization-gate", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/sprint-2/productization-gate", program);
        Assert.DoesNotContain("MapDelete", program);
        Assert.DoesNotContain("\"/api/crm/leads\"", program);
        Assert.DoesNotContain("\"/api/crm/accounts\"", program);
        Assert.DoesNotContain("\"/api/crm/contacts\"", program);
        Assert.DoesNotContain("DbContext", StripAllowedEfPrototypeMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbSet<", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MigrationBuilder", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ConnectionString", source.Replace("ConnectionStringsConfigured", string.Empty, StringComparison.Ordinal).Replace("connectionStringsConfigured", string.Empty, StringComparison.Ordinal).Replace("Connection Strings Configured", string.Empty, StringComparison.Ordinal).Replace("CrmConnectionStringPolicyContract", string.Empty, StringComparison.Ordinal).Replace("ConnectionStringPolicy", string.Empty, StringComparison.Ordinal).Replace("connectionStringPolicy", string.Empty, StringComparison.Ordinal), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalBaseUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalCorporativoUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Add" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Jwt" + "Bearer", source, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void DurablePersistenceSetupDesign_IsFoundationReadOnlyAndDoesNotActivateDatabase()
    {
        var source = ReadSourceFiles("src", "frontend", "docker-compose.yml", "docker-compose.crm.yml");
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));

        Assert.Contains("CrmDurablePersistenceSetupStatusService", source);
        Assert.Contains("Durable persistence setup design only; no database, EF runtime, migrations, or connection strings configured", source);
        Assert.Contains("DurablePersistenceSetupDesign", source);
        Assert.Contains("DesignOnly", source);
        Assert.Contains("Sprint3P2CommonDbConnectionContractAndSecretStrategy", source);
        Assert.Contains("MapGet(\"/api/crm/foundation/sprint-3/durable-persistence-setup\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/sprint-3/durable-persistence-setup", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/sprint-3/durable-persistence-setup", program);
        Assert.DoesNotContain("MapDelete", program);
        Assert.DoesNotContain("\"/api/crm/leads\"", program);
        Assert.DoesNotContain("\"/api/crm/accounts\"", program);
        Assert.DoesNotContain("\"/api/crm/contacts\"", program);
        Assert.DoesNotContain("DbContext", StripAllowedEfPrototypeMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbSet<", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MigrationBuilder", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ConnectionString", source.Replace("ConnectionStringsConfigured", string.Empty, StringComparison.Ordinal).Replace("connectionStringsConfigured", string.Empty, StringComparison.Ordinal).Replace("Connection Strings Configured", string.Empty, StringComparison.Ordinal).Replace("CrmConnectionStringPolicyContract", string.Empty, StringComparison.Ordinal).Replace("ConnectionStringPolicy", string.Empty, StringComparison.Ordinal).Replace("connectionStringPolicy", string.Empty, StringComparison.Ordinal), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalBaseUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalCorporativoUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Add" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Jwt" + "Bearer", source, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void CommonDbConnectionStrategy_IsContractOnlyAndDoesNotReadSecretsOrConnectDatabase()
    {
        var source = ReadSourceFiles("src", "frontend", "docker-compose.yml", "docker-compose.crm.yml");
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));
        var dbContextScanSource = StripAllowedEfPrototypeMarkers(source);
        var connectionScanSource = StripAllowedConnectionStringMarkers(source);

        Assert.Contains("CrmCommonDbConnectionStrategyStatusService", source);
        Assert.Contains("CrmSecretProviderPlaceholder", source);
        Assert.Contains("CrmDatabaseConfigurationPlaceholder", source);
        Assert.Contains("Common DB connection contract only; no real database or secrets configured", source);
        Assert.Contains("CommonDbConnectionStrategy", source);
        Assert.Contains("NoRealValuesInRepository", source);
        Assert.Contains("Sprint3P3EfDbContextPrototypeBehindDisabledFlag", source);
        Assert.Contains("MapGet(\"/api/crm/foundation/sprint-3/common-db-connection-strategy\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/sprint-3/common-db-connection-strategy", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/sprint-3/common-db-connection-strategy", program);
        Assert.DoesNotContain("MapDelete", program);
        Assert.DoesNotContain("\"/api/crm/leads\"", program);
        Assert.DoesNotContain("\"/api/crm/accounts\"", program);
        Assert.DoesNotContain("\"/api/crm/contacts\"", program);
        Assert.DoesNotContain("DbContext", dbContextScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbSet<", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MigrationBuilder", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ConnectionString", connectionScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalBaseUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalCorporativoUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("KeyVault", StripAllowedSecretProviderContractMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Azure.Security", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Add" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Jwt" + "Bearer", source, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void PersistenceSeam_UsesOnlyFoundationStorePortsAndInMemoryAdapters()
    {
        var applicationSource = ReadSourceFiles(Path.Combine("src", "CRM.Application"));
        var infrastructureSource = ReadSourceFiles(Path.Combine("src", "CRM.Infrastructure"));
        var source = applicationSource + Environment.NewLine + infrastructureSource;

        Assert.Contains("ILeadFoundationStore", applicationSource);
        Assert.Contains("IAccountFoundationStore", applicationSource);
        Assert.Contains("IContactFoundationStore", applicationSource);
        Assert.Contains("ICrmFoundationUnitOfWork", applicationSource);
        Assert.Contains("ICrmPersistenceFeatureFlagProvider", applicationSource);
        Assert.Contains("InMemoryLeadFoundationStore", infrastructureSource);
        Assert.Contains("InMemoryAccountFoundationStore", infrastructureSource);
        Assert.Contains("InMemoryContactFoundationStore", infrastructureSource);
        Assert.Contains("StaticCrmPersistenceFeatureFlagProvider", infrastructureSource);
        Assert.Contains("NonProductionSeam", source);
        Assert.Contains("ProductiveCrudEnabled", source);
        var repositoryScanSource = source.Replace("NoRealValuesInRepository", string.Empty, StringComparison.Ordinal)
            .Replace("RealValuesAllowedInRepository", string.Empty, StringComparison.Ordinal)
            .Replace("PasswordsAllowedInRepository", string.Empty, StringComparison.Ordinal);
        Assert.DoesNotContain("Repository", repositoryScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbContext", StripAllowedEfPrototypeMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbSet<", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MigrationBuilder", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void ControlledRuntimeProbeActivationPlan_IsFoundationOnlyAndDoesNotActivateRuntime()
    {
        var source = ReadSourceFiles("src", "frontend", "docker-compose.yml", "docker-compose.crm.yml");
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));
        var dbContextScanSource = StripAllowedEfPrototypeMarkers(source);
        var connectionScanSource = StripAllowedConnectionStringMarkers(source);

        Assert.Contains("CrmControlledRuntimeProbeActivationPlanStatusService", source);
        Assert.Contains("ControlledRuntimeProbeActivationPlan", source);
        Assert.Contains("Runtime probe activation plan only; no runtime activation approved", source);
        Assert.Contains("Sprint5P2SecretProviderRuntimeContractValidation", source);
        Assert.Contains("sprint5P1ControlledRuntimeProbeActivationPlan: 'Exists'", source);
        Assert.Contains("runtimeProbeActivationApproved: false", source);
        Assert.Contains("commonDbProbeActivationApproved: false", source);
        Assert.Contains("portalAuthProbeActivationApproved: false", source);
        Assert.Contains("productiveRoutesActivationApproved: false", source);
        Assert.Contains("realActivationApproved: false", source);
        Assert.Contains("nonProductionOnly: true", source);
        Assert.Contains("syntheticDataRequired: true", source);
        Assert.Contains("rollbackPlanRequired: true", source);
        Assert.Contains("observabilityRequired: true", source);
        Assert.Contains("secretProviderRequired: true", source);
        Assert.Contains("deleteStillNoGo: true", source);
        Assert.Contains("MapGet(\"/api/crm/foundation/sprint-5/runtime-probe-activation-plan\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/sprint-5/runtime-probe-activation-plan", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/sprint-5/runtime-probe-activation-plan", program);
        Assert.DoesNotContain("MapDelete", program);
        Assert.DoesNotContain("\"/api/crm/leads\"", program);
        Assert.DoesNotContain("\"/api/crm/accounts\"", program);
        Assert.DoesNotContain("\"/api/crm/contacts\"", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/contacts", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/contacts", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/contacts", program);
        Assert.DoesNotContain("Add" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authorization", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("AuthorizeAttribute", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalBaseUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalCorporativoUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbContext", dbContextScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbSet<", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MigrationBuilder", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ConnectionString", connectionScanSource, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void SecretProviderRuntimeContract_IsContractOnlyAndDoesNotReadSecretsOrActivateRuntime()
    {
        var source = ReadSourceFiles("src", "frontend", "docker-compose.yml", "docker-compose.crm.yml");
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));
        var dbContextScanSource = StripAllowedEfPrototypeMarkers(source);
        var connectionScanSource = StripAllowedConnectionStringMarkers(source);
        var secretProviderScanSource = StripAllowedSecretProviderContractMarkers(source);

        Assert.Contains("CrmSecretProviderRuntimeContractStatusService", source);
        Assert.Contains("SecretProviderRuntimeContractValidation", source);
        Assert.Contains("SecretProviderRuntimeContractPlaceholder", source);
        Assert.Contains("Secret Provider contract validation only; no secrets are read", source);
        Assert.Contains("Sprint5P3CommonDbProbeOptionalActivationInNonProduction", source);
        Assert.Contains("sprint5P2SecretProviderRuntimeContract: 'Exists'", source);
        Assert.Contains("secretProviderContractExists: true", source);
        Assert.Contains("secretProviderReadsEnabled: false", source);
        Assert.Contains("secretReadAttemptedByRuntime: false", source);
        Assert.Contains("realSecretsConfigured: false", source);
        Assert.Contains("envFileRequired: false", source);
        Assert.Contains("p2ConnectionStringsConfigured: false", source);
        Assert.Contains("keyVaultClientConfigured: false", source);
        Assert.Contains("secretValuesExposed: false", source);
        Assert.Contains("p2RuntimeProbeActivationApproved: false", source);
        Assert.Contains("p2CommonDbProbeActivationApproved: false", source);
        Assert.Contains("p2PortalAuthProbeActivationApproved: false", source);
        Assert.Contains("MapGet(\"/api/crm/foundation/sprint-5/secret-provider-runtime-contract\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/sprint-5/secret-provider-runtime-contract", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/sprint-5/secret-provider-runtime-contract", program);
        Assert.DoesNotContain("MapDelete", program);
        Assert.DoesNotContain("\"/api/crm/leads\"", program);
        Assert.DoesNotContain("\"/api/crm/accounts\"", program);
        Assert.DoesNotContain("\"/api/crm/contacts\"", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/contacts", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/contacts", program);
        Assert.DoesNotContain("Add" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authorization", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("AuthorizeAttribute", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Jwt" + "Bearer", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Cookie" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("local" + "Storage", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("session" + "Storage", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalBaseUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalCorporativoUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbContext", dbContextScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbSet<", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MigrationBuilder", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ConnectionString", connectionScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("KeyVault", secretProviderScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Azure.Security", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Environment.GetEnvironmentVariable", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("File.ReadAllText", source, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void CommonDbProbeOptionalActivation_IsDisabledAndDoesNotConnectDatabase()
    {
        var source = ReadSourceFiles("src", "frontend", "docker-compose.yml", "docker-compose.crm.yml");
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));
        var dbContextScanSource = StripAllowedEfPrototypeMarkers(source);
        var connectionScanSource = StripAllowedConnectionStringMarkers(source);
        var commonDbConnectionScanSource = StripAllowedCommonDbConnectionContractMarkers(source);

        Assert.Contains("CrmCommonDbProbeOptionalActivationStatusService", source);
        Assert.Contains("CommonDbProbeOptionalActivation", source);
        Assert.Contains("CommonDbProbeOptionalActivationPlaceholder", source);
        Assert.Contains("Common DB probe optional activation only; no database connection is attempted", source);
        Assert.Contains("Sprint5P4PortalAuthProbeOptionalActivationInNonProduction", source);
        Assert.Contains("sprint5P3CommonDbProbeOptionalActivation: 'Exists'", source);
        Assert.Contains("commonDbProbeOptionalActivationExists: true", source);
        Assert.Contains("p3CommonDbProbeActivationApproved: false", source);
        Assert.Contains("p3CommonDbProbeEnabled: false", source);
        Assert.Contains("p3CommonDbConnectionAttempted: false", source);
        Assert.Contains("p3SecretProviderRuntimeRequired: true", source);
        Assert.Contains("p3SecretProviderRuntimeConnected: false", source);
        Assert.Contains("secretReadsRequiredBeforeActivation: true", source);
        Assert.Contains("p3SecretReadsEnabled: false", source);
        Assert.Contains("p3RealDatabaseConfigured: false", source);
        Assert.Contains("p3ConnectionStringsConfigured: false", source);
        Assert.Contains("p3EfRuntimeEnabled: false", source);
        Assert.Contains("p3MigrationsCreated: false", source);
        Assert.Contains("p3ApiRequiresDatabase: false", source);
        Assert.Contains("MapGet(\"/api/crm/foundation/sprint-5/common-db-probe-optional-activation\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/sprint-5/common-db-probe-optional-activation", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/sprint-5/portal-auth-probe-optional-activation", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/sprint-5/common-db-probe-optional-activation", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/sprint-5/portal-auth-probe-optional-activation", program);
        Assert.DoesNotContain("MapDelete", program);
        Assert.DoesNotContain("\"/api/crm/leads\"", program);
        Assert.DoesNotContain("\"/api/crm/accounts\"", program);
        Assert.DoesNotContain("\"/api/crm/contacts\"", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/contacts", program);
        Assert.DoesNotContain("SqlConnection", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbConnection", commonDbConnectionScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbContext", dbContextScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbSet<", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MigrationBuilder", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ConnectionString", connectionScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Environment.GetEnvironmentVariable", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("File.ReadAllText", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Add" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authorization", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("AuthorizeAttribute", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Jwt" + "Bearer", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Cookie" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalBaseUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalCorporativoUrl", source, StringComparison.OrdinalIgnoreCase);
    }


    [Fact]
    public void PortalAuthProbeOptionalActivation_IsDisabledAndDoesNotReadTokens()
    {
        var repositoryRoot = FindRepositoryRoot();
        var serviceSource = File.ReadAllText(Path.Combine(repositoryRoot, "src", "CRM.Application", "Foundation", "CrmPortalAuthProbeOptionalActivationStatusService.cs"));
        var contractSource = File.ReadAllText(Path.Combine(repositoryRoot, "src", "CRM.Application", "Foundation", "CrmPortalAuthProbeOptionalActivationContracts.cs"));
        var applicationSource = serviceSource + Environment.NewLine + contractSource;
        var placeholderSource = File.ReadAllText(Path.Combine(repositoryRoot, "src", "CRM.Infrastructure", "Portal", "RuntimeProbe", "PortalAuthProbeOptionalActivationPlaceholder.cs"));

        Assert.Contains("PortalAuthProbeOptionalActivation", applicationSource);
        Assert.Contains("PortalAuthProbeEnabled", applicationSource);
        Assert.Contains("PortalHttpAttempted", applicationSource);
        Assert.Contains("TokenReadAttempted", applicationSource);
        Assert.Contains("HeaderReadAttempted", applicationSource);
        Assert.Contains("Portal Auth probe optional activation only; no tokens are read and no Portal HTTP calls are attempted", applicationSource);
        Assert.Contains("ContractOnly", placeholderSource);
        Assert.DoesNotContain("HttpClient", placeholderSource);
        Assert.DoesNotContain("GetEnvironmentVariable", placeholderSource);
    }

    [Fact]
    public void LockedProductiveRouteStubTrial_IsDocumentOnlyAndDoesNotRegisterProductiveRoutes()
    {
        var source = ReadSourceFiles("src", "frontend", "docker-compose.yml", "docker-compose.crm.yml");
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));
        var dbContextScanSource = StripAllowedEfPrototypeMarkers(source);
        var connectionScanSource = StripAllowedConnectionStringMarkers(source);

        Assert.Contains("CrmLockedProductiveRouteStubTrialStatusService", source);
        Assert.Contains("LockedProductiveRouteStubTrial", source);
        Assert.Contains("DocumentOnlyPreferredWithNoRuntimeRegistration", source);
        Assert.Contains("Locked productive route stub trial only; no productive routes are registered by default", source);
        Assert.Contains("Sprint5P6Sprint5GateDecision", source);
        Assert.Contains("MapGet(\"/api/crm/foundation/sprint-5/locked-productive-route-stub-trial\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/sprint-5/locked-productive-route-stub-trial", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/sprint-5/locked-productive-route-stub-trial", program);
        Assert.DoesNotContain("MapDelete", program);
        Assert.DoesNotContain("\"/api/crm/leads\"", program);
        Assert.DoesNotContain("\"/api/crm/accounts\"", program);
        Assert.DoesNotContain("\"/api/crm/contacts\"", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapGet(\"/api/crm/contacts", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/contacts", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/leads", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/accounts", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/contacts", program);
        Assert.DoesNotContain("DbContext", dbContextScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbSet<", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MigrationBuilder", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", StripAllowedProviderMarkers(source), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ConnectionString", connectionScanSource, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Add" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authentication", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Use" + "Authorization", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("AuthorizeAttribute", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalBaseUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalCorporativoUrl", source, StringComparison.OrdinalIgnoreCase);
    }

    private static IReadOnlySet<string> ReferencedAssemblyNames(Assembly assembly) =>
        assembly.GetReferencedAssemblies().Select(reference => reference.Name ?? "").ToHashSet(StringComparer.OrdinalIgnoreCase);

    private static string ReadSourceFiles(params string[] roots)
    {
        var repositoryRoot = FindRepositoryRoot();
        var contents = new List<string>();
        foreach (var root in roots)
        {
            var path = Path.Combine(repositoryRoot, root);
            if (File.Exists(path))
            {
                contents.Add(File.ReadAllText(path));
                continue;
            }

            if (!Directory.Exists(path))
            {
                continue;
            }

            foreach (var file in Directory.EnumerateFiles(path, "*.*", SearchOption.AllDirectories)
                         .Where(file => !file.Contains($"{Path.DirectorySeparatorChar}bin{Path.DirectorySeparatorChar}", StringComparison.OrdinalIgnoreCase))
                         .Where(file => !file.Contains($"{Path.DirectorySeparatorChar}obj{Path.DirectorySeparatorChar}", StringComparison.OrdinalIgnoreCase))
                         .Where(file => !file.Contains($"{Path.DirectorySeparatorChar}node_modules{Path.DirectorySeparatorChar}", StringComparison.OrdinalIgnoreCase))
                         .Where(file => !file.Contains($"{Path.DirectorySeparatorChar}dist{Path.DirectorySeparatorChar}", StringComparison.OrdinalIgnoreCase))
                         .Where(file => !file.Contains($"{Path.DirectorySeparatorChar}.angular{Path.DirectorySeparatorChar}", StringComparison.OrdinalIgnoreCase))
                         .Where(file => !file.Contains($"{Path.DirectorySeparatorChar}tools{Path.DirectorySeparatorChar}", StringComparison.OrdinalIgnoreCase)))
            {
                contents.Add(File.ReadAllText(file));
            }
        }

        return string.Join(Environment.NewLine, contents);
    }

    private static string StripAllowedEfPrototypeMarkers(string source) =>
        source.Replace("DbContextConfigured", string.Empty, StringComparison.Ordinal)
            .Replace("dbContextConfigured", string.Empty, StringComparison.Ordinal)
            .Replace("DbContext Configured", string.Empty, StringComparison.Ordinal)
            .Replace("DbContextRuntimeActive", string.Empty, StringComparison.Ordinal)
            .Replace("dbContextRuntimeActive", string.Empty, StringComparison.Ordinal)
            .Replace("DbContext Runtime Active", string.Empty, StringComparison.Ordinal)
            .Replace("CrmDbContextPrototypeContract", string.Empty, StringComparison.Ordinal)
            .Replace("CrmDbContextPrototype", string.Empty, StringComparison.Ordinal)
            .Replace("InheritsRealDbContext", string.Empty, StringComparison.Ordinal)
            .Replace("CRM_DBCONTEXT_RUNTIME_ACTIVE=false", string.Empty, StringComparison.Ordinal)
            .Replace("Sprint3P3EfDbContextPrototypeBehindDisabledFlag", string.Empty, StringComparison.Ordinal)
            .Replace("EfDbContextPrototypeDisabled", string.Empty, StringComparison.Ordinal)
            .Replace("EF/DbContext prototype only; runtime disabled and no database configured", string.Empty, StringComparison.Ordinal);

    private static string StripAllowedConnectionStringMarkers(string source) =>
        source.Replace("ConnectionStringsConfigured", string.Empty, StringComparison.Ordinal)
            .Replace("connectionStringsConfigured", string.Empty, StringComparison.Ordinal)
            .Replace("Connection Strings Configured", string.Empty, StringComparison.Ordinal)
            .Replace("CrmConnectionStringPolicyContract", string.Empty, StringComparison.Ordinal)
            .Replace("ConnectionStringPolicy", string.Empty, StringComparison.Ordinal)
            .Replace("connectionStringPolicy", string.Empty, StringComparison.Ordinal);

    private static string StripAllowedProviderMarkers(string source) =>
        source.Replace("UseSqlServerConfigured", string.Empty, StringComparison.Ordinal)
            .Replace("useSqlServerConfigured", string.Empty, StringComparison.Ordinal)
            .Replace("UseSqlServer Configured", string.Empty, StringComparison.Ordinal);

    private static string StripAllowedSecretProviderContractMarkers(string source) =>
        source.Replace("KeyVaultClientConfigured", string.Empty, StringComparison.Ordinal)
            .Replace("keyVaultClientConfigured", string.Empty, StringComparison.Ordinal)
            .Replace("Key Vault Client Configured", string.Empty, StringComparison.Ordinal);

    private static string StripAllowedCommonDbConnectionContractMarkers(string source) =>
        source.Replace("CommonDbConnectionStrategy", string.Empty, StringComparison.Ordinal)
            .Replace("CommonDbConnectionAttempted", string.Empty, StringComparison.Ordinal)
            .Replace("commonDbConnectionAttempted", string.Empty, StringComparison.Ordinal)
            .Replace("Common DB Connection Attempted", string.Empty, StringComparison.Ordinal)
            .Replace("DbConnectionAttempted", string.Empty, StringComparison.Ordinal)
            .Replace("dbConnectionAttempted", string.Empty, StringComparison.Ordinal)
            .Replace("DB Connection Attempted", string.Empty, StringComparison.Ordinal)
            .Replace("CommonDbConnectionContract", string.Empty, StringComparison.Ordinal)
            .Replace("Sprint3P2CommonDbConnectionContractAndSecretStrategy", string.Empty, StringComparison.Ordinal)
            .Replace("commonDbConnectionStringsConfigured", string.Empty, StringComparison.Ordinal)
            .Replace("CommonDbConnectionStringsConfigured", string.Empty, StringComparison.Ordinal)
            .Replace("CrmCommonDbConnectionStrategyStatusService", string.Empty, StringComparison.Ordinal)
            .Replace("GetCrmFoundationSprint3CommonDbConnectionStrategy", string.Empty, StringComparison.Ordinal)
            .Replace("/api/crm/foundation/sprint-3/common-db-connection-strategy", string.Empty, StringComparison.Ordinal);

    private static string FindRepositoryRoot()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);
        while (directory is not null && !File.Exists(Path.Combine(directory.FullName, "CRM.sln")))
        {
            directory = directory.Parent;
        }

        return directory?.FullName ?? throw new InvalidOperationException("Repository root was not found.");
    }
}
