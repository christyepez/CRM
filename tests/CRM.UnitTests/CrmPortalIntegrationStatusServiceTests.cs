using CRM.Application.Portal;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmPortalIntegrationStatusServiceTests
{
    [Fact]
    public void GetStatus_ReturnsPlannedDisconnectedPortalOwnership()
    {
        var response = new CrmPortalIntegrationStatusService().GetStatus();

        Assert.Equal("CRM", response.Module);
        Assert.Equal("PortalIntegrationPlanned", response.Status);
        Assert.Equal("Planned", response.IntegrationMode);
        Assert.Equal("NonProduction", response.RuntimeMode);
        Assert.Equal("PortalCorporativo", response.CapabilityOwner);
        Assert.False(response.Connected);
        Assert.True(response.FoundationMode);
        Assert.Contains("Security/Auth", response.RequiredCapabilities);
    }

    [Fact]
    public void PortalContracts_DoNotExposeSensitiveCredentialFields()
    {
        var contractTypes = typeof(CrmPortalIntegrationStatusResponse).Assembly.GetTypes()
            .Where(type => type.Namespace == "CRM.Application.Portal")
            .Where(type => type.Name.EndsWith("Contract", StringComparison.Ordinal) || type.Name.EndsWith("Response", StringComparison.Ordinal));

        foreach (var type in contractTypes)
        {
            var propertyNames = string.Join(" ", type.GetProperties().Select(property => property.Name));

            Assert.DoesNotContain("Token", propertyNames, StringComparison.OrdinalIgnoreCase);
            Assert.DoesNotContain("Password", propertyNames, StringComparison.OrdinalIgnoreCase);
            Assert.DoesNotContain("Secret", propertyNames, StringComparison.OrdinalIgnoreCase);
        }
    }
}
