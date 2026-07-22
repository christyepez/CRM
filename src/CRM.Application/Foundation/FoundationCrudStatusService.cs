namespace CRM.Application.Foundation;

public sealed class FoundationCrudStatusService
{
    public FoundationCrudStatusResponse GetStatus() =>
        new(
            "CRM",
            "FoundationCrudEnabled",
            true,
            true,
            true,
            true,
            true,
            "NonProductionSeam",
            false,
            false,
            false,
            false,
            "FoundationSimulation",
            "Sprint2P5IntegrationReadinessReview",
            FoundationCrudSupport.WarningText);
}
