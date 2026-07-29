using CRM.Infrastructure.Security.Secrets;
using Xunit;

namespace CRM.UnitTests;

public sealed class SecretProviderRuntimeTrialServiceTests
{
    private static readonly string[] AllowedNames = ["crm-common-db-connection"];

    [Fact]
    public async Task ProbeAsync_WhenFlagDisabled_ReturnsLockedWithoutRead()
    {
        var service = new SecretProviderRuntimeTrialService(
            new SecretProviderRuntimeTrialOptions(false, "Development", AllowedNames),
            new RecordingSecretProviderRuntime());

        var result = await service.ProbeAsync("crm-common-db-connection");

        Assert.False(result.ReadAttempted);
        Assert.False(result.ReadSucceeded);
        Assert.False(result.ValueReturned);
        Assert.False(result.ValueLogged);
        Assert.False(result.ValuePersisted);
        Assert.False(result.ValueCached);
        Assert.True(result.RedactionApplied);
        Assert.True(result.ProductionBlocked);
        Assert.Equal("FlagDisabled", result.ErrorCategory);
    }

    [Fact]
    public async Task ProbeAsync_WhenSecretNameIsNotAllowed_ReturnsLocked()
    {
        var service = new SecretProviderRuntimeTrialService(
            new SecretProviderRuntimeTrialOptions(true, "Development", AllowedNames),
            new RecordingSecretProviderRuntime());

        var result = await service.ProbeAsync("not-approved");

        Assert.False(result.ReadAttempted);
        Assert.False(result.AllowedLogicalSecretName);
        Assert.Equal("SecretNameNotAllowed", result.ErrorCategory);
    }

    [Fact]
    public async Task ProbeAsync_WhenProduction_ReturnsLocked()
    {
        var service = new SecretProviderRuntimeTrialService(
            new SecretProviderRuntimeTrialOptions(true, "Production", AllowedNames),
            new RecordingSecretProviderRuntime());

        var result = await service.ProbeAsync("crm-common-db-connection");

        Assert.False(result.ReadAttempted);
        Assert.True(result.ProductionBlocked);
        Assert.Equal("ProductionBlocked", result.ErrorCategory);
    }

    [Fact]
    public async Task ProbeAsync_WhenEnabled_ReturnsSanitizedMetadataOnly()
    {
        var service = new SecretProviderRuntimeTrialService(
            new SecretProviderRuntimeTrialOptions(true, "Development", AllowedNames),
            new RecordingSecretProviderRuntime());

        var result = await service.ProbeAsync("crm-common-db-connection");

        Assert.True(result.ReadAttempted);
        Assert.True(result.ReadSucceeded);
        Assert.True(result.ProviderConfigured);
        Assert.False(result.ValueReturned);
        Assert.False(result.ValueLogged);
        Assert.False(result.ValuePersisted);
        Assert.False(result.ValueCached);
        Assert.True(result.RedactionApplied);
    }

    private sealed class RecordingSecretProviderRuntime : ISecretProviderRuntime
    {
        public Task<SecretProviderRuntimeReadResult> ReadAsync(
            SecretProviderRuntimeReadRequest request,
            CancellationToken cancellationToken = default) =>
            Task.FromResult(new SecretProviderRuntimeReadResult(
                SecretName: request.SecretName,
                ReadAttempted: true,
                ReadSucceeded: true,
                ValueObserved: false,
                ValueReturned: false,
                ValueLogged: false,
                ValuePersisted: false,
                ValueCached: false,
                ProviderConfigured: true,
                RedactionApplied: true,
                AllowedSecretName: true,
                Status: "Succeeded",
                Warning: "metadata-only",
                RedactedFingerprint: "sanitized"));
    }
}
