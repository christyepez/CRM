namespace CRM.Infrastructure.Security.Secrets;

public interface ISecretProviderRuntime
{
    Task<SecretProviderRuntimeReadResult> ReadAsync(
        SecretProviderRuntimeReadRequest request,
        CancellationToken cancellationToken = default);
}
