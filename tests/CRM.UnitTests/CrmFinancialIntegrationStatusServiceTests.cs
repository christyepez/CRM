using CRM.Application.Financial;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmFinancialIntegrationStatusServiceTests
{
    [Fact]
    public void GetStatus_ReturnsPlannedDisconnectedFinancialOwnership()
    {
        var response = new CrmFinancialIntegrationStatusService().GetStatus();

        Assert.Equal("CRM", response.Module);
        Assert.Equal("FinancialIntegrationPlanned", response.Status);
        Assert.Equal("Planned", response.IntegrationMode);
        Assert.Equal("NonProduction", response.RuntimeMode);
        Assert.Equal("Financiero", response.CapabilityOwner);
        Assert.False(response.Connected);
        Assert.True(response.FoundationMode);
        Assert.Contains("NoSharedDatabase", response.IntegrationPatterns);
    }

    [Fact]
    public void FinancialContracts_DoNotExposeSensitiveCredentialOrTaxFields()
    {
        var contractTypes = typeof(CrmFinancialIntegrationStatusResponse).Assembly.GetTypes()
            .Where(type => type.Namespace == "CRM.Application.Financial")
            .Where(type => type.Name.EndsWith("Contract", StringComparison.Ordinal) || type.Name.EndsWith("Response", StringComparison.Ordinal));

        foreach (var type in contractTypes)
        {
            var propertyNames = string.Join(" ", type.GetProperties().Select(property => property.Name));

            Assert.DoesNotContain("Token", propertyNames, StringComparison.OrdinalIgnoreCase);
            Assert.DoesNotContain("Password", propertyNames, StringComparison.OrdinalIgnoreCase);
            Assert.DoesNotContain("Secret", propertyNames, StringComparison.OrdinalIgnoreCase);
            Assert.DoesNotContain("ClaveAcceso", propertyNames, StringComparison.OrdinalIgnoreCase);
            Assert.DoesNotContain("Sri", propertyNames, StringComparison.OrdinalIgnoreCase);
        }
    }
}
