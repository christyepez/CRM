namespace CRM.Application.Foundation;

public sealed class CrmRuntimeEnvironmentReadinessStatusService
{
    public const string WarningText = "Runtime readiness only; no real activation";
    public const string NextGate = "Sprint4P2ControlledCommonDbRuntimeProbeBehindDisabledFlag";

    public CrmRuntimeEnvironmentReadinessStatusResponse GetStatus() =>
        new(
            "CRM",
            "RuntimeEnvironmentReadiness",
            true,
            true,
            8093,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            "NotReady",
            NextGate,
            WarningText,
            GetToolingChecks(),
            GetHealthChecks(),
            GetBlockedItems());

    public IReadOnlyCollection<CrmRuntimeToolingCheckContract> GetToolingChecks() =>
    [
        new("Docker Desktop", "Available locally", "Expected", "Run docker version and docker compose config before runtime probes."),
        new("Docker Compose", "crm-api on 8093", "Expected", "Compose must not define SQL Server or secrets."),
        new("Node PATH", "Optional for verifier", "WarnOnly", "If node is not on PATH, use the bundled Node executable documented in the runbook."),
        new("PowerShell", "Windows local scripts", "Expected", "Run scripts from the CRM root with ExecutionPolicy Bypass if needed."),
        new("GitHub main", "Source of truth", "Required", "Start each sprint branch from origin/main.")
    ];

    public IReadOnlyCollection<CrmRuntimeHealthCheckContract> GetHealthChecks() =>
    [
        new("/health", "200 Healthy", "ExpectedWhenApiIsRunning", true),
        new("/health/live", "200 Healthy", "ExpectedWhenApiIsRunning", true),
        new("/health/ready", "200 Healthy", "ExpectedWhenApiIsRunning", true),
        new("/api/crm/readiness", "200 ReadyForFoundationOnly", "ExpectedWhenApiIsRunning", true),
        new("/api/crm/foundation/sprint-3/productization-review", "200 NoGoForRealActivation", "ExpectedWhenApiIsRunning", true),
        new("/api/crm/foundation/sprint-4/runtime-readiness", "200 RuntimeEnvironmentReadiness", "ExpectedWhenApiIsRunning", true)
    ];

    public IReadOnlyCollection<CrmRuntimeBlockedItemContract> GetBlockedItems() =>
    [
        new("Real database", "Blocked", "Sprint 4 P1 is local readiness only."),
        new("EF runtime", "Blocked", "Runtime probe belongs to a later gated package."),
        new("Portal Auth runtime", "Blocked", "Portal runtime probe belongs to Sprint 4 P3."),
        new("Productive API routes", "Blocked", "Routes remain inactive until Auth and persistence gates pass."),
        new("DELETE endpoints", "Blocked", "Audit, retention and recovery policy are not approved."),
        new("Productive CRM UI", "Blocked", "Only readiness labels are allowed.")
    ];
}
