using CRM.Application.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmLockedStubRuntimeRegistrationTrialStatusServiceTests
{
    [Fact]
    public void GetStatus_ReturnsDisabledTrialWithoutRuntimeRouteRegistration()
    {
        var service = new CrmLockedStubRuntimeRegistrationTrialStatusService();

        var status = service.GetStatus();

        Assert.Equal("CRM", status.Module);
        Assert.Equal("LockedStubRuntimeRegistrationTrial", status.Status);
        Assert.True(status.FoundationMode);
        Assert.True(status.LockedStubRuntimeRegistrationTrialExists);
        Assert.False(status.LockedStubRuntimeRegistrationApprovalGranted);
        Assert.False(status.LockedStubRuntimeRegistrationEnabled);
        Assert.False(status.LockedStubsRegisteredAtRuntime);
        Assert.False(status.ProductiveRoutesRegistered);
        Assert.False(status.ProductiveCrudEnabled);
        Assert.False(status.DeleteEndpointsEnabled);
        Assert.Equal(404, status.DefaultNegativeRouteStatus);
        Assert.Equal(423, status.FutureLockedResponseStatusIfExplicitlyEnabled);
        Assert.False(status.RuntimeFlagDefaultEnabled);
        Assert.False(status.UsesDomainServices);
        Assert.False(status.UsesFoundationStores);
        Assert.False(status.UsesDatabase);
        Assert.False(status.UsesPortalAuth);
        Assert.False(status.UsesTokenOrHeaderReads);
        Assert.True(status.NonProductionOnly);
        Assert.True(status.RollbackRequired);
        Assert.True(status.ObservabilityRequired);
        Assert.Equal("Sprint6P6Sprint6GateDecision", status.NextGate);
        Assert.Equal("Locked stub runtime registration trial only; no productive routes are registered by default", status.Warning);
        Assert.Equal("DocumentOnlyPreferredWithNoRuntimeRegistration", status.RuntimeRegistrationDecision);
    }
}
