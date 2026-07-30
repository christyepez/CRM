using CRM.Application.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmSecretProviderRuntimeEnablementTrialStatusServiceTests
{
    [Fact]
    public void GetStatus_ReturnsDefaultDisabledTrialDecision()
    {
        var status = new CrmSecretProviderRuntimeEnablementTrialStatusService().GetStatus();

        Assert.Equal("SecretProviderRuntimeEnablementTrial", status.Status);
        Assert.True(status.FoundationMode);
        Assert.True(status.SecretProviderRuntimeEnablementTrialExists);
        Assert.True(status.SecretProviderRuntimeEnablementTrialApproved);
        Assert.False(status.SecretProviderRuntimeEnablementTrialEnabled);
        Assert.False(status.SecretProviderRuntimeTrialAttempted);
        Assert.False(status.SecretProviderRuntimeConnected);
        Assert.False(status.RealSecretReadAttempted);
        Assert.False(status.SecretValueReturnedToApi);
        Assert.False(status.SecretValuePersisted);
        Assert.False(status.SecretValueCached);
        Assert.True(status.AllowedLogicalSecretNamesEnforced);
        Assert.True(status.NonProductionOnly);
        Assert.True(status.ProductionBlocked);
        Assert.True(status.FailClosedByDefault);
        Assert.Equal("Sprint9P3CommonDbRuntimeConnectivityTrial", status.NextGate);
    }

    [Fact]
    public void GetStatus_ReturnsAllowedLogicalNames()
    {
        var status = new CrmSecretProviderRuntimeEnablementTrialStatusService().GetStatus();

        Assert.Contains("crm-common-db-connection", status.AllowedLogicalSecretNames);
        Assert.Contains("crm-portal-auth-base-url", status.AllowedLogicalSecretNames);
        Assert.Contains("crm-portal-auth-client-id", status.AllowedLogicalSecretNames);
        Assert.Contains("crm-portal-auth-client-secret", status.AllowedLogicalSecretNames);
        Assert.Contains("crm-observability-endpoint", status.AllowedLogicalSecretNames);
    }
}
