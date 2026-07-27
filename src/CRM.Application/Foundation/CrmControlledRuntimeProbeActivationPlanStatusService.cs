namespace CRM.Application.Foundation;

public sealed class CrmControlledRuntimeProbeActivationPlanStatusService
{
    public const string WarningText = "Runtime probe activation plan only; no runtime activation approved";
    public const string NextGate = "Sprint5P2SecretProviderRuntimeContractValidation";

    public CrmControlledRuntimeProbeActivationPlanStatusResponse GetStatus() =>
        new(
            "CRM",
            "ControlledRuntimeProbeActivationPlan",
            true,
            true,
            false,
            false,
            false,
            false,
            false,
            true,
            true,
            true,
            true,
            true,
            true,
            NextGate,
            WarningText,
            GetActivationGates(),
            GetApprovalRequirements(),
            GetRollbackRequirements(),
            GetObservabilityRequirements(),
            [
                "Runtime probe activation plan could be confused with runtime approval if approval flags are ignored.",
                "Secret provider validation must happen before any optional probe activation.",
                "Negative route checks must continue proving productive CRM routes are inactive."
            ],
            [
                "Common DB probe runtime activation.",
                "Portal Auth probe runtime activation.",
                "Productive route locked stub runtime registration.",
                "Real activation, DELETE endpoints and productive UI."
            ]);

    public IReadOnlyCollection<CrmRuntimeProbeActivationGateContract> GetActivationGates() =>
    [
        new("Common DB probe", "Sprint5P3CommonDbProbeOptionalActivation", false, "Secret provider validated, synthetic data approved, rollback and observability ready."),
        new("Portal Auth probe", "Sprint5P4PortalAuthProbeOptionalActivation", false, "Portal contract approved, no token persistence, rollback and observability ready."),
        new("Productive route locked stubs", "Sprint5P5LockedProductiveRouteStubTrial", false, "Stubs locked, no business execution, no DELETE and negative checks passing.")
    ];

    public IReadOnlyCollection<CrmRuntimeProbeApprovalRequirementContract> GetApprovalRequirements() =>
    [
        new("Formal release approval", "Release Manager", true, false),
        new("Architecture gate approval", "Architecture Governance", true, false),
        new("Security and secret handling approval", "Security", true, false),
        new("Synthetic data approval", "Data Architect", true, false),
        new("Portal boundary approval", "Portal Integration", true, false)
    ];

    public IReadOnlyCollection<CrmRuntimeProbeRollbackRequirementContract> GetRollbackRequirements() =>
    [
        new("Disable probe flag", "Any unexpected DB, Portal or route runtime attempt", true),
        new("Return to foundation-only endpoints", "Health/readiness regression", true),
        new("Preserve negative route checks", "Any productive route returns success", true)
    ];

    public IReadOnlyCollection<CrmRuntimeProbeObservabilityRequirementContract> GetObservabilityRequirements() =>
    [
        new("Health endpoints", "/health, /health/live and /health/ready remain green", true),
        new("Structured logs without secrets", "No token, password, connection string or personal data in logs", true),
        new("Negative route evidence", "/api/crm/leads, /api/crm/accounts and /api/crm/contacts remain inactive", true)
    ];
}
