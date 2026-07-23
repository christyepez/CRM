using CRM.Application.Portal;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmPortalAuthRuntimeContractStatusServiceTests
{
    [Fact]
    public void GetStatus_ReturnsContractOnlyPortalAuthRuntimeValidation()
    {
        var status = new CrmPortalAuthRuntimeContractStatusService().GetStatus();

        Assert.Equal("PortalAuthRuntimeContractValidation", status.Status);
        Assert.True(status.FoundationMode);
        Assert.False(status.PortalRuntimeConnected);
        Assert.False(status.AuthRuntimeEnabled);
        Assert.False(status.CrmOwnsAuth);
        Assert.Equal("PortalCorporativo", status.AuthOwnedBy);
        Assert.False(status.CredentialStorageEnabled);
        Assert.False(status.LoginImplementedByCrm);
        Assert.False(status.IdentityImplementedByCrm);
        Assert.False(status.PermissionsPersistedInCrm);
        Assert.True(status.FoundationSimulationActive);
        Assert.False(status.ProductiveAuthorizationEnabled);
        Assert.Equal("Sprint3P5ProductiveApiRouteDraftBehindDisabledFlag", status.NextGate);
        Assert.Equal(CrmPortalAuthRuntimeContractStatusService.WarningText, status.Warning);
    }

    [Fact]
    public void GetStatus_ListsFoundationAndFutureCapabilitiesOwnedByPortal()
    {
        var status = new CrmPortalAuthRuntimeContractStatusService().GetStatus();

        Assert.Contains(status.Capabilities, capability => capability.Capability == "crm.foundation.read");
        Assert.Contains(status.Capabilities, capability => capability.Capability == "crm.foundation.preview.write");
        Assert.Contains(status.Capabilities, capability => capability.Capability == "crm.foundation.preview.clear");
        Assert.Contains(status.Capabilities, capability => capability.Capability == "crm.readiness.read");
        Assert.Contains(status.Capabilities, capability => capability.Capability == "crm.leads.read");
        Assert.All(status.Capabilities, capability =>
        {
            Assert.False(capability.PersistedInCrm);
            Assert.Equal("PortalCorporativo", capability.Owner);
        });
    }
}
