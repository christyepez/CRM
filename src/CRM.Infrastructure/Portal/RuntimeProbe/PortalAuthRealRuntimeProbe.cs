namespace CRM.Infrastructure.Portal.RuntimeProbe;

public sealed record PortalAuthRealRuntimeProbeStatus(
    bool Exists,
    bool ApprovalGranted,
    bool ProbeEnabled,
    bool ProbeAttempted,
    bool PortalAuthRuntimeConnected,
    bool PortalHttpClientCreated,
    bool PortalHttpCallAttempted,
    bool TokenReadAttempted,
    bool HeaderReadAttempted,
    bool UsesSyntheticFallback,
    string SyntheticPortalAuthReference,
    string SyntheticUserReference,
    bool ProbeSkippedBecausePortalAuthApprovalNotGranted,
    string Warning);

public sealed class PortalAuthRealRuntimeProbe
{
    public const string SyntheticPortalAuthReference = "mock://crm/portal-auth";
    public const string SyntheticUserReference = "mock://crm/portal-user";
    public const string WarningText = "Portal Auth real runtime probe is prepared but skipped because Portal Auth approval is not granted";

    public PortalAuthRealRuntimeProbeStatus GetStatus() =>
        new(
            true,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            true,
            SyntheticPortalAuthReference,
            SyntheticUserReference,
            true,
            WarningText);
}
