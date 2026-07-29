namespace CRM.Infrastructure.Security.Secrets;

public sealed class InMemoryNonProductionSecretProviderRuntime(IReadOnlyDictionary<string, string> values)
{
    public Task<string?> ReadInternalAsync(string secretName, CancellationToken cancellationToken = default)
    {
        values.TryGetValue(secretName, out var value);
        return Task.FromResult(value);
    }
}
