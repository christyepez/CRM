namespace CRM.Application.Foundation;

public sealed record CrmE2EPilotScenarioContract(
    string Scenario,
    string Method,
    string Endpoint,
    string Expected,
    bool FoundationOnly);

public sealed record CrmE2EPilotEvidenceContract(
    string Evidence,
    string Command,
    bool Required);

public sealed record CrmE2EPilotSafetyGateContract(
    string Gate,
    string Decision,
    bool Approved,
    string RequiredBeforeProductiveUse);

public sealed record CrmNonProductionE2EPilotReadinessStatusResponse(
    string Module,
    string Status,
    bool FoundationMode,
    bool E2EPilotCanRun,
    string E2EPilotScope,
    bool ProductiveRoutesUsed,
    bool RealDatabaseUsed,
    bool PortalAuthRuntimeUsed,
    bool DurablePersistenceUsed,
    bool DeleteOperationsUsed,
    bool SyntheticDataOnly,
    bool FoundationEndpointsOnly,
    bool NegativeRouteValidationRequired,
    string NextGate,
    string Warning,
    IReadOnlyCollection<CrmE2EPilotScenarioContract> Scenarios,
    IReadOnlyCollection<CrmE2EPilotEvidenceContract> Evidence,
    IReadOnlyCollection<CrmE2EPilotSafetyGateContract> SafetyGates,
    IReadOnlyCollection<string> Risks);
