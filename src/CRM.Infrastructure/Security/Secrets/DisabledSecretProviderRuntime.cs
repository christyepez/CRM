namespace CRM.Infrastructure.Security.Secrets;

public sealed class DisabledSecretProviderRuntime : ISecretProviderRuntime
{
    public Task<SecretProviderRuntimeReadResult> ReadAsync(
        SecretProviderRuntimeReadRequest request,
        CancellationToken cancellationToken = default) =>
        Task.FromResult(new SecretProviderRuntimeReadResult(
            SecretName: request.SecretName,
            ReadAttempted: false,
            ReadSucceeded: false,
            ValueObserved: false,
            ValueReturned: false,
            ValueLogged: false,
            ValuePersisted: false,
            ValueCached: false,
            ProviderConfigured: false,
            RedactionApplied: true,
            AllowedSecretName: false,
            Status: "Locked",
            Warning: "Secret Provider controlled real read is disabled by default",
            RedactedFingerprint: null));
}
