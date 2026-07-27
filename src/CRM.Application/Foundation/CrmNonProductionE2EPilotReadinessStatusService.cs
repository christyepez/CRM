namespace CRM.Application.Foundation;

public sealed class CrmNonProductionE2EPilotReadinessStatusService
{
    public const string WarningText = "Non-production E2E pilot readiness only; no real activation";
    public const string NextGate = "Sprint4P6Sprint4GateDecision";

    public CrmNonProductionE2EPilotReadinessStatusResponse GetStatus() =>
        new(
            "CRM",
            "NonProductionE2EPilotReadiness",
            true,
            true,
            "FoundationOnly",
            false,
            false,
            false,
            false,
            false,
            true,
            true,
            true,
            NextGate,
            WarningText,
            GetScenarios(),
            GetEvidence(),
            GetSafetyGates(),
            [
                "E2E pilot readiness can be mistaken for productive readiness if negative route checks are skipped.",
                "Foundation endpoints must remain separate from productive route activation.",
                "P6 must make the Sprint 4 GO/NO-GO decision before any real activation."
            ]);

    public IReadOnlyCollection<CrmE2EPilotScenarioContract> GetScenarios() =>
    [
        new("Health", "GET", "/health", "200", true),
        new("Liveness", "GET", "/health/live", "200", true),
        new("Readiness", "GET", "/health/ready", "200", true),
        new("CRM readiness", "GET", "/api/crm/readiness", "200", true),
        new("Sprint 3 productization review", "GET", "/api/crm/foundation/sprint-3/productization-review", "200", true),
        new("Sprint 4 runtime readiness", "GET", "/api/crm/foundation/sprint-4/runtime-readiness", "200", true),
        new("Sprint 4 common DB runtime probe", "GET", "/api/crm/foundation/sprint-4/common-db-runtime-probe", "200", true),
        new("Sprint 4 Portal Auth runtime probe", "GET", "/api/crm/foundation/sprint-4/portal-auth-runtime-probe", "200", true),
        new("Sprint 4 productive routes locked stub", "GET", "/api/crm/foundation/sprint-4/productive-routes-locked-stub", "200", true),
        new("Sprint 4 non-production E2E pilot readiness", "GET", "/api/crm/foundation/sprint-4/nonproduction-e2e-pilot-readiness", "200", true),
        new("Productive leads route negative check", "GET", "/api/crm/leads", "NotActive", false),
        new("Productive accounts route negative check", "GET", "/api/crm/accounts", "NotActive", false),
        new("Productive contacts route negative check", "GET", "/api/crm/contacts", "NotActive", false)
    ];

    public IReadOnlyCollection<CrmE2EPilotEvidenceContract> GetEvidence() =>
    [
        new("Docker service status", "docker compose ps", true),
        new("Health endpoint results", "powershell.exe -ExecutionPolicy Bypass -File tools\\check-crm-health.ps1", true),
        new("Foundation guardrails", "powershell.exe -ExecutionPolicy Bypass -File tools\\check-crm-guardrails.ps1", true),
        new("Foundation verifier", "powershell.exe -ExecutionPolicy Bypass -File tools\\verify-crm-foundation.ps1", true),
        new("E2E foundation pilot check", "powershell.exe -ExecutionPolicy Bypass -File tools\\check-crm-e2e-foundation.ps1", true)
    ];

    public IReadOnlyCollection<CrmE2EPilotSafetyGateContract> GetSafetyGates() =>
    [
        new("Productive routes remain inactive", "GoForPilot", true, "Negative route checks must pass."),
        new("Synthetic data only", "GoForPilot", true, "Do not use real customer, account or contact data."),
        new("No real DB/Auth/Portal runtime", "GoForPilot", true, "Keep probes disabled and foundation-only."),
        new("Sprint 4 P6 decision", "Pending", false, "Formal GO/NO-GO still required before real activation.")
    ];
}
