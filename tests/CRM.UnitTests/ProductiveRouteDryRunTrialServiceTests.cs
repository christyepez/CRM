using CRM.Api.ProductiveRoutes;
using CRM.Application.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class ProductiveRouteDryRunTrialServiceTests
{
    [Fact]
    public void Probe_WhenDisabled_ReturnsLockedWithoutSideEffects()
    {
        var service = CreateService(enabled: false);

        var result = service.Probe(new CrmProductiveRouteDryRunTrialProbeContract("/api/crm/leads", "GET"));

        Assert.False(result.ProductiveRouteDryRunAttempted);
        Assert.False(result.ProductiveRouteDryRunAllowed);
        Assert.Equal(423, result.ProductiveRouteDryRunStatusCode);
        Assert.False(result.ProductiveCrudEnabled);
        Assert.False(result.ProductiveDomainExecutionEnabled);
        Assert.False(result.ProductivePersistenceEnabled);
        Assert.False(result.DatabaseWriteAttempted);
        Assert.False(result.SideEffectsAllowed);
        Assert.False(result.DeleteEndpointsEnabled);
        Assert.False(result.DbRuntimeEnabled);
        Assert.False(result.EfRuntimeEnabled);
        Assert.False(result.AuthHeaderRead);
        Assert.False(result.TokenRead);
        Assert.Equal("FlagDisabled", result.ErrorCategory);
    }

    [Fact]
    public void Probe_WhenProduction_ReturnsLocked()
    {
        var service = CreateService(enabled: true, environment: "Production");

        var result = service.Probe(new CrmProductiveRouteDryRunTrialProbeContract("/api/crm/leads", "GET"));

        Assert.False(result.ProductiveRouteDryRunAttempted);
        Assert.True(result.ProductionBlocked);
        Assert.Equal("ProductionBlocked", result.ErrorCategory);
        Assert.Equal(423, result.ProductiveRouteDryRunStatusCode);
    }

    [Fact]
    public void Probe_WhenDeleteRequested_ReturnsLockedWithoutEnablingDelete()
    {
        var service = CreateService(enabled: true);

        var result = service.Probe(new CrmProductiveRouteDryRunTrialProbeContract("/api/crm/leads/1", "DELETE"));

        Assert.False(result.ProductiveRouteDryRunAttempted);
        Assert.False(result.DeleteEndpointsEnabled);
        Assert.Equal("DeleteBlocked", result.ErrorCategory);
        Assert.Equal(423, result.ProductiveRouteDryRunStatusCode);
    }

    [Fact]
    public void Probe_WhenExplicitNonProductionFlagEnabled_ReturnsSanitizedLockedDecision()
    {
        var service = CreateService(enabled: true);

        var result = service.Probe(new CrmProductiveRouteDryRunTrialProbeContract("/api/crm/leads", "GET"));

        Assert.True(result.ProductiveRouteDryRunAttempted);
        Assert.False(result.ProductiveRouteDryRunAllowed);
        Assert.True(result.ProductiveRouteDryRunDecisionReturned);
        Assert.Equal(423, result.ProductiveRouteDryRunStatusCode);
        Assert.True(result.SecretProviderMetadataDependencyValidated);
        Assert.True(result.CommonDbMetadataDependencyValidated);
        Assert.True(result.PortalAuthMetadataDependencyValidated);
        Assert.False(result.AuthHeaderRead);
        Assert.False(result.TokenRead);
        Assert.False(result.TokenStored);
        Assert.False(result.AuthAttributeEnabled);
        Assert.False(result.ProductiveCrudEnabled);
        Assert.False(result.DatabaseWriteAttempted);
        Assert.False(result.SideEffectsAllowed);
    }

    private static ProductiveRouteDryRunTrialService CreateService(bool enabled, string environment = "Development") =>
        new(
            new ProductiveRouteDryRunTrialOptions(enabled, environment),
            new CrmProductiveRouteDryRunTrialEvaluator());
}
