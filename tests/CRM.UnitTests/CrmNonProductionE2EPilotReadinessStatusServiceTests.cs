using CRM.Application.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmNonProductionE2EPilotReadinessStatusServiceTests
{
    [Fact]
    public void GetStatus_ReturnsFoundationOnlyPilotReadiness()
    {
        var status = new CrmNonProductionE2EPilotReadinessStatusService().GetStatus();

        Assert.Equal("NonProductionE2EPilotReadiness", status.Status);
        Assert.True(status.FoundationMode);
        Assert.True(status.E2EPilotCanRun);
        Assert.Equal("FoundationOnly", status.E2EPilotScope);
        Assert.False(status.ProductiveRoutesUsed);
        Assert.False(status.RealDatabaseUsed);
        Assert.False(status.PortalAuthRuntimeUsed);
        Assert.False(status.DurablePersistenceUsed);
        Assert.False(status.DeleteOperationsUsed);
        Assert.True(status.SyntheticDataOnly);
        Assert.True(status.FoundationEndpointsOnly);
        Assert.True(status.NegativeRouteValidationRequired);
        Assert.Equal(CrmNonProductionE2EPilotReadinessStatusService.NextGate, status.NextGate);
        Assert.Equal(CrmNonProductionE2EPilotReadinessStatusService.WarningText, status.Warning);
        Assert.Contains(status.Scenarios, scenario => scenario.Endpoint == "/api/crm/foundation/sprint-4/nonproduction-e2e-pilot-readiness" && scenario.FoundationOnly);
        Assert.Contains(status.Scenarios, scenario => scenario.Endpoint == "/api/crm/leads" && scenario.Expected == "NotActive");
        Assert.Contains(status.Evidence, evidence => evidence.Command.Contains("check-crm-e2e-foundation.ps1", StringComparison.Ordinal));
        Assert.Contains(status.SafetyGates, gate => gate.Gate == "Sprint 4 P6 decision" && !gate.Approved);
    }
}
