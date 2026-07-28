namespace CRM.Infrastructure.Persistence.RuntimeProbe;

public sealed record CommonDbRealConnectivityNonProductionProbeStatus(
    bool Exists,
    bool ApprovalGranted,
    bool ProbeEnabled,
    bool ProbeAttempted,
    bool CommonDbConnected,
    bool ConnectionStringResolved,
    bool SqlConnectionCreated,
    bool DbConnectionCreated,
    bool UsesSyntheticFallback,
    string SyntheticConnectionReference,
    bool ConnectionProbeSkippedBecauseSecretProviderApprovalNotGranted,
    string Warning);

public sealed class CommonDbRealConnectivityNonProductionProbe
{
    public const string SyntheticReference = "mock://crm/common-db";
    public const string WarningText = "Common DB real connectivity NonProduction probe is prepared but skipped because Secret Provider approval is not granted";

    public CommonDbRealConnectivityNonProductionProbeStatus GetStatus() =>
        new(
            true,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            true,
            SyntheticReference,
            true,
            WarningText);
}
