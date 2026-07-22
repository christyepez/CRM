using CRM.Application.Reporting;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmReportingIntegrationStatusServiceTests
{
    [Fact]
    public void GetStatus_ReturnsPlannedDisconnectedFoundationMock()
    {
        var response = new CrmReportingIntegrationStatusService().GetStatus();

        Assert.Equal("CRM", response.Module);
        Assert.Equal("ReportingIntegrationPlanned", response.Status);
        Assert.Equal("Planned", response.AnalyticsMode);
        Assert.Equal("NonProduction", response.RuntimeMode);
        Assert.Equal("FoundationMock", response.Source);
        Assert.False(response.Connected);
        Assert.True(response.FoundationMode);
        Assert.Contains("FuturePowerBiEmbedding", response.RequiredCapabilities);
        Assert.Contains("EmbedToken", response.BlockedCapabilities);
    }

    [Fact]
    public void GetKpis_ReturnsExpectedConceptualCatalog()
    {
        var kpis = new CrmReportingIntegrationStatusService().GetKpis().Select(kpi => kpi.Name).ToArray();

        Assert.Contains("LeadConversionRate", kpis);
        Assert.Contains("QualifiedLeads", kpis);
        Assert.Contains("OpportunitiesWon", kpis);
        Assert.Contains("OpportunitiesLost", kpis);
        Assert.Contains("PipelineValue", kpis);
        Assert.Contains("AverageSalesCycleDays", kpis);
        Assert.Contains("ActivitiesCompleted", kpis);
        Assert.Contains("FollowUpsPending", kpis);
        Assert.Contains("AccountsActive", kpis);
        Assert.Contains("ContactsActive", kpis);
    }

    [Fact]
    public void ReportingContracts_DoNotExposeCredentialOrPowerBiRuntimeFields()
    {
        var contractTypes = typeof(CrmReportingIntegrationStatusResponse).Assembly.GetTypes()
            .Where(type => type.Namespace == "CRM.Application.Reporting")
            .Where(type => type.Name.EndsWith("Contract", StringComparison.Ordinal) || type.Name.EndsWith("Response", StringComparison.Ordinal));

        foreach (var type in contractTypes)
        {
            var propertyNames = string.Join(" ", type.GetProperties().Select(property => property.Name));

            Assert.DoesNotContain("Token", propertyNames, StringComparison.OrdinalIgnoreCase);
            Assert.DoesNotContain("Password", propertyNames, StringComparison.OrdinalIgnoreCase);
            Assert.DoesNotContain("Secret", propertyNames, StringComparison.OrdinalIgnoreCase);
            Assert.DoesNotContain("WorkspaceId", propertyNames, StringComparison.OrdinalIgnoreCase);
            Assert.DoesNotContain("ReportId", propertyNames, StringComparison.OrdinalIgnoreCase);
            Assert.DoesNotContain("DatasetId", propertyNames, StringComparison.OrdinalIgnoreCase);
            Assert.DoesNotContain("EmbedUrl", propertyNames, StringComparison.OrdinalIgnoreCase);
        }
    }
}
