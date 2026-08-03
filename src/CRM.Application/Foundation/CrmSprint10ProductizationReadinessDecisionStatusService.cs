namespace CRM.Application.Foundation;

public sealed class CrmSprint10ProductizationReadinessDecisionStatusService
{
    public const string StatusName = "Sprint10P1ProductizationReadinessDecision";
    public const string WarningText = "Sprint 10 P1 Productization Readiness Decision: Exists; no runtime activation is approved";
    public const string NextGate = "Sprint10P2CommonDbControlledActivationPlan";

    public CrmSprint10ProductizationReadinessDecisionStatusResponse GetStatus() =>
        new(
            Module: "CRM",
            Status: StatusName,
            FoundationMode: true,
            Sprint10P1ProductizationReadinessDecisionExists: true,
            Sprint10P1Approved: true,
            Sprint9GateReviewed: true,
            Sprint9ProductionNoGoPreserved: true,
            Sprint10P1Decision: "GoForControlledNonProductionProductizationPreparation",
            ProductionActivationDecision: "NoGo",
            ProductiveRuntimeActivationDecision: "NoGoForProduction",
            CommonDbControlledActivationDecision: "GoOnlyAsExplicitNonProductionPreparation",
            PortalAuthControlledActivationDecision: "GoOnlyAsExplicitNonProductionPreparation",
            ProductiveRouteControlledActivationDecision: "GoOnlyAsExplicitNonProductionPreparation",
            ProductiveCrudPilotDecision: "NoGoUntilP5",
            ProductiveUiDecision: "NoGo",
            ProductionActivationApproved: false,
            ProductiveRuntimeActivationApprovedForProduction: false,
            CommonDbControlledPreparationApproved: true,
            PortalAuthControlledPreparationApproved: true,
            ProductiveRouteControlledPreparationApproved: true,
            ProductiveCrudPilotApproved: false,
            ProductiveUiApproved: false,
            NonProductionOnly: true,
            ExplicitFlagsRequired: true,
            FailClosedByDefault: true,
            ObservabilityMetadataOnly: true,
            RollbackAvailable: true,
            ProductizationStatus: "PreparationOnly",
            NextGate: NextGate,
            Warning: WarningText,
            Decisions: GetDecisions(),
            Evidence: GetEvidence(),
            Risks: GetRisks(),
            BlockedItems:
            [
                "Production activation",
                "Productive runtime activation for production",
                "Productive CRUD pilot before Sprint 10 P5",
                "DELETE endpoints",
                "Database writes, EF runtime, migrations and schema changes",
                "Portal Auth enforcement, header reads, token reads, login/logout and CRM Identity",
                "Productive UI"
            ]);

    public IReadOnlyCollection<CrmSprint10ProductizationDecisionContract> GetDecisions() =>
    [
        new("Sprint 10 P1", "GoForControlledNonProductionProductizationPreparation", "Sprint 9 evidence is complete, but productization remains preparation-only."),
        new("Production Activation", "NoGo", "No production runtime path is approved."),
        new("Productive Runtime Activation", "NoGoForProduction", "Runtime activation can only be prepared for explicit NonProduction gates."),
        new("Common DB Controlled Activation", "GoOnlyAsExplicitNonProductionPreparation", "Sprint 10 may prepare a controlled common DB activation plan without opening DB runtime now."),
        new("Portal Auth Controlled Activation", "GoOnlyAsExplicitNonProductionPreparation", "Sprint 10 may prepare controlled Portal Auth validation without token/header reads now."),
        new("Productive Route Controlled Activation", "GoOnlyAsExplicitNonProductionPreparation", "Sprint 10 may prepare route activation criteria while productive routes remain 404 by default."),
        new("Productive CRUD Pilot", "NoGoUntilP5", "CRUD pilot remains blocked until explicit later Sprint 10 approval."),
        new("Productive UI", "NoGo", "No productive UI is approved.")
    ];

    public IReadOnlyCollection<CrmSprint10ProductizationEvidenceContract> GetEvidence() =>
    [
        new("Sprint 9 P1", "Controlled runtime activation decision approved trials only for NonProduction and preserved production NoGo.", "Passed"),
        new("Sprint 9 P2", "Secret Provider runtime trial remains disabled/fail-closed and sanitized.", "Passed"),
        new("Sprint 9 P3", "Common DB runtime connectivity trial remains disabled/fail-closed and does not expose connection strings.", "Passed"),
        new("Sprint 9 P4", "Portal Auth runtime validation trial remains disabled/fail-closed and does not read headers or tokens.", "Passed"),
        new("Sprint 9 P5", "Productive route dry-run keeps productive routes 404 by default and probe 423 by default.", "Passed"),
        new("Sprint 9 P6", "Sprint 9 gate closed with production activation NoGo and Sprint 10 planning Go.", "Passed")
    ];

    public IReadOnlyCollection<CrmSprint10ProductizationReadinessRiskContract> GetRisks() =>
    [
        new("Decision drift", "Preparation language could be misread as production approval.", "Keep ProductionActivationDecision=NoGo and ProductiveRuntimeActivationDecision=NoGoForProduction in API, docs and checks."),
        new("Runtime leakage", "Future implementation could accidentally enable DB, Auth or productive routes by default.", "Require explicit NonProduction flags, fail-closed defaults and guardrail checks."),
        new("Side effects", "A readiness endpoint could become a probe or mutation path.", "Sprint 10 P1 endpoint is GET-only and returns static foundation status.")
    ];
}
