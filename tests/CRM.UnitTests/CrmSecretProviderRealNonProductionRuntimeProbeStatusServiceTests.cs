using CRM.Application.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmSecretProviderRealNonProductionRuntimeProbeStatusServiceTests
{
    [Fact]
    public void GetStatus_ReturnsSkippedRuntimeProbeWithoutSecretReads()
    {
        var status = new CrmSecretProviderRealNonProductionRuntimeProbeStatusService().GetStatus();

        Assert.Equal("SecretProviderRealNonProductionRuntimeProbe", status.Status);
        Assert.True(status.FoundationMode);
        Assert.True(status.SecretProviderRealNonProductionRuntimeProbeExists);
        Assert.False(status.SecretProviderRealNonProductionApprovalGranted);
        Assert.False(status.SecretProviderRealRuntimeProbeEnabled);
        Assert.False(status.SecretProviderRealRuntimeProbeAttempted);
        Assert.False(status.SecretProviderRealRuntimeConnected);
        Assert.False(status.RealSecretReadAttempted);
        Assert.False(status.RealSecretValueMaterialized);
        Assert.False(status.RealSecretValueLogged);
        Assert.False(status.SecretValueReturnedToApi);
        Assert.False(status.KeyVaultRuntimeClientCreated);
        Assert.False(status.KeyVaultRuntimeCallAttempted);
        Assert.False(status.AzureSecretSdkRuntimeEnabled);
        Assert.False(status.EnvSecretReadAttempted);
        Assert.False(status.EnvFileRequired);
        Assert.True(status.LogicalSecretNamesValidated);
        Assert.False(status.SecretValuesValidated);
        Assert.True(status.ProbeSkippedBecauseApprovalNotGranted);
        Assert.True(status.NonProductionOnly);
        Assert.True(status.RollbackRequired);
        Assert.True(status.ObservabilityRequired);
        Assert.Equal(CrmSecretProviderRealNonProductionRuntimeProbeStatusService.NextGate, status.NextGate);
        Assert.Equal(CrmSecretProviderRealNonProductionRuntimeProbeStatusService.WarningText, status.Warning);
        Assert.Contains(status.LogicalSecretNames, secret => secret.LogicalName == "crm-common-db-connection" && secret.LogicalNameAllowed && !secret.ValueRead);
    }
}
