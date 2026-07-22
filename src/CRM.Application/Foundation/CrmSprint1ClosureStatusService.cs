namespace CRM.Application.Foundation;

public sealed record CrmSprint1ClosureStatusResponse(
    string Module,
    string Sprint,
    string Status,
    string RuntimeMode,
    string Persistence,
    string PortalIntegration,
    string FinancialIntegration,
    string ReportingIntegration,
    string ProductizationStatus,
    string NextGate,
    string Warning);

public sealed class CrmSprint1ClosureStatusService
{
    public const string WarningText = "Foundation closure only; no productive activation";

    public CrmSprint1ClosureStatusResponse GetStatus() =>
        new(
            "CRM",
            "Sprint 1",
            "FoundationClosed",
            "NonProduction",
            "None",
            "Planned",
            "Planned",
            "Planned",
            "NotReady",
            "Sprint2Planning",
            WarningText);
}
