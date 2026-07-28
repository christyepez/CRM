using CRM.Infrastructure.Security.Secrets;
using Xunit;

namespace CRM.UnitTests;

public sealed class SecretProviderSafeMockTests
{
    [Theory]
    [InlineData("crm.common-db", "mock://crm/common-db")]
    [InlineData("crm.portal-auth-base-url", "mock://crm/portal-auth-base-url")]
    [InlineData("crm.client-id", "mock-client-id")]
    [InlineData("crm.client-secret", "mock-client-secret-not-real")]
    [InlineData("crm.observability", "mock://crm/observability")]
    public void TryGetSyntheticSecret_ReturnsOnlySyntheticNonRuntimeValues(string logicalName, string expectedValue)
    {
        var mock = new SecretProviderSafeMock();

        var found = mock.TryGetSyntheticSecret(logicalName, out var value);

        Assert.True(found);
        Assert.NotNull(value);
        Assert.Equal(logicalName, value.LogicalName);
        Assert.Equal(expectedValue, value.Value);
        Assert.True(value.Synthetic);
        Assert.False(value.Sensitive);
        Assert.False(value.RuntimeUsable);
    }

    [Fact]
    public void TryGetSyntheticSecret_ReturnsFalseForUnknownLogicalName()
    {
        var mock = new SecretProviderSafeMock();

        var found = mock.TryGetSyntheticSecret("crm.unknown", out var value);

        Assert.False(found);
        Assert.Null(value);
    }
}
