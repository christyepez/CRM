using CRM.Application.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmControlledRuntimeActivationDecisionStatusServiceTests
{
    [Fact]
    public void GetStatus_ReturnsSprint9P1ControlledDecision()
    {
        var status = new CrmControlledRuntimeActivationDecisionStatusService().GetStatus();

        Assert.Equal("ControlledRuntimeActivationDecision", status.Status);
        Assert.True(status.FoundationMode);
        Assert.True(status.ControlledRuntimeActivationDecisionExists);
        Assert.Equal("ApprovedForNonProductionTrialsOnly", status.ControlledRuntimeActivationDecision);
        Assert.Equal("NoGo", status.ProductionActivationDecision);
        Assert.True(status.SecretProviderRuntimeEnablementTrialApproved);
        Assert.True(status.CommonDbRuntimeConnectivityTrialApproved);
        Assert.True(status.PortalAuthRuntimeValidationTrialApproved);
        Assert.True(status.ProductiveRouteDryRunTrialApproved);
        Assert.False(status.RuntimeTrialsEnabledNow);
        Assert.False(status.ProductionRuntimeEnabledNow);
        Assert.False(status.SecretProviderRuntimeEnabledNow);
        Assert.False(status.CommonDbRuntimeEnabledNow);
        Assert.False(status.PortalAuthRuntimeEnabledNow);
        Assert.False(status.ProductiveRoutesEnabledNow);
        Assert.False(status.ProductiveCrudEnabledNow);
        Assert.False(status.DeleteEnabledNow);
        Assert.False(status.ProductiveUiEnabledNow);
        Assert.True(status.DefaultFailClosedRequired);
        Assert.True(status.ExplicitNonProductionFlagsRequired);
        Assert.True(status.RollbackRequired);
        Assert.True(status.ObservabilityRequired);
        Assert.True(status.SecurityApprovalRequiredForEachTrial);
        Assert.True(status.ArchitectureApprovalRequiredForEachTrial);
        Assert.True(status.DevOpsApprovalRequiredForEachTrial);
        Assert.True(status.QaApprovalRequiredForEachTrial);
        Assert.Equal(CrmControlledRuntimeActivationDecisionStatusService.NextGate, status.NextGate);
        Assert.Equal(CrmControlledRuntimeActivationDecisionStatusService.WarningText, status.Warning);
    }

    [Fact]
    public void GetSprint9Roadmap_ReturnsP2ToP6Gates()
    {
        var roadmap = new CrmControlledRuntimeActivationDecisionStatusService().GetSprint9Roadmap();

        Assert.Contains(roadmap, item => item.Gate == "Sprint9P2SecretProviderRuntimeEnablementTrial");
        Assert.Contains(roadmap, item => item.Gate == "Sprint9P3CommonDbRuntimeConnectivityTrial");
        Assert.Contains(roadmap, item => item.Gate == "Sprint9P4PortalAuthRuntimeValidationTrial");
        Assert.Contains(roadmap, item => item.Gate == "Sprint9P5ProductiveRouteDryRunTrial");
        Assert.Contains(roadmap, item => item.Gate == "Sprint9P6Sprint9ClosureGate");
    }
}
