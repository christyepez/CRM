using System.Security.Cryptography;
using System.Text;

namespace CRM.Infrastructure.Security.Secrets;

public sealed class ControlledNonProductionSecretProviderRuntime(
    SecretProviderRuntimeOptions options,
    Func<string, CancellationToken, Task<string?>>? safeRead = null) : ISecretProviderRuntime
{
    public async Task<SecretProviderRuntimeReadResult> ReadAsync(
        SecretProviderRuntimeReadRequest request,
        CancellationToken cancellationToken = default)
    {
        var allowed = options.ApprovedSecretNames.Contains(request.SecretName, StringComparer.OrdinalIgnoreCase);
        if (!allowed)
        {
            return Sanitized(request.SecretName, false, false, false, "Blocked", "Secret name is not approved", null);
        }

        if (!options.Enabled || !options.RedactionRequired || !IsNonProduction(options.RuntimeEnvironment))
        {
            return Sanitized(request.SecretName, false, false, options.ProviderConfigured, "Locked", "Controlled real read is fail-closed", null);
        }

        if (!options.ProviderConfigured || safeRead is null)
        {
            return Sanitized(request.SecretName, false, false, false, "Skipped", "External Secret Provider is not configured", null);
        }

        var internalValue = await safeRead(request.SecretName, cancellationToken);
        var observed = !string.IsNullOrWhiteSpace(internalValue);
        var fingerprint = observed ? Fingerprint(internalValue!) : null;

        return Sanitized(request.SecretName, true, observed, true, observed ? "Succeeded" : "Skipped", "Secret value was redacted and was not returned", fingerprint);
    }

    private static SecretProviderRuntimeReadResult Sanitized(
        string secretName,
        bool attempted,
        bool succeeded,
        bool providerConfigured,
        string status,
        string warning,
        string? fingerprint) =>
        new(
            SecretName: secretName,
            ReadAttempted: attempted,
            ReadSucceeded: succeeded,
            ValueObserved: false,
            ValueReturned: false,
            ValueLogged: false,
            ValuePersisted: false,
            ValueCached: false,
            ProviderConfigured: providerConfigured,
            RedactionApplied: true,
            AllowedSecretName: status != "Blocked",
            Status: status,
            Warning: warning,
            RedactedFingerprint: fingerprint);

    private static bool IsNonProduction(string value) =>
        value.Equals("NonProduction", StringComparison.OrdinalIgnoreCase)
        || value.Equals("Development", StringComparison.OrdinalIgnoreCase)
        || value.Equals("Staging", StringComparison.OrdinalIgnoreCase);

    private static string Fingerprint(string value)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(value));
        return Convert.ToHexString(bytes)[..16];
    }
}
