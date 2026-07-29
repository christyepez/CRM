using CRM.Infrastructure.Persistence.RuntimeProbe;
using Xunit;

namespace CRM.UnitTests;

public sealed class CommonDbConnectivityProbeTests
{
    [Fact]
    public async Task DisabledProbe_DoesNotAttemptConnectionOrReturnConnectionString()
    {
        var result = await new DisabledCommonDbConnectivityProbe()
            .ProbeAsync(new CommonDbConnectivityProbeRequest("crm-common-db-connection"));

        Assert.False(result.ProbeAttempted);
        Assert.False(result.ProviderConfigured);
        Assert.True(result.SecretProviderAvailabilityMetadataUsed);
        Assert.False(result.ConnectionAttempted);
        Assert.False(result.Connected);
        Assert.True(result.TimeoutApplied);
        Assert.False(result.ConnectionStringReturned);
        Assert.False(result.ConnectionStringLogged);
        Assert.False(result.ConnectionStringPersisted);
        Assert.False(result.ConnectionStringCached);
        Assert.Equal("Locked", result.Status);
    }
}
