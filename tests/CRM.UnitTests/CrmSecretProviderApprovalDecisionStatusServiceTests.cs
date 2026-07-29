using CRM.Application.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmSecretProviderApprovalDecisionStatusServiceTests
{
    [Fact]
    public void GetStatus_ReturnsSprint8P1ApprovalDecisionWithoutRealRead()
    {
        var status = new CrmSecretProviderApprovalDecisionStatusService().GetStatus();

        Assert.Equal("SecretProviderApprovalDecision", status.Status);
        Assert.True(status.SecretProviderApprovalDecisionExists);
        Assert.Equal("ApprovedForControlledNonProductionReadPlanning", status.SecretProviderApprovalDecision);
        Assert.True(status.SecretProviderRealReadApprovedForNextSprint);
        Assert.False(status.SecretProviderRealReadEnabledNow);
        Assert.False(status.RealSecretReadAttempted);
        Assert.False(status.RealSecretValueMaterialized);
        Assert.False(status.RealSecretValueLogged);
        Assert.False(status.SecretValueReturnedToApi);
        Assert.False(status.KeyVaultRuntimeClientCreated);
        Assert.False(status.KeyVaultRuntimeCallAttempted);
        Assert.False(status.AzureSecretSdkRuntimeEnabled);
        Assert.False(status.EnvFileRequired);
        Assert.False(status.EnvSecretReadAllowed);
        Assert.True(status.ApprovedSecretNamesOnly);
        Assert.False(status.ApprovedSecretValues);
        Assert.True(status.ApprovedForNonProductionOnly);
        Assert.True(status.SecurityApprovalRecorded);
        Assert.True(status.ArchitectureApprovalRecorded);
        Assert.True(status.DevOpsApprovalRecorded);
        Assert.Equal(CrmSecretProviderApprovalDecisionStatusService.NextGate, status.NextGate);
        Assert.Equal(CrmSecretProviderApprovalDecisionStatusService.WarningText, status.Warning);
    }

    [Fact]
    public void GetApprovedSecrets_ReturnsOnlyLogicalNamesWithoutValues()
    {
        var secrets = new CrmSecretProviderApprovalDecisionStatusService().GetApprovedSecrets();

        Assert.Collection(
            secrets,
            item => Assert.Equal("crm-common-db-connection", item.LogicalName),
            item => Assert.Equal("crm-portal-auth-base-url", item.LogicalName),
            item => Assert.Equal("crm-portal-auth-client-id", item.LogicalName),
            item => Assert.Equal("crm-portal-auth-client-secret", item.LogicalName),
            item => Assert.Equal("crm-observability-endpoint", item.LogicalName));

        Assert.All(secrets, item =>
        {
            Assert.False(item.ValueApproved);
            Assert.False(item.ValueReturnedToApi);
        });
    }
}
