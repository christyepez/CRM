using CRM.Application.Persistence;
using CRM.Infrastructure.Persistence.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class InMemoryFoundationStoreTests
{
    [Fact]
    public async Task SavePreviewAsync_KeepsPreviewInMemoryOnly()
    {
        var store = new InMemoryLeadFoundationStore();
        var preview = new CrmFoundationPreviewItemContract(
            "lead-preview-test",
            "Lead",
            "Test Preview",
            "PreviewOnly",
            new DateTimeOffset(2026, 7, 22, 0, 0, 0, TimeSpan.Zero),
            new Dictionary<string, string> { ["warning"] = "NonProductionSeam" });

        await store.SavePreviewAsync(preview);
        var previews = await store.GetPreviewAsync();
        var status = await store.GetStatusAsync();

        Assert.Contains(previews, item => item.Id == "lead-preview-test");
        Assert.Equal("NonProductionSeam", status.PersistenceMode);
        Assert.True(status.FoundationStoreEnabled);
        Assert.False(status.DurablePersistence);
    }

    [Fact]
    public async Task ClearPreviewAsync_RemovesPreviewWithoutDurablePersistence()
    {
        var store = new InMemoryAccountFoundationStore();

        await store.ClearPreviewAsync();
        var status = await store.GetStatusAsync();

        Assert.Equal(0, status.PreviewCount);
        Assert.False(status.DurablePersistence);
        Assert.Equal(CrmPersistenceSeamStatusService.WarningText, status.Warning);
    }

    [Fact]
    public async Task GetPreviewByIdAsync_ReturnsSavedFoundationPreview()
    {
        var store = new InMemoryContactFoundationStore();
        var preview = new CrmFoundationPreviewItemContract(
            "contact-preview-test",
            "Contact",
            "Contact Preview",
            "PreviewOnly",
            new DateTimeOffset(2026, 7, 22, 0, 0, 0, TimeSpan.Zero),
            new Dictionary<string, string> { ["email"] = "contact@example.test" });

        await store.SavePreviewAsync(preview);
        var result = await store.GetPreviewByIdAsync("contact-preview-test");

        Assert.NotNull(result);
        Assert.Equal("Contact Preview", result.DisplayName);
    }
}
