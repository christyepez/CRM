namespace CRM.Infrastructure.Security.Secrets;

public sealed record SecretProviderRuntimeContractOptions(
    bool Exists = true,
    bool RuntimeConnected = false,
    bool ReadsEnabled = false,
    bool SecretReadAttempted = false,
    string Strategy = "ContractOnly");
