namespace CRM.Infrastructure.Security.Secrets;

public sealed record SecretProviderRuntimeTrialResult(
    string SecretName,
    bool ReadAttempted,
    bool ReadSucceeded,
    bool ProviderConfigured,
    bool ValueReturned,
    bool ValueLogged,
    bool ValuePersisted,
    bool ValueCached,
    bool RedactionApplied,
    bool ProductionBlocked,
    bool AllowedLogicalSecretName,
    long ElapsedMs,
    string Status,
    string Warning,
    string? ErrorCategory);
