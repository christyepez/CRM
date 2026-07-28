using CRM.Infrastructure.Security.Secrets;
using Xunit;

namespace CRM.UnitTests;

public sealed class SecretProviderRealNonProductionApprovalPlaceholderTests
{
    [Fact]
    public void GetStatus_ReturnsContractOnlyApprovalState()
    {
        var status = new SecretProviderRealNonProductionApprovalPlaceholder().GetStatus();

        Assert.True(status.Exists);
        Assert.False(status.ApprovalGranted);
        Assert.False(status.RuntimeEnabled);
        Assert.False(status.RuntimeConnected);
        Assert.False(status.RealSecretReadAttempted);
        Assert.Equal(SecretProviderRealNonProductionApprovalPlaceholder.WarningText, status.Warning);
        Assert.Contains("crm-common-db-connection", status.LogicalSecretNames);
        Assert.DoesNotContain(status.LogicalSecretNames, name => name.Contains("=", StringComparison.Ordinal));
    }
}
