namespace CRM.Infrastructure.Security.Secrets;

public sealed record SecretProviderRuntimeContractPlaceholderStatus(
    bool Exists,
    bool RuntimeConnected,
    bool ReadsEnabled,
    bool SecretReadAttempted,
    string Strategy,
    string Warning);

public sealed class SecretProviderRuntimeContractPlaceholder
{
    public const string WarningText = "Secret Provider contract validation only; no secrets are read";

    public SecretProviderRuntimeContractPlaceholderStatus GetStatus() =>
        new(
            true,
            false,
            false,
            false,
            "ContractOnly",
            WarningText);
}
