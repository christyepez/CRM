using CRM.Application.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmNonProductionRuntimeApprovalPackageStatusServiceTests
{
    [Fact]
    public void GetStatus_ReturnsApprovalPackageWithAllRuntimeApprovalsFalse()
    {
        var service = new CrmNonProductionRuntimeApprovalPackageStatusService();

        var status = service.GetStatus();

        Assert.Equal("CRM", status.Module);
        Assert.Equal("NonProductionRuntimeApprovalPackage", status.Status);
        Assert.True(status.FoundationMode);
        Assert.True(status.NonProductionRuntimeApprovalPackageExists);
        Assert.False(status.NonProductionRuntimeApprovalGranted);
        Assert.False(status.SecretProviderMockApprovalGranted);
        Assert.False(status.CommonDbDryRunApprovalGranted);
        Assert.False(status.PortalAuthDryRunApprovalGranted);
        Assert.False(status.LockedStubRuntimeTrialApprovalGranted);
        Assert.False(status.RealActivationApprovalGranted);
        Assert.False(status.ProductiveRoutesApprovalGranted);
        Assert.False(status.DeleteApprovalGranted);
        Assert.True(status.SyntheticDataApprovalRequired);
        Assert.True(status.RollbackApprovalRequired);
        Assert.True(status.ObservabilityApprovalRequired);
        Assert.True(status.SecurityReviewRequired);
        Assert.True(status.ArchitectureReviewRequired);
        Assert.Equal("Sprint6P2SecretProviderSafeMockActivation", status.NextGate);
        Assert.Equal("NonProduction runtime approval package only; no runtime approval is granted", status.Warning);
        Assert.NotEmpty(status.Capabilities);
        Assert.All(status.Capabilities, capability => Assert.False(capability.ApprovalGranted));
        Assert.NotEmpty(status.BlockedItems);
    }
}
