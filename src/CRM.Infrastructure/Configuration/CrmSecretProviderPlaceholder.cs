namespace CRM.Infrastructure.Configuration;

public sealed record CrmSecretProviderPlaceholderStatus(
    bool Configured,
    bool RuntimeConnected,
    string Strategy,
    string Warning);

public sealed class CrmSecretProviderPlaceholder
{
    public const string WarningText = "Secret provider placeholder only; no secrets are read";

    public CrmSecretProviderPlaceholderStatus GetStatus() =>
        new(false, false, "ContractOnly", WarningText);
}
