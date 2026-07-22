using CRM.Infrastructure.Configuration;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmConfigurationPlaceholderTests
{
    [Fact]
    public void SecretProviderPlaceholder_DoesNotReadSecrets()
    {
        var status = new CrmSecretProviderPlaceholder().GetStatus();

        Assert.False(status.Configured);
        Assert.False(status.RuntimeConnected);
        Assert.Equal("ContractOnly", status.Strategy);
        Assert.Equal(CrmSecretProviderPlaceholder.WarningText, status.Warning);
    }

    [Fact]
    public void DatabaseConfigurationPlaceholder_DoesNotReadConnectionValues()
    {
        var status = new CrmDatabaseConfigurationPlaceholder().GetStatus();

        Assert.False(status.Configured);
        Assert.False(status.RuntimeConnected);
        Assert.Equal("CrmDb", status.LogicalDatabaseName);
        Assert.True(status.LogicalDatabaseNameIsPlaceholder);
        Assert.Equal(CrmDatabaseConfigurationPlaceholder.WarningText, status.Warning);
    }
}
