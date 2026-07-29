using CRM.Infrastructure.Security.Secrets;
using Xunit;

namespace CRM.UnitTests;

public sealed class ControlledNonProductionSecretProviderRuntimeTests
{
    [Fact]
    public async Task ReadAsync_BlocksNamesOutsideAllowList()
    {
        var runtime = new ControlledNonProductionSecretProviderRuntime(EnabledOptions());

        var result = await runtime.ReadAsync(new SecretProviderRuntimeReadRequest("crm-not-approved"));

        Assert.False(result.ReadAttempted);
        Assert.False(result.AllowedSecretName);
        Assert.Equal("Blocked", result.Status);
        Assert.False(result.ValueReturned);
    }

    [Fact]
    public async Task ReadAsync_RequiresNonProduction()
    {
        var runtime = new ControlledNonProductionSecretProviderRuntime(EnabledOptions("Production"));

        var result = await runtime.ReadAsync(new SecretProviderRuntimeReadRequest("crm-common-db-connection"));

        Assert.False(result.ReadAttempted);
        Assert.Equal("Locked", result.Status);
        Assert.False(result.ValueReturned);
    }

    [Fact]
    public async Task ReadAsync_RequiresExplicitFlag()
    {
        var runtime = new ControlledNonProductionSecretProviderRuntime(EnabledOptions(enabled: false));

        var result = await runtime.ReadAsync(new SecretProviderRuntimeReadRequest("crm-common-db-connection"));

        Assert.False(result.ReadAttempted);
        Assert.Equal("Locked", result.Status);
        Assert.False(result.ValueReturned);
    }

    [Fact]
    public async Task ReadAsync_SkipsWhenProviderIsNotConfigured()
    {
        var runtime = new ControlledNonProductionSecretProviderRuntime(EnabledOptions(providerConfigured: false));

        var result = await runtime.ReadAsync(new SecretProviderRuntimeReadRequest("crm-common-db-connection"));

        Assert.False(result.ReadAttempted);
        Assert.Equal("Skipped", result.Status);
        Assert.False(result.ValueReturned);
    }

    [Fact]
    public async Task ReadAsync_ReturnsOnlySanitizedMetadataWhenProviderReads()
    {
        var fake = new InMemoryNonProductionSecretProviderRuntime(
            new Dictionary<string, string>
            {
                ["crm-common-db-connection"] = "dummy-secret-value-never-committed-real"
            });
        var runtime = new ControlledNonProductionSecretProviderRuntime(
            EnabledOptions(),
            fake.ReadInternalAsync);

        var result = await runtime.ReadAsync(new SecretProviderRuntimeReadRequest("crm-common-db-connection"));

        Assert.True(result.ReadAttempted);
        Assert.True(result.ReadSucceeded);
        Assert.False(result.ValueObserved);
        Assert.False(result.ValueReturned);
        Assert.False(result.ValueLogged);
        Assert.False(result.ValuePersisted);
        Assert.False(result.ValueCached);
        Assert.True(result.ProviderConfigured);
        Assert.True(result.RedactionApplied);
        Assert.Equal("Succeeded", result.Status);
        Assert.NotNull(result.RedactedFingerprint);
        Assert.DoesNotContain("dummy-secret-value", result.RedactedFingerprint);
    }

    private static SecretProviderRuntimeOptions EnabledOptions(
        string environment = "NonProduction",
        bool enabled = true,
        bool providerConfigured = true) =>
        new(
            Enabled: enabled,
            RuntimeEnvironment: environment,
            RedactionRequired: true,
            ProviderConfigured: providerConfigured,
            ApprovedSecretNames:
            [
                "crm-common-db-connection",
                "crm-portal-auth-base-url",
                "crm-portal-auth-client-id",
                "crm-portal-auth-client-secret",
                "crm-observability-endpoint"
            ]);
}
