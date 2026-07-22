using CRM.Application.Portal;
using CRM.Infrastructure.Portal.Runtime;
using Xunit;

namespace CRM.UnitTests;

public sealed class PortalAuthRuntimeAdapterPlaceholderTests
{
    [Fact]
    public void Placeholder_IsContractOnlyAndDisconnected()
    {
        var adapter = new PortalAuthRuntimeAdapterPlaceholder();
        var status = adapter.GetStatus();

        Assert.False(adapter.Configured);
        Assert.False(adapter.RuntimeConnected);
        Assert.Equal(CrmPortalAuthRuntimeContractStatusService.WarningText, PortalAuthRuntimeAdapterPlaceholder.Warning);
        Assert.False(status.PortalRuntimeConnected);
        Assert.False(status.AuthRuntimeEnabled);
        Assert.False(status.ProductiveAuthorizationEnabled);
    }
}
