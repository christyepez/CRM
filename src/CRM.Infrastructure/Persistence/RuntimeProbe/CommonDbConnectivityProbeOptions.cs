namespace CRM.Infrastructure.Persistence.RuntimeProbe;

public sealed record CommonDbConnectivityProbeOptions(
    bool Enabled,
    string RuntimeEnvironment,
    bool SecretProviderControlledReadApproved,
    bool ProviderConfigured,
    string SecretName,
    int TimeoutSeconds)
{
    public const string ApprovedSecretName = "crm-common-db-connection";

    public static CommonDbConnectivityProbeOptions Disabled() =>
        new(
            Enabled: false,
            RuntimeEnvironment: "NonProduction",
            SecretProviderControlledReadApproved: true,
            ProviderConfigured: false,
            SecretName: ApprovedSecretName,
            TimeoutSeconds: 3);
}
