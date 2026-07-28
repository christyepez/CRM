namespace CRM.Infrastructure.Security.Secrets;

public sealed record SecretProviderSafeMockValue(
    string LogicalName,
    string Value,
    bool Synthetic,
    bool Sensitive,
    bool RuntimeUsable);

public sealed class SecretProviderSafeMock
{
    private static readonly IReadOnlyDictionary<string, SecretProviderSafeMockValue> SyntheticValues =
        new Dictionary<string, SecretProviderSafeMockValue>(StringComparer.OrdinalIgnoreCase)
        {
            ["crm.common-db"] = new("crm.common-db", "mock://crm/common-db", true, false, false),
            ["crm.portal-auth-base-url"] = new("crm.portal-auth-base-url", "mock://crm/portal-auth-base-url", true, false, false),
            ["crm.client-id"] = new("crm.client-id", "mock-client-id", true, false, false),
            ["crm.client-secret"] = new("crm.client-secret", "mock-client-secret-not-real", true, false, false),
            ["crm.observability"] = new("crm.observability", "mock://crm/observability", true, false, false)
        };

    public bool TryGetSyntheticSecret(string logicalName, out SecretProviderSafeMockValue? value) =>
        SyntheticValues.TryGetValue(logicalName, out value);

    public IReadOnlyCollection<SecretProviderSafeMockValue> GetSyntheticValues() =>
        SyntheticValues.Values.ToArray();
}
