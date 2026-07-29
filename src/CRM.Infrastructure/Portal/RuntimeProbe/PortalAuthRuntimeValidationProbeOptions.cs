namespace CRM.Infrastructure.Portal.RuntimeProbe;

public sealed record PortalAuthRuntimeValidationProbeOptions(
    bool Enabled,
    string RuntimeEnvironment,
    bool SecretProviderControlledReadApproved,
    bool ProviderConfigured,
    int TimeoutSeconds)
{
    public const string BaseUrlSecretName = "crm-portal-auth-base-url";
    public const string ClientIdSecretName = "crm-portal-auth-client-id";
    public const string ClientSecretName = "crm-portal-auth-client-secret";

    public static PortalAuthRuntimeValidationProbeOptions Disabled() =>
        new(false, "NonProduction", false, false, 3);
}
