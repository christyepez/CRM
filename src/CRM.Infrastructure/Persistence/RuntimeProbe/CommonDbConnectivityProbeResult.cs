namespace CRM.Infrastructure.Persistence.RuntimeProbe;

public sealed record CommonDbConnectivityProbeResult(
    string SecretName,
    bool ProbeAttempted,
    bool ProviderConfigured,
    bool SecretProviderAvailabilityMetadataUsed,
    bool ConnectionAttempted,
    bool Connected,
    bool TimeoutApplied,
    long ElapsedMs,
    string ErrorCategory,
    bool ConnectionStringReturned,
    bool ConnectionStringLogged,
    bool ConnectionStringPersisted,
    bool ConnectionStringCached,
    bool AllowedSecretName,
    string Status,
    string Warning);
