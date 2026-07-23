using CRM.Application.Portal;
using CRM.Infrastructure.Portal.Runtime;
using Xunit;

namespace CRM.UnitTests;

public sealed class PortalAuthContextMapperPlaceholderTests
{
    [Fact]
    public void Placeholder_ReturnsContractMappingsWithoutRuntimeConnection()
    {
        var mapper = new PortalAuthContextMapperPlaceholder();
        var mappings = mapper.GetMappings();

        Assert.False(mapper.Configured);
        Assert.False(mapper.RuntimeConnected);
        Assert.Equal(CrmPortalAuthRuntimeContractStatusService.WarningText, PortalAuthContextMapperPlaceholder.Warning);
        Assert.Contains(mappings, mapping => mapping.Target == "CrmUserContext.UserId");
        Assert.Contains(mappings, mapping => mapping.Target == "CrmUserContext.TenantId");
        Assert.All(mappings, mapping => Assert.False(mapping.RuntimeMapped));
    }
}
