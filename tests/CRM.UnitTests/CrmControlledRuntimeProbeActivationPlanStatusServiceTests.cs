using CRM.Application.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmControlledRuntimeProbeActivationPlanStatusServiceTests
{
    [Fact]
    public void GetStatus_ReturnsControlledRuntimeProbeActivationPlanWithoutActivatingRuntime()
    {
        var status = new CrmControlledRuntimeProbeActivationPlanStatusService().GetStatus();

        Assert.Equal("ControlledRuntimeProbeActivationPlan", status.Status);
        Assert.True(status.FoundationMode);
        Assert.True(status.RuntimeProbeActivationPlanExists);
        Assert.False(status.RuntimeProbeActivationApproved);
        Assert.False(status.CommonDbProbeActivationApproved);
        Assert.False(status.PortalAuthProbeActivationApproved);
        Assert.False(status.ProductiveRoutesActivationApproved);
        Assert.False(status.RealActivationApproved);
        Assert.True(status.NonProductionOnly);
        Assert.True(status.SyntheticDataRequired);
        Assert.True(status.RollbackPlanRequired);
        Assert.True(status.ObservabilityRequired);
        Assert.True(status.SecretProviderRequired);
        Assert.True(status.DeleteStillNoGo);
        Assert.Equal(CrmControlledRuntimeProbeActivationPlanStatusService.NextGate, status.NextGate);
        Assert.Equal(CrmControlledRuntimeProbeActivationPlanStatusService.WarningText, status.Warning);
        Assert.All(status.ActivationGates, gate => Assert.False(gate.Approved));
        Assert.All(status.ApprovalRequirements, requirement =>
        {
            Assert.True(requirement.Required);
            Assert.False(requirement.Satisfied);
        });
        Assert.Contains("Common DB probe runtime activation.", status.BlockedItems);
    }
}
