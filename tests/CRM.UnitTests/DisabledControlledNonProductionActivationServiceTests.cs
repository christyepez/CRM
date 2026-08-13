using CRM.Application.Foundation;
using CRM.Infrastructure.Portal.ControlledRuntimePilot;
using Xunit;

namespace CRM.UnitTests;

public sealed class DisabledControlledNonProductionActivationServiceTests
{
    [Fact]
    public void GetDryRunResult_RemainsNoOpAndFailClosed()
    {
        var service = new DisabledControlledNonProductionActivationService(
            ControlledNonProductionActivationOptions.Disabled(),
            ControlledNonProductionActivationFeatureFlags.Disabled());

        var result = service.GetDryRunResult(new CrmControlledRuntimePilotFirstSliceNonProductionActivationControlledDryRunRequest(
            ApprovalReference: "logical-approval-reference-placeholder",
            RequestedBy: "logical-requester-placeholder"));

        Assert.True(result.DryRunOnly);
        Assert.True(result.ControlledImplementationPrepared);
        Assert.False(result.ControlledImplementationExecuted);
        Assert.False(result.ActivationAttempted);
        Assert.False(result.ActivationExecuted);
        Assert.False(result.ExternalCallAttempted);
        Assert.False(result.PortalCouplingEnabled);
        Assert.False(result.FeatureFlagsEnabled);
        Assert.True(result.ApprovalReferenceAccepted);
        Assert.Equal("Locked", result.Status);
    }

    [Fact]
    public void DisabledFeatureFlags_ReportNoEnabledFlags()
    {
        var flags = ControlledNonProductionActivationFeatureFlags.Disabled();

        Assert.False(flags.FirstSliceEnabled);
        Assert.False(flags.PortalClientEnabled);
        Assert.False(flags.DryRunEnabled);
        Assert.False(flags.GatewayRoutesEnabled);
        Assert.False(flags.PortalNavigationEnabled);
        Assert.False(flags.AnyEnabled);
    }
}
