using CRM.Infrastructure.Security.Secrets;
using Xunit;

namespace CRM.UnitTests;

public sealed class SecretProviderRealNonProductionRuntimeProbeTests
{
    [Fact]
    public void GetStatus_ReturnsContractOnlySkippedProbe()
    {
        var status = new SecretProviderRealNonProductionRuntimeProbe().GetStatus();

        Assert.True(status.Exists);
        Assert.False(status.ApprovalGranted);
        Assert.False(status.ProbeEnabled);
        Assert.False(status.ProbeAttempted);
        Assert.False(status.RuntimeConnected);
        Assert.False(status.RealSecretReadAttempted);
        Assert.True(status.LogicalSecretNamesValidated);
        Assert.True(status.ProbeSkippedBecauseApprovalNotGranted);
        Assert.Equal(SecretProviderRealNonProductionRuntimeProbe.WarningText, status.Warning);
        Assert.Contains("crm-common-db-connection", status.LogicalSecretNames);
        Assert.DoesNotContain(status.LogicalSecretNames, name => name.Contains("=", StringComparison.Ordinal));
    }
}
