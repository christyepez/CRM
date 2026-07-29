using CRM.Infrastructure.Security.Secrets;
using Xunit;

namespace CRM.UnitTests;

public sealed class SecretProviderRuntimeTests
{
    [Fact]
    public async Task DisabledRuntime_DoesNotAttemptReadOrReturnValue()
    {
        var result = await new DisabledSecretProviderRuntime()
            .ReadAsync(new SecretProviderRuntimeReadRequest("crm-common-db-connection"));

        Assert.False(result.ReadAttempted);
        Assert.False(result.ReadSucceeded);
        Assert.False(result.ValueObserved);
        Assert.False(result.ValueReturned);
        Assert.False(result.ValueLogged);
        Assert.False(result.ValuePersisted);
        Assert.False(result.ValueCached);
        Assert.False(result.ProviderConfigured);
        Assert.True(result.RedactionApplied);
        Assert.Equal("Locked", result.Status);
        Assert.Null(result.RedactedFingerprint);
    }
}
