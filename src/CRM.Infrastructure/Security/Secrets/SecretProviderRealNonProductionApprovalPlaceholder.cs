namespace CRM.Infrastructure.Security.Secrets;

public sealed record SecretProviderRealNonProductionApprovalPlaceholderStatus(
    bool Exists,
    bool ApprovalGranted,
    bool RuntimeEnabled,
    bool RuntimeConnected,
    bool RealSecretReadAttempted,
    string Warning,
    IReadOnlyCollection<string> LogicalSecretNames);

public sealed class SecretProviderRealNonProductionApprovalPlaceholder
{
    public const string WarningText = "Secret Provider real NonProduction approval package only; no real secrets are read";

    private static readonly string[] LogicalSecretNames =
    [
        "crm-common-db-connection",
        "crm-portal-auth-base-url",
        "crm-portal-auth-client-id",
        "crm-portal-auth-client-secret",
        "crm-observability-endpoint"
    ];

    public SecretProviderRealNonProductionApprovalPlaceholderStatus GetStatus() =>
        new(
            true,
            false,
            false,
            false,
            false,
            WarningText,
            LogicalSecretNames);
}
