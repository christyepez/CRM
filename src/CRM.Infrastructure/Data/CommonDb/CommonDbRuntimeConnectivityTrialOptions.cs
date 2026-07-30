namespace CRM.Infrastructure.Data.CommonDb;

public sealed record CommonDbRuntimeConnectivityTrialOptions(
    bool Enabled,
    string RuntimeEnvironment,
    string SecretName);
