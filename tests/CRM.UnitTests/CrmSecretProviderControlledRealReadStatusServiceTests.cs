using CRM.Application.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmSecretProviderControlledRealReadStatusServiceTests
{
    [Fact]
    public void GetStatus_ReturnsFailClosedSprint8P2Defaults()
    {
        var status = new CrmSecretProviderControlledRealReadStatusService().GetStatus();

        Assert.Equal("SecretProviderControlledRealNonProductionRead", status.Status);
        Assert.True(status.FoundationMode);
        Assert.True(status.SecretProviderControlledRealNonProductionReadExists);
        Assert.True(status.SecretProviderControlledRealNonProductionReadApproved);
        Assert.False(status.SecretProviderControlledRealNonProductionReadEnabled);
        Assert.False(status.SecretProviderControlledRealNonProductionReadAttempted);
        Assert.False(status.RealSecretReadAttempted);
        Assert.False(status.RealSecretValueMaterialized);
        Assert.False(status.RealSecretValueLogged);
        Assert.False(status.SecretValueReturnedToApi);
        Assert.False(status.SecretValuePersisted);
        Assert.False(status.SecretValueCached);
        Assert.False(status.KeyVaultRuntimeClientCreated);
        Assert.False(status.KeyVaultRuntimeCallAttempted);
        Assert.False(status.AzureSecretSdkRuntimeEnabled);
        Assert.True(status.UsesApprovedSecretNamesOnly);
        Assert.True(status.NonProductionOnly);
        Assert.True(status.FailClosedByDefault);
        Assert.Equal(CrmSecretProviderControlledRealReadStatusService.NextGate, status.NextGate);
        Assert.Equal(CrmSecretProviderControlledRealReadStatusService.WarningText, status.Warning);
    }

    [Fact]
    public void GetSecrets_ReturnsApprovedLogicalNamesWithoutValues()
    {
        var secrets = new CrmSecretProviderControlledRealReadStatusService().GetSecrets();

        Assert.Collection(
            secrets,
            item => Assert.Equal("crm-common-db-connection", item.SecretName),
            item => Assert.Equal("crm-portal-auth-base-url", item.SecretName),
            item => Assert.Equal("crm-portal-auth-client-id", item.SecretName),
            item => Assert.Equal("crm-portal-auth-client-secret", item.SecretName),
            item => Assert.Equal("crm-observability-endpoint", item.SecretName));

        Assert.All(secrets, item =>
        {
            Assert.True(item.Approved);
            Assert.False(item.ValueApproved);
            Assert.False(item.ValueReturnedToApi);
        });
    }
}
