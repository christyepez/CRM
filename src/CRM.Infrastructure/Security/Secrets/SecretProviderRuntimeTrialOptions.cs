namespace CRM.Infrastructure.Security.Secrets;

public sealed record SecretProviderRuntimeTrialOptions(
    bool Enabled,
    string RuntimeEnvironment,
    IReadOnlyCollection<string> AllowedLogicalSecretNames)
{
    public static SecretProviderRuntimeTrialOptions Disabled(string runtimeEnvironment) =>
        new(
            Enabled: false,
            RuntimeEnvironment: runtimeEnvironment,
            AllowedLogicalSecretNames:
            [
                "crm-common-db-connection",
                "crm-portal-auth-base-url",
                "crm-portal-auth-client-id",
                "crm-portal-auth-client-secret",
                "crm-observability-endpoint"
            ]);
}
