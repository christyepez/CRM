namespace CRM.Infrastructure.Security.Secrets;

public sealed record SecretProviderRuntimeReadResult(
    string SecretName,
    bool ReadAttempted,
    bool ReadSucceeded,
    bool ValueObserved,
    bool ValueReturned,
    bool ValueLogged,
    bool ValuePersisted,
    bool ValueCached,
    bool ProviderConfigured,
    bool RedactionApplied,
    bool AllowedSecretName,
    string Status,
    string Warning,
    string? RedactedFingerprint);
