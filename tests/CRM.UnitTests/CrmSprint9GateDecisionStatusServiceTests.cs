using CRM.Application.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmSprint9GateDecisionStatusServiceTests
{
    [Fact]
    public void GetStatus_ReturnsClosedSprint9GateDecision()
    {
        var status = new CrmSprint9GateDecisionStatusService().GetStatus();

        Assert.Equal("Sprint9GateDecision", status.Status);
        Assert.True(status.FoundationMode);
        Assert.True(status.Sprint9GateDecisionExists);
        Assert.True(status.Sprint9GateDecisionApproved);
        Assert.True(status.Sprint9Closed);
        Assert.True(status.Sprint9EvidenceComplete);
        Assert.True(status.Sprint9P1Complete);
        Assert.True(status.Sprint9P2Complete);
        Assert.True(status.Sprint9P3Complete);
        Assert.True(status.Sprint9P4Complete);
        Assert.True(status.Sprint9P5Complete);
        Assert.Equal("GoForSprint10ControlledProductizationReadinessPlanning", status.OverallSprint9Decision);
        Assert.Equal("NoGo", status.ProductionActivationDecision);
        Assert.Equal("GoOnlyAsExplicitNonProductionTrial", status.SecretProviderRuntimeTrialDecision);
        Assert.Equal("GoOnlyAsExplicitNonProductionTrial", status.CommonDbRuntimeConnectivityTrialDecision);
        Assert.Equal("GoOnlyAsExplicitNonProductionTrial", status.PortalAuthRuntimeValidationTrialDecision);
        Assert.Equal("GoOnlyAsExplicitNonProductionDryRun", status.ProductiveRouteDryRunTrialDecision);
        Assert.Equal("NoGoByDefault", status.ProductiveRouteRegistrationDecision);
        Assert.Equal("NoGo", status.ProductiveCrudDecision);
        Assert.Equal("NoGo", status.DeleteDecision);
        Assert.Equal("NoGoForProduction", status.DbRuntimeDecision);
        Assert.Equal("NoGoForProduction", status.PortalAuthEnforcementDecision);
        Assert.False(status.ProductionActivationApproved);
        Assert.False(status.RuntimeActivationApprovedForProduction);
        Assert.False(status.ProductiveRoutesApprovedByDefault);
        Assert.False(status.ProductiveCrudApproved);
        Assert.False(status.DeleteApproved);
        Assert.False(status.DatabaseWritesApproved);
        Assert.False(status.EfRuntimeApproved);
        Assert.False(status.MigrationsApproved);
        Assert.False(status.SchemaChangesApproved);
        Assert.False(status.PortalAuthEnforcementApproved);
        Assert.False(status.TokenHeaderReadsApproved);
        Assert.False(status.LoginLogoutApproved);
        Assert.False(status.IdentityRuntimeApproved);
        Assert.False(status.ProductiveUiApproved);
        Assert.True(status.NonProductionTrialsRemainAllowedOnlyWithExplicitFlags);
        Assert.True(status.AllTrialsFailClosedByDefault);
        Assert.True(status.AllObservabilityMetadataOnly);
        Assert.True(status.RollbackAvailable);
        Assert.Equal("NotReady", status.ProductizationStatus);
        Assert.Equal(CrmSprint9GateDecisionStatusService.NextGate, status.NextGate);
        Assert.Equal(CrmSprint9GateDecisionStatusService.WarningText, status.Warning);
    }

    [Fact]
    public void GetSprint10Roadmap_ReturnsNextGate()
    {
        var roadmap = new CrmSprint9GateDecisionStatusService().GetSprint10Roadmap();

        Assert.Collection(
            roadmap,
            item => Assert.Equal("Sprint 10 P1", item.Package),
            item => Assert.Equal("Sprint 10 P2", item.Package),
            item => Assert.Equal("Sprint 10 P3", item.Package));
    }
}
