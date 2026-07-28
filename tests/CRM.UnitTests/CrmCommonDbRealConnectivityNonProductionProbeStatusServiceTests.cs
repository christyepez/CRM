using CRM.Application.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmCommonDbRealConnectivityNonProductionProbeStatusServiceTests
{
    [Fact]
    public void GetStatus_ReturnsSkippedCommonDbProbeWithoutConnection()
    {
        var status = new CrmCommonDbRealConnectivityNonProductionProbeStatusService().GetStatus();

        Assert.Equal("CommonDbRealConnectivityNonProductionProbe", status.Status);
        Assert.True(status.FoundationMode);
        Assert.True(status.CommonDbRealConnectivityNonProductionProbeExists);
        Assert.False(status.CommonDbRealConnectivityApprovalGranted);
        Assert.False(status.SecretProviderRealNonProductionApprovalGranted);
        Assert.False(status.SecretProviderRealRuntimeProbeEnabled);
        Assert.False(status.ConnectionStringResolved);
        Assert.False(status.ConnectionStringValueMaterialized);
        Assert.False(status.ConnectionStringLogged);
        Assert.False(status.ConnectionStringReturnedToApi);
        Assert.False(status.CommonDbProbeEnabled);
        Assert.False(status.CommonDbProbeAttempted);
        Assert.False(status.CommonDbConnected);
        Assert.False(status.SqlConnectionCreated);
        Assert.False(status.DbConnectionCreated);
        Assert.False(status.UseSqlServerEnabled);
        Assert.False(status.EfRuntimeEnabled);
        Assert.False(status.AddDbContextRuntimeEnabled);
        Assert.False(status.MigrationsCreated);
        Assert.False(status.DatabaseSchemaChanged);
        Assert.False(status.ProductivePersistenceEnabled);
        Assert.False(status.ApiRequiresDatabase);
        Assert.False(status.UsesSecretProviderRuntime);
        Assert.True(status.UsesSyntheticFallback);
        Assert.Equal(CrmCommonDbRealConnectivityNonProductionProbeStatusService.SyntheticConnectionReference, status.SyntheticConnectionReference);
        Assert.True(status.ConnectionProbeSkippedBecauseSecretProviderApprovalNotGranted);
        Assert.Equal(CrmCommonDbRealConnectivityNonProductionProbeStatusService.NextGate, status.NextGate);
        Assert.Equal(CrmCommonDbRealConnectivityNonProductionProbeStatusService.WarningText, status.Warning);
    }
}
