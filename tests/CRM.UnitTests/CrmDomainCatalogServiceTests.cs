using CRM.Application.Contracts;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmDomainCatalogServiceTests
{
    [Fact]
    public void GetCatalog_ReturnsDraftCrmDomainCatalog()
    {
        var service = new CrmDomainCatalogService();

        var catalog = service.GetCatalog();

        Assert.Equal("CRM", catalog.Module);
        Assert.Equal("Draft", catalog.ContractStatus);
        Assert.Contains(catalog.Entities, entity => entity.Name == "Lead");
        Assert.Contains(catalog.Entities, entity => entity.Name == "Opportunity");
        Assert.Contains(catalog.Events, domainEvent => domainEvent.Name == "OpportunityWonDomainEvent");
    }
}
