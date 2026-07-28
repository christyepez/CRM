namespace CRM.Infrastructure.Security.Secrets;

public sealed record SecretProviderRealNonProductionRuntimeProbeStatus(
    bool Exists,
    bool ApprovalGranted,
    bool ProbeEnabled,
    bool ProbeAttempted,
    bool RuntimeConnected,
    bool RealSecretReadAttempted,
    bool LogicalSecretNamesValidated,
    bool ProbeSkippedBecauseApprovalNotGranted,
    string Warning,
    IReadOnlyCollection<string> LogicalSecretNames);

public sealed class SecretProviderRealNonProductionRuntimeProbe
{
    public const string WarningText = "Secret Provider real NonProduction runtime probe is prepared but skipped because approval is not granted";

    private static readonly string[] LogicalSecretNames =
    [
        "crm-common-db-connection",
        "crm-portal-auth-base-url",
        "crm-portal-auth-client-id",
        "crm-portal-auth-client-secret",
        "crm-observability-endpoint"
    ];

    public SecretProviderRealNonProductionRuntimeProbeStatus GetStatus() =>
        new(
            true,
            false,
            false,
            false,
            false,
            false,
            true,
            true,
            WarningText,
            LogicalSecretNames);
}
