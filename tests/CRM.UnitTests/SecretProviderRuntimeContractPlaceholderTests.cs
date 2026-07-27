using CRM.Infrastructure.Security.Secrets;
using Xunit;

namespace CRM.UnitTests;

public sealed class SecretProviderRuntimeContractPlaceholderTests
{
    [Fact]
    public void GetStatus_ReturnsContractOnlyWithoutRuntimeConnectionOrReadAttempt()
    {
        var status = new SecretProviderRuntimeContractPlaceholder().GetStatus();

        Assert.True(status.Exists);
        Assert.False(status.RuntimeConnected);
        Assert.False(status.ReadsEnabled);
        Assert.False(status.SecretReadAttempted);
        Assert.Equal("ContractOnly", status.Strategy);
        Assert.Equal(SecretProviderRuntimeContractPlaceholder.WarningText, status.Warning);
    }
}
