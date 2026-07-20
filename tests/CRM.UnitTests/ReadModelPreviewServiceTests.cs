using CRM.Application.ReadModels;
using Xunit;

namespace CRM.UnitTests;

public sealed class ReadModelPreviewServiceTests
{
    [Fact]
    public void LeadReadModelPreview_ReturnsFoundationMockResponse()
    {
        var response = new LeadReadModelPreviewService().Preview(new CrmReadModelQuery(null));

        Assert.True(response.FoundationMode);
        Assert.Equal("FoundationMock", response.Source);
        Assert.Equal("None", response.Persistence);
        Assert.Equal("Read model preview only, not persisted", response.Warning);
    }

    [Fact]
    public void AccountReadModelPreview_ReturnsFoundationMockResponse()
    {
        var response = new AccountReadModelPreviewService().Preview(new CrmReadModelQuery("example"));

        Assert.Single(response.Items);
        Assert.Equal("FoundationMock", response.Source);
        Assert.Equal("None", response.Persistence);
    }

    [Fact]
    public void ContactReadModelPreview_ReturnsFoundationMockResponse()
    {
        var response = new ContactReadModelPreviewService().Preview(new CrmReadModelQuery("example"));

        Assert.Single(response.Items);
        Assert.Equal("Read model preview only, not persisted", response.Warning);
    }

    [Fact]
    public void ReadModelStatus_ReturnsDraftStrategy()
    {
        var response = new CrmReadModelStatusService().GetStatus();

        Assert.Equal("Draft", response.StrategyStatus);
        Assert.Contains(response.ActivationCriteria, item => item.Contains("Migration", StringComparison.OrdinalIgnoreCase));
    }
}
