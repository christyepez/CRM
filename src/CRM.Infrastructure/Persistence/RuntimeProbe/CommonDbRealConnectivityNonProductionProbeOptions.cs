namespace CRM.Infrastructure.Persistence.RuntimeProbe;

public sealed record CommonDbRealConnectivityNonProductionProbeOptions(
    bool Exists = true,
    bool ApprovalGranted = false,
    bool ProbeEnabled = false,
    bool ProbeAttempted = false,
    bool CommonDbConnected = false,
    bool ConnectionStringResolved = false,
    bool SqlConnectionCreated = false,
    bool DbConnectionCreated = false,
    bool UsesSyntheticFallback = true,
    bool ConnectionProbeSkippedBecauseSecretProviderApprovalNotGranted = true);
