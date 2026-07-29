namespace CRM.Infrastructure.Security.Secrets;

public sealed record SecretProviderRuntimeOptions(
    bool Enabled,
    string RuntimeEnvironment,
    bool RedactionRequired,
    bool ProviderConfigured,
    IReadOnlyCollection<string> ApprovedSecretNames)
{
    public static SecretProviderRuntimeOptions Disabled() =>
        new(
            Enabled: false,
            RuntimeEnvironment: "NonProduction",
            RedactionRequired: true,
            ProviderConfigured: false,
            ApprovedSecretNames:
            [
                "crm-common-db-connection",
                "crm-portal-auth-base-url",
                "crm-portal-auth-client-id",
                "crm-portal-auth-client-secret",
                "crm-observability-endpoint"
            ]);
}
