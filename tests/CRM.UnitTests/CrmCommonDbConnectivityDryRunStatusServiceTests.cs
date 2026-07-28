using CRM.Application.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmCommonDbConnectivityDryRunStatusServiceTests
{
    [Fact]
    public void GetStatus_ReturnsContractOnlyDryRunWithoutConnection()
    {
        var service = new CrmCommonDbConnectivityDryRunStatusService();

        var status = service.GetStatus();

        Assert.Equal("CRM", status.Module);
        Assert.Equal("CommonDbConnectivityDryRunContract", status.Status);
        Assert.True(status.FoundationMode);
        Assert.True(status.CommonDbConnectivityDryRunContractExists);
        Assert.False(status.CommonDbDryRunApprovalGranted);
        Assert.False(status.CommonDbDryRunEnabled);
        Assert.False(status.CommonDbConnectionAttempted);
        Assert.True(status.UsesSecretProviderSafeMockMetadata);
        Assert.True(status.UsesSyntheticConnectionReference);
        Assert.Equal("mock://crm/common-db", status.SyntheticConnectionReference);
        Assert.False(status.RealConnectionStringUsed);
        Assert.False(status.ConnectionStringResolved);
        Assert.False(status.SqlConnectionCreated);
        Assert.False(status.DbConnectionCreated);
        Assert.False(status.EfRuntimeEnabled);
        Assert.False(status.MigrationsCreated);
        Assert.False(status.ApiRequiresDatabase);
        Assert.True(status.NonProductionOnly);
        Assert.True(status.RollbackRequired);
        Assert.True(status.ObservabilityRequired);
        Assert.Equal("Sprint6P4PortalAuthTokenPropagationDryRunContract", status.NextGate);
        Assert.Equal("Common DB connectivity dry-run contract only; no database connection is attempted", status.Warning);
    }
}
