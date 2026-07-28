using CRM.Application.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmSecretProviderSafeMockActivationStatusServiceTests
{
    [Fact]
    public void GetStatus_ReturnsSafeMockEnabledWithNoRealSecretAccess()
    {
        var service = new CrmSecretProviderSafeMockActivationStatusService();

        var status = service.GetStatus();

        Assert.Equal("CRM", status.Module);
        Assert.Equal("SecretProviderSafeMockActivation", status.Status);
        Assert.True(status.FoundationMode);
        Assert.True(status.SecretProviderSafeMockExists);
        Assert.True(status.SecretProviderSafeMockEnabled);
        Assert.False(status.SecretProviderRuntimeConnected);
        Assert.False(status.SecretProviderReadsRealSecrets);
        Assert.True(status.SecretProviderReadsSyntheticValues);
        Assert.True(status.SecretProviderReadsEnabledForMockOnly);
        Assert.False(status.RealSecretsConfigured);
        Assert.False(status.EnvFileRequired);
        Assert.False(status.KeyVaultClientConfigured);
        Assert.False(status.AzureSdkForSecretsConfigured);
        Assert.False(status.SecretValuesExposedInLogs);
        Assert.False(status.CommonDbDryRunApprovalGranted);
        Assert.False(status.PortalAuthDryRunApprovalGranted);
        Assert.False(status.RealActivationApprovalGranted);
        Assert.True(status.NonProductionOnly);
        Assert.Equal("Sprint6P3CommonDbConnectivityDryRunContract", status.NextGate);
        Assert.Equal("Secret Provider safe mock only; no real secrets are read", status.Warning);
        Assert.All(status.SyntheticValues, value =>
        {
            Assert.True(value.Synthetic);
            Assert.False(value.Sensitive);
            Assert.False(value.RuntimeUsable);
        });
    }
}
