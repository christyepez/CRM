using CRM.Application.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class FoundationPreviewServiceTests
{
    [Fact]
    public void LeadPreview_ReturnsFoundationOnlyResponse()
    {
        var response = new LeadFoundationService().Preview(new LeadFoundationRequest(
            "Ada",
            "Lovelace",
            "ada@example.test",
            "Example Co",
            "Website"));

        Assert.True(response.FoundationMode);
        Assert.Equal("None", response.Persistence);
        Assert.Equal("Preview only, not persisted", response.Warning);
    }

    [Fact]
    public void AccountPreview_ReturnsFoundationOnlyResponse()
    {
        var response = new AccountFoundationService().Preview(new AccountFoundationRequest(
            "Example Co",
            null,
            "Technology",
            "Enterprise"));

        Assert.True(response.FoundationMode);
        Assert.Equal("Account", response.Entity);
        Assert.Equal("None", response.Persistence);
    }

    [Fact]
    public void ContactPreview_ReturnsFoundationOnlyResponse()
    {
        var response = new ContactFoundationService().Preview(new ContactFoundationRequest(
            "Grace",
            "Hopper",
            "grace@example.test",
            null,
            "Decision maker",
            "Email"));

        Assert.True(response.FoundationMode);
        Assert.Equal("Contact", response.Entity);
        Assert.Equal("Preview only, not persisted", response.Warning);
    }
}
