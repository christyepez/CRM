using CRM.Application.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmSecretProviderRuntimeContractStatusServiceTests
{
    [Fact]
    public void GetStatus_ReturnsSecretProviderContractWithoutReadingSecrets()
    {
        var status = new CrmSecretProviderRuntimeContractStatusService().GetStatus();

        Assert.Equal("SecretProviderRuntimeContractValidation", status.Status);
        Assert.True(status.FoundationMode);
        Assert.True(status.SecretProviderContractExists);
        Assert.False(status.SecretProviderRuntimeConnected);
        Assert.False(status.SecretProviderReadsEnabled);
        Assert.False(status.SecretReadAttemptedByRuntime);
        Assert.False(status.RealSecretsConfigured);
        Assert.False(status.EnvFileRequired);
        Assert.False(status.ConnectionStringsConfigured);
        Assert.False(status.KeyVaultClientConfigured);
        Assert.False(status.SecretValuesExposed);
        Assert.False(status.CommonDbProbeActivationApproved);
        Assert.False(status.PortalAuthProbeActivationApproved);
        Assert.False(status.RuntimeProbeActivationApproved);
        Assert.Equal(CrmSecretProviderRuntimeContractStatusService.NextGate, status.NextGate);
        Assert.Equal(CrmSecretProviderRuntimeContractStatusService.WarningText, status.Warning);
        Assert.Contains(status.LogicalSecrets, secret => secret.LogicalName == "CRM_COMMON_DB_CONNECTION" && !secret.ValueConfigured && !secret.ValueExposed);
        Assert.Contains(status.LogicalSecrets, secret => secret.LogicalName == "CRM_PORTAL_AUTH_CLIENT_SECRET" && secret.Scope == "LogicalNameOnly");
        Assert.All(status.ApprovalGates, gate =>
        {
            Assert.True(gate.Required);
            Assert.False(gate.Approved);
        });
        Assert.All(status.NoReadPolicies, policy => Assert.True(policy.Enforced));
        Assert.Contains("Secret read attempts.", status.BlockedItems);
    }
}
