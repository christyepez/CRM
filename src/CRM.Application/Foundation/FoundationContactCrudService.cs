using CRM.Application.Persistence;
using CRM.Application.Portal;
using CRM.Application.Ports.Persistence;

namespace CRM.Application.Foundation;

public sealed class FoundationContactCrudService(
    IContactFoundationStore store,
    ICrmPersistenceFeatureFlagProvider flags,
    CrmFoundationPermissionGuard permissionGuard)
{
    public async Task<FoundationCrudOperationResult<IReadOnlyCollection<FoundationContactResponse>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        FoundationCrudSupport.EnsureSafeFlags(flags);
        var permission = await permissionGuard.CheckAsync("crm.foundation.read", cancellationToken);
        IReadOnlyCollection<FoundationContactResponse> data = permission.Allowed ? (await store.GetPreviewAsync(cancellationToken)).Select(ToResponse).ToArray() : [];
        return FoundationCrudSupport.Result<IReadOnlyCollection<FoundationContactResponse>>("Read", "Contact", data, "crm.foundation.read", permission);
    }

    public async Task<FoundationCrudOperationResult<FoundationContactResponse>> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        FoundationCrudSupport.EnsureSafeFlags(flags);
        var permission = await permissionGuard.CheckAsync("crm.foundation.read", cancellationToken);
        var item = permission.Allowed ? await store.GetPreviewByIdAsync(id, cancellationToken) : null;
        return FoundationCrudSupport.Result("ReadById", "Contact", item is null ? null : ToResponse(item), "crm.foundation.read", permission);
    }

    public async Task<FoundationCrudOperationResult<FoundationContactResponse>> CreateAsync(FoundationContactCreateRequest request, CancellationToken cancellationToken = default)
    {
        FoundationCrudSupport.EnsureSafeFlags(flags);
        var permission = await permissionGuard.CheckAsync("crm.foundation.preview.write", cancellationToken);
        var response = FromCreate($"contact-foundation-{Guid.NewGuid():N}"[..31], request);
        if (permission.Allowed)
        {
            await store.SavePreviewAsync(ToPreview(response), cancellationToken);
        }

        return FoundationCrudSupport.Result("Create", "Contact", response, "crm.foundation.preview.write", permission);
    }

    public async Task<FoundationCrudOperationResult<FoundationContactResponse>> UpdateAsync(string id, FoundationContactUpdateRequest request, CancellationToken cancellationToken = default)
    {
        FoundationCrudSupport.EnsureSafeFlags(flags);
        var permission = await permissionGuard.CheckAsync("crm.foundation.preview.write", cancellationToken);
        var existing = await store.GetPreviewByIdAsync(id, cancellationToken);
        var response = FromUpdate(id, request, existing);
        if (permission.Allowed)
        {
            await store.SavePreviewAsync(ToPreview(response), cancellationToken);
        }

        return FoundationCrudSupport.Result("Update", "Contact", response, "crm.foundation.preview.write", permission);
    }

    private static FoundationContactResponse FromCreate(string id, FoundationContactCreateRequest request) =>
        new(id, FoundationCrudSupport.CleanOptional(request.FirstName), FoundationCrudSupport.CleanOptional(request.LastName), FoundationCrudSupport.CleanOptional(request.Email), FoundationCrudSupport.CleanOptional(request.Phone), FoundationCrudSupport.CleanOptional(request.Title), "PreviewOnly", true, "NonProductionSeam", false, false, false, "FoundationSimulation", FoundationCrudSupport.WarningText);

    private static FoundationContactResponse FromUpdate(string id, FoundationContactUpdateRequest request, CrmFoundationPreviewItemContract? existing) =>
        new(id, FoundationCrudSupport.CleanOptional(request.FirstName), FoundationCrudSupport.CleanOptional(request.LastName), FoundationCrudSupport.CleanOptional(request.Email), FoundationCrudSupport.CleanOptional(request.Phone), FoundationCrudSupport.CleanOptional(request.Title), FoundationCrudSupport.Clean(request.Status, existing?.Status ?? "PreviewOnly"), true, "NonProductionSeam", false, false, false, "FoundationSimulation", FoundationCrudSupport.WarningText);

    private static CrmFoundationPreviewItemContract ToPreview(FoundationContactResponse response) =>
        FoundationCrudSupport.ToPreview(response.Id, "Contact", FoundationCrudSupport.Clean($"{response.FirstName} {response.LastName}", "Contact Foundation Preview"), response.Status, new Dictionary<string, string> { ["email"] = response.Email ?? "", ["phone"] = response.Phone ?? "", ["title"] = response.Title ?? "" });

    private static FoundationContactResponse ToResponse(CrmFoundationPreviewItemContract item) =>
        new(item.Id, item.DisplayName.Split(' ', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault(), item.DisplayName.Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1).FirstOrDefault(), item.Metadata.GetValueOrDefault("email"), item.Metadata.GetValueOrDefault("phone"), item.Metadata.GetValueOrDefault("title"), item.Status, true, "NonProductionSeam", false, false, false, "FoundationSimulation", FoundationCrudSupport.WarningText);
}
