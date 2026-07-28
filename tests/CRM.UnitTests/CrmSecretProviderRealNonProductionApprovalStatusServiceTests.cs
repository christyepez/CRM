using CRM.Application.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmSecretProviderRealNonProductionApprovalStatusServiceTests
{
    [Fact]
    public void GetStatus_ReturnsApprovalPackageWithoutRealSecretReads()
    {
        var status = new CrmSecretProviderRealNonProductionApprovalStatusService().GetStatus();

        Assert.Equal("SecretProviderRealNonProductionApproval", status.Status);
        Assert.True(status.FoundationMode);
        Assert.True(status.SecretProviderRealNonProductionApprovalPackageExists);
        Assert.False(status.SecretProviderRealNonProductionApprovalGranted);
        Assert.False(status.SecretProviderRealRuntimeEnabled);
        Assert.False(status.SecretProviderRealRuntimeConnected);
        Assert.False(status.RealSecretReadAttempted);
        Assert.False(status.KeyVaultRuntimeClientEnabled);
        Assert.False(status.AzureSecretSdkRuntimeEnabled);
        Assert.False(status.EnvFileRequired);
        Assert.False(status.EnvSecretReadAllowed);
        Assert.False(status.SecretsLogged);
        Assert.False(status.SecretNamesApproved);
        Assert.False(status.SecretValuesApproved);
        Assert.True(status.NonProductionOnly);
        Assert.True(status.SecurityReviewRequired);
        Assert.True(status.ArchitectureReviewRequired);
        Assert.True(status.DevOpsReviewRequired);
        Assert.True(status.RollbackRequired);
        Assert.True(status.ObservabilityRequired);
        Assert.Equal(CrmSecretProviderRealNonProductionApprovalStatusService.NextGate, status.NextGate);
        Assert.Equal(CrmSecretProviderRealNonProductionApprovalStatusService.WarningText, status.Warning);
        Assert.Contains(status.LogicalSecretNames, secret => secret.LogicalName == "crm-common-db-connection" && !secret.ValueIncluded);
        Assert.Contains(status.BlockedItems, item => item.Item == "Real secret reads");
    }
}
